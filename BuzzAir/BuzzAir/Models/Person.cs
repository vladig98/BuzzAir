using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class Person : IPerson
    {
        public Person()
        {
            this.Services = new HashSet<PersonService>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public ICollection<PersonService> Services { get; set; }
    }
}
