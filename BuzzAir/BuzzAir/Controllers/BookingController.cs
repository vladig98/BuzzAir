using BuzzAir.Models.CreateModels;
using BuzzAir.Models.DbModels;
using BuzzAir.Models.DbModels.Contraccts;
using BuzzAir.Models.DbModels.Enums;
using BuzzAir.Models.DbModels.Services;
using BuzzAir.Models.ViewModels;
using BuzzAir.Services.Contracts;
using BuzzAir.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection;

namespace BuzzAir.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IUserBookingService _userBookingService;
        private readonly IPaymentService _paymentService;
        private readonly IFlightsService _flightsService;
        private readonly IBookingFlightService _bookingFlightService;
        private readonly IBookingPassengerService _bookingPassengerService;
        private readonly IAirportService _airportService;
        private readonly ICityService _cityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPassengerService _passengerService;
        private readonly IFlightPassengerService _flightPassengerService;
        private readonly IServiceService _serviceService;

        public BookingController(IBookingService bookingService, IUserBookingService userBookingService,
            IPaymentService paymentService, IFlightsService flightsService, IBookingFlightService bookingFlightService,
            IBookingPassengerService bookingPassengerService, UserManager<ApplicationUser> userManager, IAirportService airportService,
            ICityService cityService, IPassengerService passengerService, IFlightPassengerService flightPassengerService, IServiceService serviceService)
        {
            _bookingService = bookingService;
            _userBookingService = userBookingService;
            _paymentService = paymentService;
            _flightsService = flightsService;
            _bookingFlightService = bookingFlightService;
            _bookingPassengerService = bookingPassengerService;
            _userManager = userManager;
            _airportService = airportService;
            _cityService = cityService;
            _passengerService = passengerService;
            _flightPassengerService = flightPassengerService;
            _serviceService = serviceService;
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            //check if the booking exists
            var bookingExists = await _bookingService.ExistsById(id);

            //throw an error if the booking does not exist
            if (!bookingExists)
            {
                return BadRequest();
            }

            //delete the booking
            await _bookingService.Delete(id);

            return this.Redirect("/Booking/All");
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var booking = await _bookingService.GetById(id);

            if (booking == null)
            {
                return BadRequest();
            }

            List<FlightViewModel> flights = new List<FlightViewModel>();

            foreach (var flight in booking.Flights)
            {
                flights.Add(new FlightViewModel
                {
                    Id = flight.Flight.Id,
                    Arrival = flight.Flight.Arrival,
                    Departure = flight.Flight.Departure,
                    Destination = new AirportViewModel
                    {
                        City = new CityViewModel
                        {
                            Name = flight.Flight.Destination.City.Name
                        },
                        IATA = flight.Flight.Destination.IATA,
                        Name = flight.Flight.Destination.Name
                    },
                    FlightNumber = flight.Flight.FlightNumber,
                    Origin = new AirportViewModel
                    {
                        City = new CityViewModel
                        {
                            Name = flight.Flight.Origin.City.Name
                        },
                        IATA = flight.Flight.Origin.IATA,
                        Name = flight.Flight.Origin.Name
                    },
                    Price = flight.Flight.Price,
                    IsOutbound = flight.IsOutbound
                });
            }

            var resultsPassengers = string.Empty;

            List<PassengerViewModel> pax = new List<PassengerViewModel>();

            for (int i = 0; i < booking.Passengers.Count; i++)
            {
                var passeneger = booking.Passengers.ElementAt(i).Passenger;
                var services = ((Passenger)passeneger).Services.ToList();

                List<ServiceViewModel> paxServices = new List<ServiceViewModel>();

                foreach (var service in services)
                {
                    paxServices.Add(new ServiceViewModel()
                    {
                        Name = service.Service.Name,
                        Price = service.Service.Price,
                        Kilos = service.Service.GetType() == typeof(Baggage) ? int.Parse(Math.Floor(((Baggage)service.Service).Kilos).ToString()) : 0,
                        Seat = service.Service.GetType() == typeof(Seat) ? ((Seat)service.Service).SeatType.GetDisplayName() : SeatType.Normal.ToString()
                    });
                }

                pax.Add(new PassengerViewModel
                {
                    FirstName = passeneger.FirstName,
                    LastName = passeneger.LastName,
                    Gender = passeneger.Gender,
                    Services = paxServices
                });
            }

            var model = new BookingViewModel
            {
                Amount = booking.TotalPrice.ToString("F2"),
                Inbound = flights.FirstOrDefault(x => !x.IsOutbound),
                Outbound = flights.FirstOrDefault(x => x.IsOutbound),
                Id = booking.Id,
                Passengers = pax,
                Currency = booking.Payment.Currency.ToString()
            };

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var model = new AllBookingsViewModel();

            //get all booking specifict to the user
            var userBookings = User.IsInRole(GlobalConstants.AdminRole) ? await _userBookingService.GetAll() : await _userBookingService.GetAllForUser(User.Identity.Name);
            var bookings = await _bookingService.GetAllForUser(userBookings);

            //create viewModel for each booking
            foreach (var booking in bookings)
            {
                model.Bookings.Add(new BookingViewModel
                {
                    Amount = booking.Payment.Amount.ToString("F2"),
                    Id = booking.Id,
                    Currency = booking.Payment.Currency.ToString(),
                    User = userBookings.Where(x => x.Booking == booking).Select(x => x.ApplicationUser.UserName).FirstOrDefault(),
                    Inbound = booking.Flights.Count() > 1 ?
                                new FlightViewModel
                                {
                                    Arrival = booking.Flights.First(x => !x.IsOutbound).Flight.Arrival,
                                    Departure = booking.Flights.First(x => !x.IsOutbound).Flight.Departure,
                                    Destination = new AirportViewModel
                                    {
                                        City = new CityViewModel
                                        {
                                            Name = booking.Flights.First(x => !x.IsOutbound).Flight.Destination.City.Name
                                        },
                                        IATA = booking.Flights.First(x => !x.IsOutbound).Flight.Destination.IATA,
                                        Name = booking.Flights.First(x => !x.IsOutbound).Flight.Destination.Name
                                    },
                                    FlightNumber = booking.Flights.First(x => !x.IsOutbound).Flight.FlightNumber,
                                    Id = booking.Flights.First(x => !x.IsOutbound).Flight.Id,
                                    Origin = new AirportViewModel
                                    {
                                        City = new CityViewModel
                                        {
                                            Name = booking.Flights.First(x => !x.IsOutbound).Flight.Origin.City.Name
                                        },
                                        IATA = booking.Flights.First(x => !x.IsOutbound).Flight.Origin.IATA,
                                        Name = booking.Flights.First(x => !x.IsOutbound).Flight.Origin.Name
                                    },
                                    Price = booking.Flights.First(x => !x.IsOutbound).Flight.Price
                                } : null,
                    Outbound = new FlightViewModel
                    {
                        Arrival = booking.Flights.First(x => x.IsOutbound).Flight.Arrival,
                        Departure = booking.Flights.First(x => x.IsOutbound).Flight.Departure,
                        Destination = new AirportViewModel
                        {
                            City = new CityViewModel
                            {
                                Name = booking.Flights.First(x => x.IsOutbound).Flight.Destination.City.Name
                            },
                            IATA = booking.Flights.First(x => x.IsOutbound).Flight.Destination.IATA,
                            Name = booking.Flights.First(x => x.IsOutbound).Flight.Destination.Name
                        },
                        FlightNumber = booking.Flights.First(x => x.IsOutbound).Flight.FlightNumber,
                        Id = booking.Flights.First(x => x.IsOutbound).Flight.Id,
                        Origin = new AirportViewModel
                        {
                            City = new CityViewModel
                            {
                                Name = booking.Flights.First(x => x.IsOutbound).Flight.Origin.City.Name
                            },
                            IATA = booking.Flights.First(x => x.IsOutbound).Flight.Origin.IATA,
                            Name = booking.Flights.First(x => x.IsOutbound).Flight.Origin.Name
                        },
                        Price = booking.Flights.First(x => x.IsOutbound).Flight.Price
                    }
                });
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingViewModel model)
        {
            //check if any input does not have valid data
            if (!ModelState.IsValid)
            {
                //send the view with all incorrect inputs for the user to correct them
                return View("CreateBooking", model);
            }

            //Get the flights
            Flight goingFlight = await _flightsService.GetById(model.GoingFlightSelection);
            Flight returnFlight = await _flightsService.GetById(model.ReturnFlightSelection);

            //you will need to book at least 1 flight. This is to prevent API calls buying for non-existent flights
            if (goingFlight == null)
            {
                return BadRequest();
            }

            //calculate the price for the booking
            decimal price = returnFlight == null ? (decimal)(model.Passengers.Sum(x => x.Services.Where(y => y.IsChecked).Select(y => y.Price).Sum()) + //get all services prices
                model.Passengers.Where(x => x.BaggageType != BaggageType.Cabin).Sum(x => decimal.Parse(x.BaggageType.GetPrice())) + //get all prices for baggage
                goingFlight.Price * model.Passengers.Count) : //get all ticket priced for the outbound flight
                (decimal)(model.Passengers.Sum(x => x.Services.Where(y => y.IsChecked).Select(y => y.Price).Sum()) + //get all services prices
                model.Passengers.Where(x => x.BaggageType != BaggageType.Cabin).Sum(x => decimal.Parse(x.BaggageType.GetPrice())) + //get all prices for baggage
                goingFlight.Price * model.Passengers.Count +
                returnFlight?.Price * model.Passengers.Count);

            //check if the price matches the price shown to the client, if not, throw an error. This is also supposed to prevent falsly submitted values via an API call
            if (price != model.Price)
            {
                return BadRequest();
            }

            //Check if the expiryDate for the Payment is in the desired format and parse it to a DateTime if it is
            bool validDate = DateTime.TryParseExact(model.Payment.ExpiryDate, "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate);

            //return the View with an error if the expiryDate does not fit the pattern
            if (!validDate)
            {
                ModelState.AddModelError("Payment.ExpiryDate", "Invalid date format");
                return View("CreateBooking", model);
            }

            //generate a collection for all passengers
            IList<IPassenger> passengers = new List<IPassenger>();

            //Loop through each passenger's details from the view and create a new passenger object
            foreach (PassengerViewModel passenger in model.Passengers)
            {
                //generate a collection for all services the passenger wants
                IList<IService> services = new List<IService>();

                //Get all valid services Types
                IEnumerable<Type> servicesTypes = from type in Assembly.GetExecutingAssembly().GetTypes()
                                                  where type.IsClass && type.Namespace == typeof(AirportCheckIn).Namespace
                                                  select type;

                // Get all valid services names
                IList<string> servicesNames = servicesTypes.ToList().Select(x => x.Name).ToList();

                //loop through each service details from the view and create an intance of a service object
                foreach (ServiceViewModel item in passenger.Services)
                {
                    //add a service only if it is selected
                    if (item.IsChecked)
                    {
                        //Prevents injecting non-existent Services using an API call
                        if (!servicesNames.Contains(item.Name))
                        {
                            return BadRequest();
                        }

                        //Create the service
                        IService service = await _serviceService.Create(item.Name);

                        //add the service instance to the service collection
                        services.Add(service);
                    }
                }

                //create the baggage service and add it to the collection
                IService baggage = await _serviceService.Create(passenger.BaggageType);
                services.Add(baggage);

                //create a passenger
                IPassenger pax = await _passengerService.Create(passenger.FirstName, passenger.LastName, passenger.Gender, passenger.BaggageType, services);

                //associate the passenger with each flight
                FlightPassenger flightPaxGoing = await _flightPassengerService.Create(pax, goingFlight);
                if (returnFlight != null)
                {
                    FlightPassenger flightPaxReturning = await _flightPassengerService.Create(pax, returnFlight);
                }

                //add the passenger to the passengers collection
                passengers.Add(pax);
            }

            //create the payment object
            Payment payment = await _paymentService.Create(model.Payment.CardType, model.Payment.CardNumber, expiryDate, model.Payment.CardHolder, model.Payment.CVC, price, model.Payment.Currency);

            //create the booking object
            Booking booking = await _bookingService.Create(payment);

            //Associate the booking with the flights
            BookingFlight goingPax = await _bookingFlightService.Create(goingFlight, booking, true);
            if (returnFlight != null)
            {
                BookingFlight returnPax = await _bookingFlightService.Create(returnFlight, booking);
            }

            //Associate each passenger with the booking
            foreach (Passenger pax in passengers)
            {
                BookingPassenger bookingPax = await _bookingPassengerService.Create(booking, pax);
            }

            //get the logged in user and assign the booking to them
            ApplicationUser cuurentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            UserBooking userBooking = await _userBookingService.Create(cuurentUser, booking);

            //return the list of bookings View
            return this.Redirect("All");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBooking(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("/", model);
            }

            //check if they are booking a OneWay ticket
            bool isReturning = model.isReturning == "OneWay" ? false : true;

            //get destination and origin to look for flights
            var origin = await _cityService.GetById(model.Origin);
            var destination = await _cityService.GetById(model.Destination);

            //throw an error if the origin or the destination is not valid
            if (origin == null || destination == null)
            {
                return BadRequest();
            }

            //check if the returning date is after the going date
            if (isReturning)
            {
                if (model.Departure > model.Return)
                {
                    return BadRequest();
                }
            }

            //get flights options
            var goingFlights = await _flightsService.GetFlightsByOriginAndDestination(origin, destination, model.Departure);
            var returningFlights = new List<Flight>();

            if (isReturning)
            {
                returningFlights = (List<Flight>)await _flightsService.GetFlightsByOriginAndDestination(destination, origin, model.Return.Value);
            }

            List<FlightViewModel> going = new List<FlightViewModel>();

            //create a viewModel for each flight option
            foreach (var item in goingFlights)
            {
                going.Add(new FlightViewModel()
                {
                    Arrival = item.Arrival,
                    Departure = item.Departure,
                    Id = item.Id,
                    Price = item.Price,
                    Destination = new AirportViewModel()
                    {
                        IATA = item.Destination.IATA,
                        City = new CityViewModel()
                        {
                            Name = item.Destination.City.Name
                        },
                        Name = item.Destination.Name
                    },
                    Origin = new AirportViewModel()
                    {
                        IATA = item.Origin.IATA,
                        City = new CityViewModel()
                        {
                            Name = item.Origin.City.Name
                        },
                        Name = item.Origin.Name
                    },
                    FlightNumber = item.FlightNumber
                });
            }

            Dictionary<string, List<FlightViewModel>> flights = new Dictionary<string, List<FlightViewModel>>()
            {
                { "Going", going }
            };

            //create a viewModel for each return flight option
            if (isReturning)
            {
                List<FlightViewModel> returning = new List<FlightViewModel>();

                foreach (var item in returningFlights)
                {
                    returning.Add(new FlightViewModel()
                    {
                        Arrival = item.Arrival,
                        Departure = item.Departure,
                        Id = item.Id,
                        Price = item.Price,
                        Destination = new AirportViewModel()
                        {
                            IATA = item.Destination.IATA,
                            City = new CityViewModel()
                            {
                                Name = item.Destination.City.Name
                            },
                            Name = item.Destination.Name
                        },
                        Origin = new AirportViewModel()
                        {
                            IATA = item.Origin.IATA,
                            City = new CityViewModel()
                            {
                                Name = item.Origin.City.Name
                            },
                            Name = item.Origin.Name
                        },
                        FlightNumber = item.FlightNumber
                    });
                }

                flights.Add("Returning", returning);
            }

            List<PassengerViewModel> passengers = new List<PassengerViewModel>();

            //get all possible services
            List<Service> services = new List<Service>()
            {
                new AirportCheckIn(),
                new Flexibility(),
                new OnTimeArrival(),
                new Priority(),
                new Seat()
            };

            //assign fontAwesome icnos to each service
            Dictionary<string, string> servicesURLs = new Dictionary<string, string>()
            {
                { "AirportCheckIn", "<i class=\"fa-solid fa-plane-circle-check\"></i>" },
                { "Flexibility", "<i class=\"fa-solid fa-shuffle\"></i>" },
                { "OnTimeArrival", "<i class=\"fa-solid fa-clock\"></i>" },
                { "Priority", "<i class=\"fa-solid fa-van-shuttle\"></i>" },
                { "Seat", "<i class=\"fa-solid fa-chair\"></i>" }
            };

            //generate viewModel for each passenger and service
            for (int i = 0; i < model.Passengers; i++)
            {
                var passenger = new PassengerViewModel()
                {
                    Services = new List<ServiceViewModel>()
                };

                foreach (var service in services)
                {
                    passenger.Services.Add(new ServiceViewModel
                    {
                        IsChecked = false,
                        Name = service.Name,
                        Price = service.Price,
                        URL = servicesURLs[service.Name]
                    });
                }

                passengers.Add(passenger);
            }

            //generate the final viewModel
            CreateBookingViewModel viewModel = new CreateBookingViewModel()
            {
                PassengersCount = model.Passengers,
                Flights = flights,
                Passengers = passengers
            };

            //return the View with the model
            return View(viewModel);
        }
    }
}