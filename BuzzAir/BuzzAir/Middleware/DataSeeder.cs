using BuzzAir.Models.JSONModels;

namespace BuzzAir.Middleware
{
    public class DataSeeder
    {
        private readonly RequestDelegate _next;

        public DataSeeder(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, BuzzAirDbContext _context)
        {
            try
            {
                //GenerateJSON(_context);

                if (!_context.Countries.Any())
                {
                    await SeedCountries(_context);
                }

                if (!_context.States.Any())
                {
                    await SeedStates(_context);
                }

                if (!_context.Cities.Any())
                {
                    await SeedCities(_context);
                }

                if (!_context.Timezones.Any())
                {
                    await SeedTimezones(_context);
                }

                if (!_context.Airports.Any())
                {
                    await SeedAirports(_context);
                }

                if (!_context.Aircrafts.Any())
                {
                    await SeedAircraft(_context);
                }

                if (!_context.Flights.Any())
                {
                    await SeedFlights(_context);
                }

                await _next(context);
            }
            catch (Exception)
            {
            }
        }

        private async Task SeedFlights(BuzzAirDbContext _context)
        {
            Random rnd = new Random();

            List<Flight> flights = new List<Flight>();

            for (int i = 0; i < 10000; i++)
            {
                DateTime departure = DateTime.Now.AddDays(rnd.Next(366));
                int duration = rnd.Next(30, 1441);

                Aircraft aircraft = _context.Aircrafts.ToList().ElementAt(rnd.Next(0, _context.Aircrafts.Count()));
                Airport origin = _context.Airports.ToList().ElementAt(rnd.Next(0, _context.Airports.Count()));

                Airport destination = _context.Airports.ToList().ElementAt(rnd.Next(0, _context.Airports.Count()));

                while (origin.Id == destination.Id)
                {
                    destination = _context.Airports.ToList().ElementAt(rnd.Next(0, _context.Airports.Count()));
                }

                string flightNumber = string.Format("FN-{0}{1}{2}", rnd.Next(0, 10), rnd.Next(0, 10), rnd.Next(0, 10));
                int lastDigit = rnd.Next(0, 9);

                var goingFlight = new Flight()
                {
                    Aircraft = aircraft,
                    AircraftId = aircraft.Id,
                    Arrival = departure.AddMinutes(duration),
                    Destination = destination,
                    DestinationId = destination.Id,
                    DurationInMinutes = duration,
                    FlightNumber = string.Format("{0}{1}", flightNumber, lastDigit),
                    Id = Guid.NewGuid().ToString(),
                    Origin = origin,
                    OriginId = origin.Id,
                    Price = (decimal)(rnd.NextDouble() * (500 - 9.99) + 9.99),
                    Departure = departure
                };

                for (int j = 1; j <= aircraft.NumberOfSeats; j++)
                {
                    goingFlight.Seats.Add(new FlightSeat()
                    {
                        Flight = goingFlight,
                        FlightId = goingFlight.Id,
                        Id = Guid.NewGuid().ToString(),
                        IsAvailable = true,
                        SeatNumber = j
                    });
                }

                flights.Add(goingFlight);

                if (rnd.Next(0, 2) != 0)
                {
                    DateTime newDeparture = departure.AddDays(rnd.Next(2, 15));

                    var returnFlight = new Flight()
                    {
                        Aircraft = aircraft,
                        AircraftId = aircraft.Id,
                        Arrival = newDeparture.AddMinutes(duration),
                        Destination = origin,
                        DestinationId = origin.Id,
                        DurationInMinutes = duration,
                        FlightNumber = string.Format("{0}{1}", flightNumber, lastDigit + 1),
                        Id = Guid.NewGuid().ToString(),
                        Origin = destination,
                        OriginId = destination.Id,
                        Price = (decimal)(rnd.NextDouble() * (500 - 9.99) + 9.99),
                        Departure = newDeparture
                    };

                    for (int j = 1; j <= aircraft.NumberOfSeats; j++)
                    {
                        returnFlight.Seats.Add(new FlightSeat()
                        {
                            Flight = returnFlight,
                            FlightId = returnFlight.Id,
                            Id = Guid.NewGuid().ToString(),
                            IsAvailable = true,
                            SeatNumber = j
                        });
                    }

                    flights.Add(returnFlight);
                }
            }

            await _context.Flights.AddRangeAsync(flights);
            await _context.SaveChangesAsync();
        }

        private async Task SeedStates(BuzzAirDbContext _context)
        {
            string json = File.ReadAllText("Data/cities.json");
            List<CityModel> citiesJson = JsonConvert.DeserializeObject<List<CityModel>>(json);

            List<State> statesToAdd = new List<State>();

            foreach (var city in citiesJson)
            {
                if (city.state == "NA")
                {
                    continue;
                }

                if (statesToAdd.Any(x => x.Name == city.state && x.Country.Name == city.country))
                {
                    continue;
                }

                Country country = _context.Countries.FirstOrDefault(x => x.Name == city.country);

                State state = new State
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = city.state,
                    Country = country,
                    CountryId = country.Id
                };

                statesToAdd.Add(state);
            }

            await _context.States.AddRangeAsync(statesToAdd);
            await _context.SaveChangesAsync();
        }

        private async Task SeedCities(BuzzAirDbContext _context)
        {
            string json = File.ReadAllText("Data/cities.json");
            List<CityModel> citiesJson = JsonConvert.DeserializeObject<List<CityModel>>(json);

            List<City> citiesToAdd = new List<City>();

            foreach (var city in citiesJson)
            {
                if (city.city == "NA")
                {
                    continue;
                }

                if (citiesToAdd.Any(x => x.Name == city.city && x.State?.Name == city.state))
                {
                    continue;
                }

                Country country = _context.Countries.FirstOrDefault(x => x.Name == city.country);
                State state = _context.States.FirstOrDefault(x => x.Name == city.state);

                City c = new City
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = city.city,
                    State = state,
                    Country = country
                };

                citiesToAdd.Add(c);
            }

            await _context.Cities.AddRangeAsync(citiesToAdd);
            await _context.SaveChangesAsync();
        }

        private class CityState
        {
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
        }

        private void GenerateJSON(BuzzAirDbContext _context)
        {
            string json = File.ReadAllText("Data/airports.json");
            List<Dictionary<string, AirportModel>> airportsJson = JsonConvert.DeserializeObject<List<Dictionary<string, AirportModel>>>(json);

            Dictionary<string, AirportModel> airports = airportsJson[0].Where(x => !string.IsNullOrWhiteSpace(x.Value.iata) && !string.IsNullOrEmpty(x.Value.iata))
                .ToDictionary(x => x.Key, x => x.Value);

            List<CityState> cities = new List<CityState>();

            foreach (KeyValuePair<string, AirportModel> pair in airports)
            {
                AirportModel model = pair.Value;

                Country country = _context.Countries.Where(x => x.ISO == model.country).Count() == 1 ?
                    _context.Countries.FirstOrDefault(x => x.ISO == model.country) : _context.Countries.FirstOrDefault(x => x.ISO == model.country && x.IsCountry);

                if (country == null)
                {
                    country = _context.Countries.FirstOrDefault(x => x.ISO == model.country);
                }

                cities.Add(new CityState
                {
                    city = string.IsNullOrEmpty(model.city) ? "NA" : model.city,
                    state = string.IsNullOrEmpty(model.state) ? "NA" : model.state,
                    country = country.Name
                });
            }

            string serializedJson = JsonConvert.SerializeObject(cities, Formatting.Indented);

            File.WriteAllText("Data/cities.json", serializedJson);
        }

        private async Task SeedCountries(BuzzAirDbContext _context)
        {
            string json = File.ReadAllText("Data/countries.json");
            List<CountryModel> countriesJson = JsonConvert.DeserializeObject<List<CountryModel>>(json);

            foreach (var country in countriesJson)
            {
                Country c = new Country
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = country.country,
                    ISO = country.iso,
                    IsCountry = country.isCountry
                };

                _context.Countries.Add(c);
            }

            await _context.SaveChangesAsync();
        }

        private async Task SeedTimezones(BuzzAirDbContext _context)
        {
            string json = File.ReadAllText("Data/timezones.json");
            List<TimezoneModel> timezonesJson = JsonConvert.DeserializeObject<List<TimezoneModel>>(json);

            foreach (var timezone in timezonesJson)
            {
                Timezone time = new Timezone
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = timezone.timezone
                };

                _context.Timezones.Add(time);
            }

            await _context.SaveChangesAsync();
        }

        private async Task SeedAircraft(BuzzAirDbContext _context)
        {
            string json = File.ReadAllText("Data/aircraft.json");
            List<AircraftModel> aircraftJson = JsonConvert.DeserializeObject<List<AircraftModel>>(json);

            foreach (var aircraft in aircraftJson)
            {
                Aircraft air = new Aircraft
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = aircraft.name,
                    NumberOfSeats = aircraft.seats
                };

                _context.Aircrafts.Add(air);
            }

            await _context.SaveChangesAsync();
        }

        private async Task SeedAirports(BuzzAirDbContext _context)
        {
            string json = File.ReadAllText("Data/airports.json");
            List<Dictionary<string, AirportModel>> airportsJson = JsonConvert.DeserializeObject<List<Dictionary<string, AirportModel>>>(json);

            Dictionary<string, AirportModel> airports = airportsJson[0].Where(x => !string.IsNullOrWhiteSpace(x.Value.iata) && !string.IsNullOrEmpty(x.Value.iata))
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (KeyValuePair<string, AirportModel> pair in airports)
            {
                AirportModel model = pair.Value;

                Country country = _context.Countries.Where(x => x.ISO == model.country).Count() == 1 ?
                    _context.Countries.FirstOrDefault(x => x.ISO == model.country) : _context.Countries.FirstOrDefault(x => x.ISO == model.country && x.IsCountry);

                City city = _context.Cities.FirstOrDefault(x => x.Name == model.city);

                State state = _context.States.FirstOrDefault(x => x.Name == model.state);

                if (city == null)
                {
                    continue;
                }

                if (state == null)
                {
                    continue;
                }

                if (country == null)
                {
                    country = _context.Countries.FirstOrDefault(x => x.ISO == model.country);
                }

                Airport airport = new Airport()
                {
                    City = city,
                    Country = country,
                    Elevation = model.elevation,
                    IATA = model.iata,
                    ICAO = model.icao,
                    Id = Guid.NewGuid().ToString(),
                    Latitude = model.lat,
                    Longitude = model.lon,
                    Name = model.name,
                    State = state,
                    TimeZone = model.tz,
                    CountryId = country.Id
                };

                _context.Airports.Add(airport);
            }

            await _context.SaveChangesAsync();
        }
    }
}
