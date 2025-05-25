namespace BuzzAir.Services.Interfaces
{
    public interface IAircraftService
    {
        Task CreateAsync(string name, int numberOFSeats);
        Task EditAsync(string id, string name, int numberOFSeats);
        Task DeleteAsync(string id);
        Task<PaginatedList<Aircraft>> GetAllAsPaginatedListAsync(int pageSize, int? pageNumber);
        Task<AircraftEditViewModel> GetEditModelAsync(string aircraftId);
        Task<List<SelectListItem>> GetAircraftForSelect();
        Task<Aircraft> GetByIdAsync(string id);
    }
}
