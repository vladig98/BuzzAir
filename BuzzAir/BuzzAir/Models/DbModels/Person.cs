using BuzzAir.Models.DbModels.Contraccts;
using BuzzAir.Models.DbModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class Person : IPerson
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}
