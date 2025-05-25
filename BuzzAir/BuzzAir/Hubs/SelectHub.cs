using Microsoft.AspNetCore.SignalR;
using System.Text;

namespace BuzzAir.Hubs
{
    public class SelectHub : Hub
    {
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;
        private readonly IAirportService _airportService;
        private readonly IFlightsService _flightsService;

        public SelectHub(ICityService cityService, IStateService stateService, IAirportService airportService, IFlightsService flightsService)
        {
            _cityService = cityService;
            _stateService = stateService;
            _airportService = airportService;
            _flightsService = flightsService;
        }

        public async Task SelectCountry(string countryId)
        {
            var states = await _stateService.GetAllAsync();

            states = states.Where(x => x.CountryId == countryId).OrderBy(x => x.Name).ToList();

            await Clients.All.SendAsync("CountrySelected", states);
        }

        public async Task SelectState(string stateId)
        {
            var cities = await _cityService.GetAllAsync();

            cities = cities.Where(x => x.State?.Id == stateId).OrderBy(x => x.Name).ToList();

            await Clients.All.SendAsync("StateSelected", cities);
        }

        public async Task SelectCountryForFlightOrigin(string countryId)
        {
            var airports = await _airportService.GetAll();

            airports = airports.Where(x => x.CountryId == countryId).OrderBy(x => x.Name).ToList();

            await Clients.All.SendAsync("CountryFlightSelectedOrigin", airports);
        }

        public async Task SelectCountryForFlightDestination(string countryId)
        {
            var airports = await _airportService.GetAll();

            airports = airports.Where(x => x.CountryId == countryId).OrderBy(x => x.Name).ToList();

            await Clients.All.SendAsync("CountryFlightSelectedDestination", airports);
        }

        public async Task SelectDestinationsForHomePage(string cityId)
        {
            var flights = await _flightsService.GetFlightsByCityId(cityId);

            List<string> names = new List<string>();

            List<object> values = new List<object>();

            foreach (var flight in flights)
            {
                if (!names.Any(x => x == flight.Destination.City.Name))
                {
                    names.Add(flight.Destination.City.Name);

                    values.Add(new
                    {
                        Name = flight.Destination.City.Name,
                        Id = flight.Destination.City.Id,
                        Group = flight.Destination.Country.Name
                    });
                }
            }

            values = values.OrderBy(x => x.GetType().GetProperty("Group").GetValue(x, null)).ThenBy(x => x.GetType().GetProperty("Name").GetValue(x, null)).ToList();

            await Clients.All.SendAsync("DestinationsHomePageSelected", values);
        }

        public async Task GetFlightDates(string originId, string destinationId)
        {
            List<DateTime> dates = new List<DateTime>();

            var flights = await _flightsService.GetFlightsForOriginIdAndDestinationId(originId, destinationId);

            foreach (var flight in flights)
            {
                dates.Add(flight.Departure);
            }

            StringBuilder sb = new StringBuilder();

            dates = dates.OrderBy(x => x.Year).ThenBy(x => x.Month).ThenBy(x => x.Day).ToList();

            foreach (var date in dates)
            {
                sb.AppendLine($"[{date.Year}, {date.Month - 1}, {date.Day}];");
            }

            string datesResult = sb.ToString().TrimEnd(new[] { ';', '\r', '\n' });

            await Clients.All.SendAsync("FlightDatesSelected", datesResult);
        }

        public async Task GetReturnFlightDates(string originId, string destinationId)
        {
            List<DateTime> dates = new List<DateTime>();

            //getting return flights
            var flights = await _flightsService.GetFlightsForOriginIdAndDestinationId(destinationId, originId);

            foreach (var flight in flights)
            {
                dates.Add(flight.Departure);
            }

            StringBuilder sb = new StringBuilder();

            dates = dates.OrderBy(x => x.Year).ThenBy(x => x.Month).ThenBy(x => x.Day).ToList();

            foreach (var date in dates)
            {
                sb.AppendLine($"[{date.Year}, {date.Month - 1}, {date.Day}];");
            }

            string datesResult = sb.ToString().TrimEnd(new[] { ';', '\r', '\n' });

            await Clients.All.SendAsync("ReturnFlightDatesSelected", datesResult);
        }

        public async Task GetCurrencyValues(string currency, string amount)
        {
            int currencyValue = int.Parse(currency);
            Currency curr = (Currency)currencyValue;

            decimal price = decimal.Parse(amount);

            price *= decimal.Parse(curr.GetPrice());

            await Clients.All.SendAsync("CurrencyConvertionCompleted", curr.ToString(), price);
        }
    }
}
