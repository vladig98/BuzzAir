using System;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class Payment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public virtual CardType Card { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        public int CVC { get; set; }

        [Required]
        public virtual Currency Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}