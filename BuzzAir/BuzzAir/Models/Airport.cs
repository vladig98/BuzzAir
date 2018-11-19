namespace BuzzAir.Models
{
    public class Airport
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public virtual Country Country { get; set; }

        public string Terminal { get; set; }
    }
}