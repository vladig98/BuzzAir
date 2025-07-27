namespace BuzzAir.Models.DbModels
{
    public class PassengerService
    {
        public required string PassengerId { get; set; }
        public required Passenger Passenger { get; set; }

        public required string ServiceId { get; set; }
        public required Service Service { get; set; }
    }
}
