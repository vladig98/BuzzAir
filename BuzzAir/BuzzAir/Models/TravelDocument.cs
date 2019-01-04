using System;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class TravelDocument
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DocumenType Type { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public Country Nationality { get; set; }

        [Required]
        public Country BirthCountry { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}