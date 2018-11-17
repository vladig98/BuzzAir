namespace BuzzAir.Models
{
    public class Baggage : IService
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Kilos { get; set; }
    }
}