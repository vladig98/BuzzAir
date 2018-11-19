using System.Collections.Generic;

namespace BuzzAir.Models
{
    public interface IPerson
    {
        string FullName { get; set; }

        Gender Gender { get; set; }

        ICollection<PersonService> Services { get; set; }
    }
}