using BuzzAir.Models.DbModels;
using BuzzAir.Models.EditModels;

namespace BuzzAir.Services.Contracts
{
    public interface IFlightsService
    {
        Task<bool> Exists(string id);
        Task<bool> ExistsByOrigin(string origin);
        Task<bool> ExistsByDestination(string destination);
        Task<Flight> GetById(string id);
        Task<Flight> GetByOrigin(string origin);
        Task<Flight> GetByDestination(string destination);
        Task<int> GetCount();
        Task<IEnumerable<Flight>> GetAll();
        Task<List<Flight>> GetAllAsQueryable(int pageSize, int? pageNumber);
        Task<IEnumerable<Flight>> GetFlightsByOriginAndDestination(City origin, City destination, DateTime departure);
        Task<IEnumerable<Flight>> GetFlightsByCityId(string cityId);
        Task<IEnumerable<Flight>> GetFlightsForOriginIdAndDestinationId(string originId, string destinationId);
        Task<Flight> Create(Aircraft aircraft, int duration, decimal price, DateTime departure, DateTime arrival, string flightNumber, Airport origin, Airport destination);
        Task Update(FlightEditViewModel model);
        Task Delete(string flightId);
    }
}
