using System;

namespace BuzzAir.Models
{
    public class Seat : Service
    {
        public override decimal Price
        {
            get
            {
                if (Type == SeatType.Normal)
                {
                    return GlobalConstants.SeatPrice;
                }
                else
                {
                    return GlobalConstants.SeatPrice * 2;
                }
            }
            set
            {
                base.Price = value;
            }
        }

        public virtual SeatType Type { get; set; }

        public int SeatNumber { get; set; }
    }
}
