namespace BuzzAir.Models.DbModels
{
    public abstract class Service
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public abstract decimal Price { get; set; }
        public abstract string Name { get; set; }

        public ICollection<PassengerService> Passengers { get; set; } = new HashSet<PassengerService>();
    }
}
