using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Models.DbModels.Contraccts;
using BuzzAir.Models.DbModels.Enums;
using BuzzAir.Services.Contracts;

namespace BuzzAir.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly BuzzAirDbContext _context;
        private readonly IPassengerServiceService _passengerServiceService;

        public PassengerService(BuzzAirDbContext context, IPassengerServiceService passengerServiceService)
        {
            _context = context;
            _passengerServiceService = passengerServiceService;
        }

        public async Task<IPassenger> Create(string firstName, string lastName, Gender gender, BaggageType baggageType, ICollection<IService> services)
        {
            var passenger = new Passenger
            {
                BaggageType = baggageType,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Id = Guid.NewGuid().ToString()
            };

            foreach (var service in services)
            {
                var paxSevice = await _passengerServiceService.Create(passenger, service);

                passenger.Services.Add(paxSevice);
            }

            //await _context.Passengers.AddAsync(passenger);
            //await _context.SaveChangesAsync();

            return passenger;
        }
    }
}
