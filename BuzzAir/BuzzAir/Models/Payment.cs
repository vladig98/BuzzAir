using System;

namespace BuzzAir.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public virtual CardType Card { get; set; }

        public string CardNumber { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string CardHolder { get; set; }

        public int CVC { get; set; }
    }
}