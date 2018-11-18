namespace BuzzAir.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string City { get; set; }

        public virtual Country Country { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }
    }
}