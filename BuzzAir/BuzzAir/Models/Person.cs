using System.Collections.Generic;

namespace BuzzAir.Models
{
    public class Person : IPerson
    {
        public Person()
        {
            this.Services = new HashSet<Service>();
        }

        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
