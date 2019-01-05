namespace BuzzAir.Models
{
    public class Baggage : Service
    {
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