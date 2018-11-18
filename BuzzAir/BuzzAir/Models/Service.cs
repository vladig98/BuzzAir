namespace BuzzAir.Models
{
    public class Service : IService
    {
        public int Id { get; set; }

        public virtual decimal Price { get; set; }
    }
}
