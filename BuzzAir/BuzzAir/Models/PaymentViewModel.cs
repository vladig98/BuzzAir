using BuzzAir.Models.DbModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class PaymentViewModel
    {
        [Required]
        [RegularExpression("\\d{16,16}", ErrorMessage = "The card number is invalid")]
        [Display(Name = "Card Number", Prompt = "Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Prompt = "Expiry Date (MM / yy)", Name = "Expiry Date (MM / yy)")]
        public string ExpiryDate { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z ]+", ErrorMessage = "The card holder must contain only latin letters.")]
        [Display(Name = "Card Holder", Prompt = "Card Holder")]
        public string CardHolder { get; set; }

        [Required]
        [RegularExpression("\\d{3,3}", ErrorMessage = "The CVC number is invalid")]
        [Display(Name = "CVC", Prompt = "CVC")]
        public string CVC { get; set; }

        [Required]
        [Display(Name = "Card Type", Prompt = "Card Type")]
        public CardType CardType { get; set; }

        [Required]
        [Display(Name = "Currency", Prompt = "Currency")]
        public Currency Currency { get; set; }
    }
}
