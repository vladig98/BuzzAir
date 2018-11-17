using System;

namespace BuzzAir.Models
{
    public class TravelDocument
    {
        public int Id { get; set; }

        public virtual DocumenType Type { get; set; }

        public string Number { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public virtual Country Nationality { get; set; }

        public virtual Country BirthCountry { get; set; }
    }
}