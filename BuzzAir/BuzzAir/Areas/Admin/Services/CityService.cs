namespace BuzzAir.Areas.Admin.Services
{
    public class CityService(
        ICityRepository cityRepository,
        ICachingService cachingService) : ICityService
    {
        public async Task CreateAsync(CreateCityVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifyStringValue(model.Name);

            City city = CityFactory.Create(model);
            await cityRepository.CreateAsync(city, token);
        }

        public async Task<PaginatedList<CityDTO>> AllAsync(int? pageNumber, CancellationToken token)
        {
            long count = await cityRepository.GetCountAsync(token);

            List<City> cities = await GetAllCitiesAsync(pageNumber, GlobalConstants.ItemsPerPage, CityEnum.All, token);
            PaginatedList<CityDTO> paginatedList = CityFactory.GetPaginatedList(pageNumber ?? 0, count, cities);

            return paginatedList;
        }

        public async Task<PaginatedList<CityDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token)
        {
            long count = await cityRepository.GetDeletedCountAsync(token);

            List<City> cities = await GetAllCitiesAsync(pageNumber, GlobalConstants.ItemsPerPage, CityEnum.All, deleted: true, token);
            PaginatedList<CityDTO> paginatedList = CityFactory.GetPaginatedList(pageNumber ?? 0, count, cities);

            return paginatedList;
        }

        public async Task DeleteAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await cityRepository.DeleteAsync(id, CityEnum.None, token);
        }

        public Task<City> GetByIdAsync(string cityId, CancellationToken token) =>
            GetCityByIdAsync(cityId, CityEnum.All, token);

        public async Task EditAsync(EditCityVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifyStringValue(model.Name);
            VerifyStringValue(model.CountryId);
            VerifyStringValue(model.Id);

            City city = await GetCityByIdAsync(model.Id, CityEnum.None, token); ;
            bool canChangeLocation = await cityRepository.CanChangeLocationAsync(model.Id, token);

            CityFactory.Update(city, model, canChangeLocation);
            await cityRepository.EditAsync(city, token);
        }

        public async Task<DeleteCityVM> GetDeleteDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            City city = await GetCityByIdAsync(id, CityEnum.All, token);
            DeleteCityVM model = CityFactory.GetDeleteViewModel(city);

            return model;
        }

        public async Task<EditCityVM> GetEditDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            City city = await GetCityByIdAsync(id, CityEnum.All, token);
            EditCityVM model = CityFactory.GetEditViewModel(city);

            return model;
        }

        public async Task<RestoreCityVM> GetRestoreDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            City city = await GetCityByIdAsync(id, CityEnum.All, deleted: true, token);
            RestoreCityVM model = CityFactory.GetRestoreViewModel(city);

            return model;
        }

        public async Task RestoreAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await cityRepository.RestoreAsync(id, CityEnum.None, token);
        }

        private Task<List<City>> GetAllCitiesAsync(
            int? pageNumber = null,
            int? itemPerPage = null,
            CityEnum include = CityEnum.None,
            CancellationToken token = default) =>
            GetAllCitiesAsync(pageNumber, itemPerPage, include, deleted: false, token);

        private Task<List<City>> GetAllCitiesAsync(
            int? pageNumber = null,
            int? itemPerPage = null,
            CityEnum include = CityEnum.None,
            bool deleted = false,
            CancellationToken token = default)
        {
            async Task<List<City>> dbFunc(CancellationToken ct) { return await cityRepository.AllAsync(pageNumber, itemPerPage, include, ct); }
            async Task<List<City>> dbFuncDeleted(CancellationToken ct) { return await cityRepository.AllDeletedAsync(pageNumber, itemPerPage, include, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(GlobalConstants.CITIES_DELETED_CACHE_KEY, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(GlobalConstants.CITIES_CACHE_KEY, dbFunc, token);
        }

        private Task<City> GetCityByIdAsync(string id, CityEnum include, CancellationToken token = default) =>
            GetCityByIdAsync(id, include, deleted: false, token);

        private Task<City> GetCityByIdAsync(string id, CityEnum include, bool deleted = false, CancellationToken token = default)
        {
            string cacheKey = string.Format(GlobalConstants.CITY_CACHE_KEY, id);
            async Task<City> dbFunc(CancellationToken ct) { return await cityRepository.GetByIdAsync(id, include, ct); }
            async Task<City> dbFuncDeleted(CancellationToken ct) { return await cityRepository.GetDeletedByIdAsync(id, include, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(cacheKey, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(cacheKey, dbFunc, token);
        }

        private static void VerifyStringValue(string value) =>
            string.IsNullOrWhiteSpace(value);
    }
}
