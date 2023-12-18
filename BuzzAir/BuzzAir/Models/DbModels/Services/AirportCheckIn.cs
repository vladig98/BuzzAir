using BuzzAir.Utilities;

namespace BuzzAir.Models.DbModels.Services
{
    public class AirportCheckIn : Service
    {
        public AirportCheckIn()
        {
            Name = GetType().Name;
        }

        public override decimal Price { get; set; } = GlobalConstants.AirportCheckInPrice;

        public override string Name { get; set; }
    }
}
