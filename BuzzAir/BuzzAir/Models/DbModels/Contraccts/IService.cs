namespace BuzzAir.Models.DbModels.Contraccts
{
    public interface IService
    {
        string Id { get; set; }

        decimal Price { get; set; }

        string Name { get; set; }
    }
}