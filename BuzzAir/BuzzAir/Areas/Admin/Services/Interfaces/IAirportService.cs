namespace BuzzAir.Areas.Admin.Services.Interfaces
{
    public interface IAirportService
    {
        Task<PaginatedList<AirportDTO>> AllAsync(int? pageNumber, CancellationToken token);
        Task<PaginatedList<AirportDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token);
        Task CreateAsync(CreateAirportVM model, CancellationToken token);
        Task DeleteAsync(string id, CancellationToken token);
        Task EditAsync(EditAirportVM model, CancellationToken token);
        Task<DeleteAirportVM> GetDeleteDetailsAsync(string id, CancellationToken token);
        Task<EditAirportVM> GetEditDetailsAsync(string id, CancellationToken token);
        Task<RestoreAirportVM> GetRestoreDetailsAsync(string id, CancellationToken token);
        Task RestoreAsync(string id, CancellationToken token);
    }
}
