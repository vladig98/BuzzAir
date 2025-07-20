namespace BuzzAir.Areas.Admin.Services
{
    public class CityService(ICityRepository cityRepository) : ICityService
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

            List<City> cities = await cityRepository.AllAsync(pageNumber, GlobalConstants.ItemsPerPage, CityEnum.All, token);
            PaginatedList<CityDTO> paginatedList = CityFactory.GetPaginatedList(pageNumber ?? 0, count, cities);

            return paginatedList;
        }

        public async Task<PaginatedList<CityDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token)
        {
            long count = await cityRepository.GetDeletedCountAsync(token);

            List<City> cities = await cityRepository.AllDeletedAsync(pageNumber, GlobalConstants.ItemsPerPage, CityEnum.All, token);
            PaginatedList<CityDTO> paginatedList = CityFactory.GetPaginatedList(pageNumber ?? 0, count, cities);

            return paginatedList;
        }

        public async Task DeleteAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await cityRepository.DeleteAsync(id, CityEnum.None, token);
        }

        public Task<City> GetByIdAsync(string cityId, CancellationToken token) =>
            cityRepository.GetByIdAsync(cityId, CityEnum.All, token);

        public async Task EditAsync(EditCityVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifyStringValue(model.Name);
            VerifyStringValue(model.CountryId);
            VerifyStringValue(model.Id);

            City city = await cityRepository.GetByIdAsync(model.Id, CityEnum.None, token);
            bool canChangeLocation = await cityRepository.CanChangeLocationAsync(model.Id, token);

            CityFactory.Update(city, model, canChangeLocation);
            await cityRepository.EditAsync(city, token);
        }

        public async Task<DeleteCityVM> GetDeleteDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            City city = await cityRepository.GetByIdAsync(id, CityEnum.All, token);
            DeleteCityVM model = CityFactory.GetDeleteViewModel(city);

            return model;
        }

        public async Task<EditCityVM> GetEditDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            City city = await cityRepository.GetByIdAsync(id, CityEnum.All, token);
            EditCityVM model = CityFactory.GetEditViewModel(city);

            return model;
        }

        public async Task<RestoreCityVM> GetRestoreDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            City city = await cityRepository.GetDeletedByIdAsync(id, CityEnum.None, token);
            RestoreCityVM model = CityFactory.GetRestoreViewModel(city);

            return model;
        }

        public async Task RestoreAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await cityRepository.RestoreAsync(id, CityEnum.None, token);
        }

        private static void VerifyStringValue(string value) =>
            string.IsNullOrWhiteSpace(value);
    }
}
