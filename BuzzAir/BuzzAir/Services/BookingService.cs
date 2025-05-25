using BuzzAir.Helpers;
using BuzzAir.Models.DbModels.Contraccts;
using BuzzAir.Services.Contracts;
using System.Collections;

namespace BuzzAir.Services
{
    public class BookingService(
        BuzzAirDbContext context,
        IFlightsService flightsService,
        IPassengerService passengerService,
        IUserBookingService userBookingService,
        IPriceCalculator priceCalculator,
        IFlightPassengerService flightPassengerService,
        IPaymentService paymentService,
        IBookingFlightService bookingFlightService,
        IBookingPassengerService bookingPassengerService,
        UserManager<ApplicationUser> userManager,
        ICityService cityService) : IBookingService
    {
        public async Task CreateAsync(Payment payment)
        {
            Booking booking = BookingFactory.CreateBooking(payment);

            await context.Bookings.AddAsync(booking);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Booking booking = await GetByIdAsync(id);
            booking.IsDeleted = true;

            IEnumerable<Flight> flights = booking.Flights.Select(x => x.Flight);

            // Make the seats available
            // TO DO: Fix this as it won't work
            foreach (Flight flight in flights)
            {
                List<int> seats = [.. flight.Passengers.Select(x => x.SeatNumber)];
                flight.Seats.Where(x => seats.Contains(x.SeatNumber)).First().IsAvailable = true;
            }

            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            return await context.Bookings.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await context.Bookings
                .Include(x => x.Payment)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Origin)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Destination)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Aircraft)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Passengers)
                            .ThenInclude(x => x.Person)
                .Include(x => x.Passengers)
                    .ThenInclude(x => x.Passenger)
                .ThenInclude(x => x.Services)
                .ThenInclude(x => x.Service)
                .Where(x => x.IsDeleted == false)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(string id)
        {
            return await context.Bookings
                .Include(x => x.Payment)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Origin)
                            .ThenInclude(x => x.City)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Destination)
                            .ThenInclude(x => x.City)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Aircraft)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Passengers)
                            .ThenInclude(x => x.Person)
                .Include(x => x.Passengers)
                    .ThenInclude(x => x.Passenger)
                        .ThenInclude(x => x.Services)
                            .ThenInclude(x => x.Service)
                .Where(x => x.IsDeleted == false)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id) ??
                throw new ArgumentException($"Can't find a booking with id {id}");
        }

        public async Task<BookingViewModel> GetBookingDetailsAsync(string id)
        {
            Booking booking = await GetByIdAsync(id);
            List<FlightViewModel> flights = flightsService.GetFlightsDetails(booking.Flights);
            List<PassengerViewModel> passengers = passengerService.GetPassengersDetails(booking.Passengers);

            BookingViewModel viewModel = BookingFactory.CreateViewModel(booking, flights, passengers);

            return viewModel;
        }

        public async Task<AllBookingsViewModel> GetAllAsync(string? username)
        {
            if (username == null)
            {
                throw new ArgumentException($"Invalid username. Can't retrieve any bookings.");
            }

            IEnumerable<UserBooking> userBookings = await userBookingService.GetAllForUser(username);
            List<string> bookingIds = GetAllForUser(userBookings);

            int bookingsCount = bookingIds.Count;
            List<Task<BookingViewModel>> bookingTasks = new(bookingsCount);

            foreach (string bookingId in bookingIds)
            {
                Task<BookingViewModel> viewModelTask = GetBookingDetailsAsync(bookingId);

                bookingTasks.Add(viewModelTask);
            }

            await Task.WhenAll(bookingTasks);
            List<BookingViewModel> viewModels = new(bookingsCount);

            foreach (Task<BookingViewModel> completedTask in bookingTasks)
            {
                viewModels.Add(completedTask.Result);
            }

            AllBookingsViewModel viewModel = BookingFactory.CreateViewModelForAll(viewModels);

            return viewModel;
        }

        private static List<string> GetAllForUser(IEnumerable<UserBooking> userBookings)
        {
            List<string> bookingsIds = [.. userBookings.Select(y => y.BookingId)];

            return bookingsIds;
        }

        public async Task CreateAsync(CreateBookingViewModel model, string username)
        {
            ApplicationUser? currentUser = await userManager.FindByNameAsync(username);

            if (currentUser == null)
            {
                throw new ArgumentException($"The user with username {username} does not exist.");
            }

            Task<Flight?> outboundTask = flightsService.GetById(model.GoingFlightSelection ?? string.Empty);
            Task<Flight?> inboundTask = flightsService.GetById(model.ReturnFlightSelection ?? string.Empty);

            await Task.WhenAll(outboundTask, inboundTask);

            Flight? outbound = outboundTask.Result;
            Flight? inbound = inboundTask.Result;

            if (outbound == null)
            {
                throw new ArgumentException($"No outbound flight has been selected.");
            }

            decimal bookingPrice = priceCalculator.Calculate(model, outbound, inbound);

            // Somebody hacked the FE
            if (bookingPrice != model.Price)
            {
                throw new ArgumentException(
                    $"The price coming from the front-end {model.Price} doesn't match the price from the back-end {bookingPrice}.\nCan't proceed with the booking.");
            }

            bool validExpiryDate = DateValidator.IsValidExpiryDate(model.Payment.ExpiryDate, out DateTime expiryDate);

            if (!validExpiryDate)
            {
                throw new ArgumentException($"Invalid expiry date {model.Payment.ExpiryDate}.");
            }

            List<IPassenger> passengers = await passengerService.CreatePassengersAsync(model.Passengers);

            Task[] flightPassengerTasks =
            [
                flightPassengerService.CreateAsync(passengers, outbound),
                flightPassengerService.CreateAsync(passengers, inbound)
            ];

            await Task.WhenAll(flightPassengerTasks);

            Payment payment = await paymentService.CreateAsync(model.Payment, expiryDate, bookingPrice);
            Booking booking = BookingFactory.CreateBooking(payment);

            await context.Bookings.AddAsync(booking);
            await context.SaveChangesAsync();

            Task[] bookingTasks =
            [
                bookingFlightService.CreateAsync(booking, outbound, isOutbound: true),
                bookingFlightService.CreateAsync(booking, inbound),
                bookingPassengerService.CreateAsync(passengers, booking),
                userBookingService.CreateAsync(currentUser, booking)
            ];

            await Task.WhenAll(bookingTasks);
        }

        public Task CreateAsync(CreateBookingViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateBookingViewModel> CreateViewModelAsync(IndexViewModel model)
        {
            bool isReturning = model.isReturning != "OneWay";

            if (isReturning && model.Departure > model.Return)
            {
                throw new ArgumentException($"The return date {model.Return:yyyy-MM-dd} must be after the departure date {model.Departure:yyyy-MM-dd}.");
            }

            Task<City> originTask = cityService.GetByNameAsync(model.Origin);
            Task<City> destinationTask = cityService.GetByNameAsync(model.Destination);

            await Task.WhenAll(originTask, destinationTask);

            City origin = originTask.Result;
            City destination = destinationTask.Result;

            IEnumerable<Flight> outboundFlights = await flightsService.GetFlightsByOriginAndDestination(origin, destination, model.Departure);
            IEnumerable<Flight> inboundFlights = await flightsService.GetFlightsByOriginAndDestination(origin, destination, model.Return ?? new DateTime());

            List<FlightViewModel> outboundModels = flightsService.GetViewModels(outboundFlights);
            List<FlightViewModel> inboundModels = flightsService.GetViewModels(inboundFlights);
            List<PassengerViewModel> passengers = passengerService.GetViewModels(model.Passengers);

            CreateBookingViewModel viewModel = BookingFactory.CreateViewModel(outboundModels, inboundModels, passengers);

            return viewModel;
        }
    }
}
