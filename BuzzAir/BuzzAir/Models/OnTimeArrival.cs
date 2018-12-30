namespace BuzzAir.Models
{
    public class OnTimeArrival : Service
    {
        public override decimal Price { get => GlobalConstants.OnTimeArrivalPrice; set => base.Price = value; }
    }
}
