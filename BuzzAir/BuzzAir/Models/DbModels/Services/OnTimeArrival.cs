using BuzzAir.Utilities;

namespace BuzzAir.Models.DbModels.Services
{
    public class OnTimeArrival : Service
    {
        public OnTimeArrival()
        {
            Name = GetType().Name;
        }

        public override decimal Price { get; set; } = GlobalConstants.OnTimeArrivalPrice;

        public override string Name { get; set; }
    }
}
