namespace BuzzAir.Models
{
    public interface IPerson
    {
        string FullName { get; set; }

        Gender Gender { get; set; }
    }
}