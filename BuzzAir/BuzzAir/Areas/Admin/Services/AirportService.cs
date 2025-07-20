namespace BuzzAir.Areas.Admin.Services
{
    public class AirportService(IAirportRepository airportRepository) : IAirportService
    {
        public async Task<PaginatedList<AirportDTO>> AllAsync(int? pageNumber, CancellationToken token)
        {
            long count = await airportRepository.GetCountAsync(token);

            List<Airport> airports = await airportRepository.AllAsync(pageNumber, GlobalConstants.ItemsPerPage, AirportEnum.All, token);
            PaginatedList<AirportDTO> paginatedList = AirportFactory.GetPaginatedList(pageNumber ?? 0, count, airports);

            return paginatedList;
        }

        public async Task<PaginatedList<AirportDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token)
        {
            long count = await airportRepository.GetDeletedCountAsync(token);

            List<Airport> airports = await airportRepository.AllDeletedAsync(pageNumber, GlobalConstants.ItemsPerPage, AirportEnum.All, token);
            PaginatedList<AirportDTO> paginatedList = AirportFactory.GetPaginatedList(pageNumber ?? 0, count, airports);

            return paginatedList;
        }

        public async Task CreateAsync(CreateAirportVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifyStringValue(model.ICAO);
            VerifyStringValue(model.Name);
            VerifyStringValue(model.CityId);
            VerifyStringValue(model.CountryId);
            VerifyStringValue(model.TimezoneId);

            Airport airport = AirportFactory.Create(model);
            await airportRepository.CreateAsync(airport, token);
        }

        public async Task DeleteAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await airportRepository.DeleteAsync(id, AirportEnum.None, token);
        }

        public async Task EditAsync(EditAirportVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifyStringValue(model.ICAO);
            VerifyStringValue(model.Name);
            VerifyStringValue(model.CityId);
            VerifyStringValue(model.CountryId);
            VerifyStringValue(model.TimezoneId);
            VerifyStringValue(model.Id);

            Airport airport = await airportRepository.GetByIdAsync(model.Id, AirportEnum.None, token);
            bool canChangeLocation = await airportRepository.CanChangeLocationAsync(airport.Id, token);

            AirportFactory.Update(airport, model, canChangeLocation);
            await airportRepository.EditAsync(airport, token);
        }

        public async Task<DeleteAirportVM> GetDeleteDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Airport airport = await airportRepository.GetByIdAsync(id, AirportEnum.All, token);
            DeleteAirportVM model = AirportFactory.GetDeleteViewModel(airport);

            return model;
        }

        public async Task<EditAirportVM> GetEditDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Airport airport = await airportRepository.GetByIdAsync(id, AirportEnum.All, token);
            EditAirportVM model = AirportFactory.GetEditViewModel(airport);

            return model;
        }

        public async Task<RestoreAirportVM> GetRestoreDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Airport airport = await airportRepository.GetDeletedByIdAsync(id, AirportEnum.None, token);
            RestoreAirportVM model = AirportFactory.GetRestoreViewModel(airport);

            return model;
        }

        public async Task RestoreAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await airportRepository.RestoreAsync(id, AirportEnum.None, token);
        }

        private static void VerifyStringValue(string value) =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
    }
}
