namespace BuzzAir.Models.DbModels.Services
{
    public class Baggage : Service
    {
        public Baggage()
        {
            if (Kilos == 20)
            {
                Price = GlobalConstants.PriceFor20kg;
            }
            else if (Kilos == 32)
            {
                Price = GlobalConstants.PriceFor32kg;
            }
            else
            {
                Price = 0;
            }
        }

        public override string Name { get; set; } = "Baggage";
        public decimal Kilos { get; set; }
        public override decimal Price { get; set; }
    }
}