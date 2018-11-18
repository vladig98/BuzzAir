using System;

namespace BuzzAir.Models
{
    public class DiscountClub : Service
    {
        public override decimal Price { get => GetPrice(); set => SetPrice(); }

        public virtual DiscountClubType DiscountClubType { get; set; }

        public int NumberOfPassengers => GetNumber();

        private void SetPrice()
        {
            return;
        }

        private decimal GetPrice()
        {
            decimal price = 0;
            switch (this.DiscountClubType)
            {
                case DiscountClubType.Standard:
                    price = GlobalConstants.PriceForStandardDiscountClub;
                    break;
                case DiscountClubType.Group:
                    price = GlobalConstants.PriceForGroupDiscountClub;
                    break;
                default:
                    break;
            }
            return price;
        }

        private int GetNumber()
        {
            int passengers = 0;
            switch (this.DiscountClubType)
            {
                case DiscountClubType.Standard:
                    passengers = GlobalConstants.NumberOfPassengersForStandardDiscountClub;
                    break;
                case DiscountClubType.Group:
                    passengers = GlobalConstants.NumberOfPassengersForGroupDiscountClub;
                    break;
                default:
                    break;
            }
            return passengers;
        }
    }
}
