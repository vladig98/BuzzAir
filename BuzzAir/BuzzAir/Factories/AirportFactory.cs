namespace BuzzAir.Factories
{
    public static class AirportFactory
    {
        public static AirportCreateViewModel CreateViewModelForCreation(
            List<SelectListItem> countries,
            List<SelectListItem> timezones,
            List<City> cities,
            List<State> states)
        {
            AirportCreateViewModel viewModel = new()
            {
                CountryOptions = countries,
                TimezoneOptions = timezones,
                Cities = [.. cities.OrderBy(x => x.Name)],
                States = [.. states.OrderBy(x => x.Name)]
            };

            return viewModel;
        }

        public static Airport CreateAirport(AirportCreateViewModel model, Country country, City city, State? state)
        {
            Airport airport = new()
            {
                ICAO = model.ICAO,
                IATA = model.IATA,
                Name = model.Name,
                City = city,
                State = state,
                Country = country,
                Elevation = model.Elevation,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                TimeZone = model.TimeZone
            };

            return airport;
        }

        internal static AirportViewModel GetEditViewModel(Airport airport)
        {
            CityViewModel cityViewModel = CityFactory.GetViewModel(airport.City);

            AirportViewModel model = new()
            {
                Name = airport.Name,
                City = cityViewModel,
                IATA = airport.IATA
            };

            return model;
        }

        internal static List<SelectListItem> CreateAirportForSelect(IEnumerable<Airport> airports)
        {
            List<SelectListItem> selectItems = [];

            foreach (Airport airport in airports)
            {
                SelectListItem airportItem = new()
                {
                    Text = airport.Name,
                    Value = airport.Id
                };

                selectItems.Add(airportItem);
            }

            return selectItems;
        }
    }
}
