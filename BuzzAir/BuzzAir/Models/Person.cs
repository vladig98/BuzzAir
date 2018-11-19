using System.Collections.Generic;

namespace BuzzAir.Models
{
    public class Person : IPerson
    {
        public Person()
        {
            this.Services = new HashSet<PersonService>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<PersonService> Services { get; set; }
    }
}
