namespace BuzzAir.Areas.Admin.Services
{
    public class CountryService(
        ICountryRepository countryRepository,
        ICachingService cachingService) : ICountryService
    {
        public async Task<PaginatedList<CountryDTO>> AllAsync(int? pageNumber, CancellationToken token)
        {
            long count = await countryRepository.GetCountAsync(token);

            List<Country> countries = await GetAllCountriesAsync(pageNumber, GlobalConstants.ItemsPerPage, token);
            PaginatedList<CountryDTO> paginatedList = CountryFactory.GetPaginatedList(pageNumber ?? 0, count, countries);

            return paginatedList;
        }

        public async Task<PaginatedList<CountryDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token)
        {
            long count = await countryRepository.GetDeletedCountAsync(token);

            List<Country> countries = await GetAllCountriesAsync(pageNumber, GlobalConstants.ItemsPerPage, deleted: true, token);
            PaginatedList<CountryDTO> paginatedList = CountryFactory.GetPaginatedList(pageNumber ?? 0, count, countries);

            return paginatedList;
        }

        public async Task CreateAsync(CreateCountryVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifyStringValue(model.ISO);
            VerifyStringValue(model.Name);

            Country country = CountryFactory.Create(model);
            await countryRepository.CreateAsync(country, token);
        }

        public async Task DeleteAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await countryRepository.DeleteAsync(id, token);
        }

        public async Task EditAsync(EditCountryVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifyStringValue(model.ISO);
            VerifyStringValue(model.Name);
            VerifyStringValue(model.Id);

            Country country = await GetCountryByIdAsync(model.Id, token);

            CountryFactory.Update(country, model);
            await countryRepository.EditAsync(country, token);
        }

        public Task<Country> GetByIdAsync(string id, CancellationToken token) =>
            GetCountryByIdAsync(id, token);

        public async Task<List<SelectListItem>> GetCountriesForSelect(CancellationToken token)
        {
            List<Country> countries = await GetAllCountriesAsync(deleted: false, token: token);
            List<SelectListItem> countriesSelect = CountryFactory.GetCountriesAsSelectItems(countries);

            return countriesSelect;
        }

        public async Task<DeleteCountryVM> GetDeleteDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Country country = await GetCountryByIdAsync(id, token);
            DeleteCountryVM model = CountryFactory.GetDeleteViewModel(country);

            return model;
        }

        public async Task<EditCountryVM> GetEditDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Country country = await GetCountryByIdAsync(id, token);
            EditCountryVM model = CountryFactory.GetEditViewModel(country);

            return model;
        }

        public async Task<RestoreCountryVM> GetRestoreDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Country country = await GetCountryByIdAsync(id, deleted: true, token);
            RestoreCountryVM model = CountryFactory.GetRestoreViewModel(country);

            return model;
        }

        public async Task RestoreAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await countryRepository.RestoreAsync(id, token);
        }

        private Task<List<Country>> GetAllCountriesAsync(
            int? pageNumber = null,
            int? itemPerPage = null,
            CancellationToken token = default) =>
            GetAllCountriesAsync(pageNumber, itemPerPage, deleted: false, token);

        private Task<List<Country>> GetAllCountriesAsync(
            int? pageNumber = null,
            int? itemPerPage = null,
            bool deleted = false,
            CancellationToken token = default)
        {
            async Task<List<Country>> dbFunc(CancellationToken ct) { return await countryRepository.AllAsync(pageNumber, itemPerPage, ct); }
            async Task<List<Country>> dbFuncDeleted(CancellationToken ct) { return await countryRepository.AllDeletedAsync(pageNumber, itemPerPage, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(GlobalConstants.COUNTRIES_DELETED_CACHE_KEY, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(GlobalConstants.COUNTRIES_CACHE_KEY, dbFunc, token);
        }

        private Task<Country> GetCountryByIdAsync(string id, CancellationToken token = default) =>
            GetCountryByIdAsync(id, deleted: false, token);

        private Task<Country> GetCountryByIdAsync(string id, bool deleted = false, CancellationToken token = default)
        {
            string cacheKey = string.Format(GlobalConstants.COUNTRY_CACHE_KEY, id);
            async Task<Country> dbFunc(CancellationToken ct) { return await countryRepository.GetByIdAsync(id, ct); }
            async Task<Country> dbFuncDeleted(CancellationToken ct) { return await countryRepository.GetDeletedByIdAsync(id, ct); }

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
