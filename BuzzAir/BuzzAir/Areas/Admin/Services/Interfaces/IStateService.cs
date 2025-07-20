namespace BuzzAir.Areas.Admin.Services.Interfaces
{
    public interface IStateService
    {
        Task<PaginatedList<StateDTO>> AllAsync(int? pageNumber, CancellationToken token);
        Task<PaginatedList<StateDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token);
        Task CreateAsync(CreateStateVM model, CancellationToken token);
        Task DeleteAsync(string id, CancellationToken token);
        Task EditAsync(EditStateVM model, CancellationToken token);
        Task<State?> GetByIdAsync(string id, CancellationToken token);
        Task<DeleteStateVM> GetDeleteDetailsAsync(string id, CancellationToken token);
        Task<EditStateVM> GetEditDetailsAsync(string id, CancellationToken token);
        Task<RestoreStateVM> GetRestoreDetailsAsync(string id, CancellationToken token);
        Task<List<SelectListItem>> GetStatesForSelectAsync(CancellationToken token);
        Task RestoreAsync(string id, CancellationToken token);
    }
}
