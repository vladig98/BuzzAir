namespace BuzzAir.Areas.Admin.Services.Interfaces
{
    public interface ICityService
    {
        Task CreateAsync(CreateCityVM model, CancellationToken token);
        Task<PaginatedList<CityDTO>> AllAsync(int? pageNumber, CancellationToken token);
        Task<PaginatedList<CityDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token);
        Task DeleteAsync(string id, CancellationToken token);
        Task EditAsync(EditCityVM model, CancellationToken token);
        Task<DeleteCityVM> GetDeleteDetailsAsync(string id, CancellationToken token);
        Task<EditCityVM> GetEditDetailsAsync(string id, CancellationToken token);
        Task<RestoreCityVM> GetRestoreDetailsAsync(string id, CancellationToken token);
        Task RestoreAsync(string id, CancellationToken token);
        Task<City> GetByIdAsync(string cityId, CancellationToken token);
    }
}
