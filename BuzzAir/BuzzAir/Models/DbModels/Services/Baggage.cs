using BuzzAir.Utilities;

namespace BuzzAir.Models.DbModels.Services
{
    public class Baggage : Service
    {
        public override string Name { get; set; }

        public decimal Kilos { get; set; }

        public Baggage()
        {
            Price = Kilos == 20 ? GlobalConstants.PriceFor20kg : Kilos == 32 ? GlobalConstants.PriceFor32kg : 0;
            Name = GetType().Name;
        }

        public override decimal Price { get; set; }
    }
}