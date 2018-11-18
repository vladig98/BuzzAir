using System;

namespace BuzzAir.Models
{
    public class Seat : IService
    {
        public int Id { get; set; }

        public decimal Price { get => GetPrice(); set => SetPrice(value); }

        public virtual SeatType Type { get; set; }

        public int SeatNumber { get; set; }

        private void SetPrice(decimal price)
        {
            this.Price = price;
        }

        private decimal GetPrice()
        {
            if (this.Type == SeatType.Extra_Leg_Room)
            {
                return this.Price * 2;
            }
            return this.Price;
        }
    }
}
