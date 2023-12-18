using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Models.DbModels.Contraccts;
using BuzzAir.Models.DbModels.Enums;
using BuzzAir.Models.DbModels.Services;
using BuzzAir.Services.Contracts;

namespace BuzzAir.Services
{
    public class ServiceService : IServiceService
    {
        private readonly BuzzAirDbContext _context;

        public ServiceService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<IService> Create(string name)
        {
            //Get the fully qualified name for a random default service
            string fullyQualifiedName = typeof(AirportCheckIn).AssemblyQualifiedName;

            //replace the default service with the actual service the passenger wants
            fullyQualifiedName = fullyQualifiedName.Replace(nameof(AirportCheckIn), name);

            //create an instance of the service the passenger wants
            IService service = (IService)Activator.CreateInstance(Type.GetType(fullyQualifiedName));

            service.Id = Guid.NewGuid().ToString();

            if (service.GetType() == typeof(Seat))
            {
                ((Seat)service).SeatType = SeatType.Extra_Leg_Room;
            }

            await _context.Services.AddAsync((Service)service);
            await _context.SaveChangesAsync();

            return service;
        }

        public async Task<IService> Create(BaggageType baggageType)
        {
            IService baggage = new Baggage()
            {
                Id = Guid.NewGuid().ToString(),
                Kilos = baggageType == BaggageType.Twenty_KG ? 20 : baggageType == BaggageType.ThirtyTwo_KG ? 32 : 10
            };

            await _context.Services.AddAsync((Service)baggage);
            await _context.SaveChangesAsync();

            return baggage;
        }
    }
}
