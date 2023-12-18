using BuzzAir.Models.DbModels.Enums;
using BuzzAir.Utilities;

namespace BuzzAir.Models.DbModels.Services
{
    public class Seat : Service
    {
        public Seat()
        {
            Name = GetType().Name;
        }

        public override decimal Price { get; set; } = GlobalConstants.SeatPrice;

        public SeatType SeatType { get; set; } = SeatType.Normal;

        public override string Name { get; set; }
    }
}
