namespace BuzzAir.Models
{
    public class Flexibility : Service
    {
        public override decimal Price { get => GlobalConstants.FlexibilityPrice; set => base.Price = value; }
    }
}
