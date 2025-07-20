namespace BuzzAir.Areas.Admin.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<List<City>> AllAsync(int? pageNumber = null, int? itemsPerPage = null, CityEnum includes = CityEnum.None, CancellationToken token = default);
        Task<List<City>> AllDeletedAsync(int? pageNumber = null, int? itemsPerPage = null, CityEnum includes = CityEnum.None, CancellationToken token = default);
        Task<bool> CanChangeLocationAsync(string id, CancellationToken token);
        Task CreateAsync(City city, CancellationToken token = default);
        Task DeleteAsync(string id, CityEnum includes, CancellationToken token = default);
        Task EditAsync(City city, CancellationToken token = default);
        Task<City> GetByIdAsync(string id, CityEnum includes, CancellationToken token = default);
        Task<long> GetCountAsync(CancellationToken token = default);
        Task<City> GetDeletedByIdAsync(string id, CityEnum includes, CancellationToken token = default);
        Task<long> GetDeletedCountAsync(CancellationToken token = default);
        Task RestoreAsync(string id, CityEnum includes, CancellationToken token = default);
    }
}
