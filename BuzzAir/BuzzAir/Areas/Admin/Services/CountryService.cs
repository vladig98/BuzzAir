namespace BuzzAir.Areas.Admin.Services
{
    public class CountryService(ICountryRepository countryRepository) : ICountryService
    {
        public async Task<PaginatedList<CountryDTO>> AllAsync(int? pageNumber, CancellationToken token)
        {
            long count = await countryRepository.GetCountAsync(token);

            List<Country> countries = await countryRepository.AllAsync(pageNumber, GlobalConstants.ItemsPerPage, token);
            PaginatedList<CountryDTO> paginatedList = CountryFactory.GetPaginatedList(pageNumber ?? 0, count, countries);

            return paginatedList;
        }

        public async Task<PaginatedList<CountryDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token)
        {
            long count = await countryRepository.GetDeletedCountAsync(token);

            List<Country> countries = await countryRepository.AllDeletedAsync(pageNumber, GlobalConstants.ItemsPerPage, token);
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

            Country country = await countryRepository.GetByIdAsync(model.Id, token);

            CountryFactory.Update(country, model);
            await countryRepository.EditAsync(country, token);
        }

        public Task<Country> GetByIdAsync(string id, CancellationToken token) =>
            countryRepository.GetByIdAsync(id, token);

        public async Task<List<SelectListItem>> GetCountriesForSelect(CancellationToken token)
        {
            List<Country> countries = await countryRepository.AllAsync(null, null, token);
            List<SelectListItem> countriesSelect = CountryFactory.GetCountriesAsSelectItems(countries);

            return countriesSelect;
        }

        public async Task<DeleteCountryVM> GetDeleteDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Country country = await countryRepository.GetByIdAsync(id, token);
            DeleteCountryVM model = CountryFactory.GetDeleteViewModel(country);

            return model;
        }

        public async Task<EditCountryVM> GetEditDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Country country = await countryRepository.GetByIdAsync(id, token);
            EditCountryVM model = CountryFactory.GetEditViewModel(country);

            return model;
        }

        public async Task<RestoreCountryVM> GetRestoreDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            Country country = await countryRepository.GetByIdAsync(id, token);
            RestoreCountryVM model = CountryFactory.GetRestoreViewModel(country);

            return model;
        }

        public async Task RestoreAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await countryRepository.RestoreAsync(id, token);
        }

        private static void VerifyStringValue(string value) =>
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
    }
}
