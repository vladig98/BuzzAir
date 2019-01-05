namespace BuzzAir.Models
{
    public class Baggage : Service
    {
        public int Kilos { get; set; }

        public override string Name => this.GetType().Name;

        public override decimal Price
        {
            get
            {
                if (this.Kilos == 20)
                {
                    return GlobalConstants.PriceFor20kg;
                }
                else
                {
                    return GlobalConstants.PriceFor32kg;
                }
            }
            set
            {
                base.Price = value;
            }
        }
    }
}