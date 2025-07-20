using BuzzAir.Areas.Admin.ViewModels.AircraftDTOs;

namespace BuzzAir.Areas.Admin.Services.Interfaces
{
    public interface IAircraftService
    {
        Task CreateAsync(CreateAircraftVM model, CancellationToken token);
        Task<EditAircraftVM> GetEditDetailsAsync(string id, CancellationToken token);
        Task EditAsync(EditAircraftVM model, CancellationToken token);
        Task<DeleteAircraftVM> GetDeleteDetailsAsync(string id, CancellationToken token);
        Task DeleteAsync(string id, CancellationToken token);
        Task<RestoreAircraftVM> GetRestoreDetailsAsync(string aircraftId, CancellationToken token);
        Task RestoreAsync(string id, CancellationToken token);
        Task<PaginatedList<AircraftDTO>> AllAsync(int? pageNumber, CancellationToken token);
        Task<PaginatedList<AircraftDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token);

        // TO DO: Review this two
        Task<List<SelectListItem>> GetAircraftForSelect(CancellationToken token);
        Task<Aircraft> GetByIdAsync(string id, CancellationToken token);
    }
}
