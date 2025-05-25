namespace BuzzAir.Models.DbModels.Contraccts
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }

        Gender Gender { get; set; }

        DateTime DateOfBirth { get; set; }
    }
}