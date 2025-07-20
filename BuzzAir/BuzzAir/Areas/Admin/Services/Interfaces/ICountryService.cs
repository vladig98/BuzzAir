namespace BuzzAir.Areas.Admin.Services.Interfaces
{
    public interface ICountryService
    {
        Task<PaginatedList<CountryDTO>> AllAsync(int? pageNumber, CancellationToken token);
        Task<PaginatedList<CountryDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token);
        Task CreateAsync(CreateCountryVM model, CancellationToken token);
        Task DeleteAsync(string id, CancellationToken token);
        Task EditAsync(EditCountryVM model, CancellationToken token);
        Task<Country> GetByIdAsync(string id, CancellationToken token);
        Task<List<SelectListItem>> GetCountriesForSelect(CancellationToken token);
        Task<DeleteCountryVM> GetDeleteDetailsAsync(string id, CancellationToken token);
        Task<EditCountryVM> GetEditDetailsAsync(string id, CancellationToken token);
        Task<RestoreCountryVM> GetRestoreDetailsAsync(string id, CancellationToken token);
        Task RestoreAsync(string id, CancellationToken token);
    }
}
