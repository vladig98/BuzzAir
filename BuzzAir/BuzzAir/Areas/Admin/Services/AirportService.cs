namespace BuzzAir.Areas.Admin.Services
{
    public class AirportService(
        IAirportRepository airportRepository,
        ICachingService cachingService) : IAirportService
    {
        public async Task<PaginatedList<AirportDTO>> AllAsync(int? pageNumber, CancellationToken token)
        {
            long count = await airportRepository.GetCountAsync(token);

            List<Airport> airports = await GetAllAirportsAsync(pageNumber, GlobalConstants.ItemsPerPage, AirportEnum.All, token);
            PaginatedList<AirportDTO> paginatedList = AirportFactory.GetPaginatedList(pageNumber ?? 0, count, airports);

            return paginatedList;
        }

        public async Task<PaginatedList<AirportDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token)
        {
            long count = await airportRepository.GetDeletedCountAsync(token);

            List<Airport> airports = await GetAllAirportsAsync(pageNumber, GlobalConstants.ItemsPerPage, AirportEnum.All, deleted: true, token);
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

            Airport airport = await GetAirportByIdAsync(model.Id, AirportEnum.None, token);
            bool canChangeLocation = await airportRepository.CanChangeLocationAsync(airport.Id, token);

            AirportFactory.Update(airport, model, canChangeLocation);
            await airportRepository.EditAsync(airport, token);
        }

        public async Task<DeleteAirportVM> GetDeleteDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Airport airport = await GetAirportByIdAsync(id, AirportEnum.All, token);
            DeleteAirportVM model = AirportFactory.GetDeleteViewModel(airport);

            return model;
        }

        public async Task<EditAirportVM> GetEditDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Airport airport = await GetAirportByIdAsync(id, AirportEnum.All, token);
            EditAirportVM model = AirportFactory.GetEditViewModel(airport);

            return model;
        }

        public async Task<RestoreAirportVM> GetRestoreDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Airport airport = await GetAirportByIdAsync(id, AirportEnum.None, deleted: true, token);
            RestoreAirportVM model = AirportFactory.GetRestoreViewModel(airport);

            return model;
        }

        public async Task RestoreAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await airportRepository.RestoreAsync(id, AirportEnum.None, token);
        }

        private Task<List<Airport>> GetAllAirportsAsync(
            int? pageNumber = null,
            int? itemPerPage = null,
            AirportEnum include = AirportEnum.None,
            CancellationToken token = default) =>
            GetAllAirportsAsync(pageNumber, itemPerPage, include, deleted: false, token);

        private Task<List<Airport>> GetAllAirportsAsync(
            int? pageNumber = null,
            int? itemPerPage = null,
            AirportEnum include = AirportEnum.None,
            bool deleted = false,
            CancellationToken token = default)
        {
            async Task<List<Airport>> dbFunc(CancellationToken ct) { return await airportRepository.AllAsync(pageNumber, itemPerPage, include, ct); }
            async Task<List<Airport>> dbFuncDeleted(CancellationToken ct) { return await airportRepository.AllDeletedAsync(pageNumber, itemPerPage, include, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(GlobalConstants.AIRPORTS_DELETED_CACHE_KEY, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(GlobalConstants.AIRPORTS_CACHE_KEY, dbFunc, token);
        }

        private Task<Airport> GetAirportByIdAsync(string id, AirportEnum include, CancellationToken token = default) =>
            GetAirportByIdAsync(id, include, deleted: false, token);

        private Task<Airport> GetAirportByIdAsync(string id, AirportEnum include, bool deleted = false, CancellationToken token = default)
        {
            string cacheKey = string.Format(GlobalConstants.AIRPORT_CACHE_KEY, id);
            async Task<Airport> dbFunc(CancellationToken ct) { return await airportRepository.GetByIdAsync(id, include, ct); }
            async Task<Airport> dbFuncDeleted(CancellationToken ct) { return await airportRepository.GetDeletedByIdAsync(id, include, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(cacheKey, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(cacheKey, dbFunc, token);
        }

        private static void VerifyStringValue(string value) =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
    }
}
