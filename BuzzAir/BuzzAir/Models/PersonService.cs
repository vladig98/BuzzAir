namespace BuzzAir.Models
{
    public class PersonService
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public virtual IPerson Person { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
