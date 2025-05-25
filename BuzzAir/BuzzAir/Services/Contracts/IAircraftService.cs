namespace BuzzAir.Services.Contracts
{
    public interface IAircraftService
    {
        Task CreateAsync(string name, int numberOFSeats);
        Task EditAsync(string id, string name, int numberOFSeats);
        Task DeleteAsync(string id);
        Task<PaginatedList<Aircraft>> GetAllAsPaginatedListAsync(int pageSize, int? pageNumber);
        Task<AircraftEditViewModel> GetEditModelAsync(string aircraftId);
    }
}
