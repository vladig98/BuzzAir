using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Models.DbModels.Contraccts;
using BuzzAir.Services.Contracts;

namespace BuzzAir.Services
{
    public class PassengerServiceService : IPassengerServiceService
    {
        private readonly BuzzAirDbContext _context;

        public PassengerServiceService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<PersonService> Create(IPassenger passenger, IService service)
        {
            var paxSevice = new PersonService
            {
                Id = Guid.NewGuid().ToString(),
                Passenger = (Passenger)passenger,
                PassengerId = ((Passenger)passenger).Id,
                Service = (Service)service,
                ServiceId = service.Id,
            };

            await _context.PersonServices.AddAsync(paxSevice);
            await _context.SaveChangesAsync();

            return paxSevice;
        }
    }
}
