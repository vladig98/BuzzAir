using BuzzAir.Models.DTOs;

namespace BuzzAir.Factories
{
    public static class FlightFactory
    {
        public static FlightViewModel CreateViewModel(BookingFlight flight)
        {
            FlightViewModel viewModel = CreateViewModel(flight.Flight);
            viewModel.IsOutbound = flight.IsOutbound;

            return viewModel;
        }

        public static FlightViewModel CreateViewModel(Flight flight)
        {
            AirportViewModel destination = AirportFactory.GetEditViewModel(flight.Destination);
            AirportViewModel origin = AirportFactory.GetEditViewModel(flight.Origin);

            FlightViewModel viewModel = new()
            {
                Id = flight.Id,
                Arrival = flight.Arrival,
                Departure = flight.Departure,
                Destination = destination,
                FlightNumber = flight.FlightNumber,
                Origin = origin,
                Price = flight.Price
            };

            return viewModel;
        }

        internal static Flight Create(Airport origin, Airport destination, Aircraft aircraft, CreateFlightViewModel model)
        {
            DateTime arrival = model.Departure.AddMinutes(model.DurationInMinutes);

            Flight flight = new()
            {
                Aircraft = aircraft,
                Arrival = arrival,
                Departure = model.Departure,
                Destination = destination,
                DurationInMinutes = model.DurationInMinutes,
                FlightNumber = model.FlightNumber,
                Origin = origin,
                Price = model.Price,
                AircraftId = aircraft.Id,
                OriginId = origin.Id,
                DestinationId = destination.Id
            };

            return flight;
        }

        internal static FlightEditViewModel CreateEditViewModel(Flight flight, List<SelectListItem> aircraft, List<SelectListItem> countries,
            List<SelectListItem> originAirportsSelect, List<SelectListItem> destinationAirportsSelect)
        {
            FlightEditViewModel viewModel = new()
            {
                Departure = flight.Departure,
                Destination = flight.DestinationId,
                DestinationCountry = flight.Destination.CountryId,
                FlightNumber = flight.FlightNumber,
                Origin = flight.OriginId,
                OriginCountry = flight.Origin.CountryId,
                Price = flight.Price,
                Aircraft = flight.AircraftId,
                DurationInMinutes = flight.DurationInMinutes,
                AircraftOptions = aircraft,
                OriginOptions = countries,
                DestinationOptions = [.. countries],
                DestinationAirportsOptions = originAirportsSelect,
                OriginAirportsOptions = destinationAirportsSelect,
                Id = flight.Id
            };

            return viewModel;
        }

        internal static BookingFlight CreateFlightForBooking(Booking booking, Flight flight, bool isOutbound)
        {
            BookingFlight bookingFlight = new()
            {
                Flight = flight,
                FlightId = flight.Id,
                Booking = booking,
                BookingId = booking.Id,
                IsOutbound = isOutbound
            };

            return bookingFlight;
        }

        internal static CreateFlightViewModel CreateViewModelForCreation(List<SelectListItem> aircraft, List<SelectListItem> countries)
        {
            CreateFlightViewModel viewModel = new()
            {
                Aircrafts = aircraft,
                OriginCountryOptions = countries,
                DestinationCountryOptions = [.. countries]
            };

            return viewModel;
        }

        internal static List<SelectListItem> GetFlightsForSelect(List<FlightInfo> flights)
        {
            List<SelectListItem> list = [];
            Dictionary<string, SelectListGroup> groups = [];
            HashSet<string> cities = [];

            foreach (FlightInfo flight in flights)
            {
                string countryName = flight.CountryName;
                string cityName = flight.CityName;
                string cityId = flight.CityId;

                if (cities.Contains(cityName))
                {
                    continue;
                }

                cities.Add(cityName);

                if (!groups.TryGetValue(countryName, out SelectListGroup? group))
                {
                    group = new SelectListGroup()
                    {
                        Name = countryName
                    };

                    groups.Add(countryName, group);
                }

                SelectListItem flightItem = new()
                {
                    Text = cityName,
                    Value = cityId,
                    Group = group
                };

                list.Add(flightItem);
            }

            return list;
        }
    }
}
