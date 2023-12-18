using BuzzAir.Utilities;

namespace BuzzAir.Models.DbModels.Services
{
    public class Flexibility : Service
    {
        public override decimal Price { get; set; } = GlobalConstants.FlexibilityPrice;

        public override string Name { get; set; }

        public Flexibility()
        {
            Name = GetType().Name;
        }
    }
}
