using BuzzAir.Models.DbModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class Payment
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public CardType Card { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        public string CVC { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}