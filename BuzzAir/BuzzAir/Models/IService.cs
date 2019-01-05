namespace BuzzAir.Models
{
    public interface IService
    {
        int Id { get; set; }

        decimal Price { get; set; }

        string Name { get; set; }
    }
}