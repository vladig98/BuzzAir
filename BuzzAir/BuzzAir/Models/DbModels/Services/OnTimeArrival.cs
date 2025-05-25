namespace BuzzAir.Models.DbModels.Services
{
    public class OnTimeArrival : Service
    {
        public override decimal Price { get; set; } = GlobalConstants.OnTimeArrivalPrice;
        public override string Name { get; set; } = "OnTimeArrival";
    }
}
