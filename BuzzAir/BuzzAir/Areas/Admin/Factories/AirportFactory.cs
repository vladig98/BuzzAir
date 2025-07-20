namespace BuzzAir.Areas.Admin.Factories
{
    public static class AirportFactory
    {
        public static PaginatedList<AirportDTO> GetPaginatedList(int pageNumber, long count, List<Airport> airports)
        {
            List<AirportDTO> dtos = MapModelToDTO(airports);
            PaginatedList<AirportDTO> paginatedList = new(dtos, count, pageNumber, GlobalConstants.ItemsPerPage);

            return paginatedList;
        }

        public static Airport Create(CreateAirportVM model)
        {
            Airport airport = new()
            {
                Elevation = model.Elevation,
                Name = model.Name,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                ICAO = model.ICAO,
                IATA = model.IATA,
                City = model.City!,
                State = model.State,
                Country = model.Country!,
                Timezone = model.Timezone!
            };

            return airport;
        }

        public static DeleteAirportVM GetDeleteViewModel(Airport airport)
        {
            DeleteAirportVM deleteViewModel = new()
            {
                City = airport.City.Name,
                Country = airport.Country.Name,
                IATA = airport.IATA,
                Elevation = airport.Elevation,
                Name = airport.Name,
                ICAO = airport.ICAO,
                Id = airport.Id,
                Latitude = airport.Latitude,
                Longitude = airport.Longitude,
                State = airport.State?.Name ?? string.Empty,
                Timezone = airport.Timezone
            };

            return deleteViewModel;
        }

        public static EditAirportVM GetEditViewModel(Airport airport)
        {
            EditAirportVM viewModel = new()
            {
                CityName = airport.City.Name,
                CityId = airport.CityId,
                CountryName = airport.Country.Name,
                CountryId = airport.CountryId,
                IATA = airport.IATA,
                Elevation = airport.Elevation,
                Name = airport.Name,
                ICAO = airport.ICAO,
                Id = airport.Id,
                Latitude = airport.Latitude,
                Longitude = airport.Longitude,
                StateName = airport.State?.Name ?? string.Empty,
                TimezoneName = airport.Timezone,
                TimezoneId = airport.TimezoneId
            };

            return viewModel;
        }

        public static RestoreAirportVM GetRestoreViewModel(Airport airport)
        {
            RestoreAirportVM viewModel = new()
            {
                City = airport.City.Name,
                Country = airport.Country.Name,
                IATA = airport.IATA,
                Elevation = airport.Elevation,
                Name = airport.Name,
                ICAO = airport.ICAO,
                Id = airport.Id,
                Latitude = airport.Latitude,
                Longitude = airport.Longitude,
                State = airport.State?.Name ?? string.Empty,
                Timezone = airport.Timezone
            };

            return viewModel;
        }

        public static void Update(Airport airport, EditAirportVM model, bool canChangeLocation)
        {
            airport.IATA = model.IATA;
            airport.ICAO = model.ICAO;
            airport.Name = model.Name;
            airport.Timezone = model.Timezone!;

            if (!canChangeLocation)
            {
                return;
            }

            airport.Elevation = model.Elevation;
            airport.Latitude = model.Latitude;
            airport.Longitude = model.Longitude;

            airport.City = model.City!;
            airport.Country = model.Country!;
            airport.State = model.State;
        }

        public static CreateAirportVM InitializeCreateAirportViewModel(List<SelectListItem> countries)
        {
            CreateAirportVM model = new()
            {
                TimezoneOptions = GetTimeZonesAsSelect(),
                CountryOptions = countries
            };

            return model;
        }

        public static void UpdateEditViewModelWithSelects(EditAirportVM model, List<SelectListItem> countries)
        {
            model.CountryOptions = countries;
            model.TimezoneOptions = GetTimeZonesAsSelect();
        }

        private static List<AirportDTO> MapModelToDTO(List<Airport> airports)
        {
            List<AirportDTO> dtos = new(airports.Count);

            foreach (Airport airport in airports)
            {
                AirportDTO dto = new(
                    airport.Id,
                    airport.ICAO,
                    airport.IATA,
                    airport.Name,
                    airport.City.Name,
                    airport.State?.Name ?? string.Empty,
                    airport.Country.Name,
                    airport.Elevation,
                    airport.Latitude,
                    airport.Longitude,
                    airport.Timezone);

                dtos.Add(dto);
            }

            return dtos;
        }

        private static List<SelectListItem> GetTimeZonesAsSelect()
        {
            Dictionary<string, string> timezones = TimeZoneInfo.GetSystemTimeZones().ToDictionary(x => x.Id, x => x.DaylightName);
            List<SelectListItem> listItems = [];

            foreach (KeyValuePair<string, string> timezone in timezones)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = timezone.Value,
                    Value = timezone.Key
                });
            }

            return listItems;
        }
    }
}
