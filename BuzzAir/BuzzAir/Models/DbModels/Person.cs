using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Models.DbModels
{
    public class Person : IPerson
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
