﻿namespace BuzzAir.Models
{
    public class AirportCheckIn : Service
    {
        public override decimal Price { get => GlobalConstants.AirportCheckInPrice; set => base.Price = value; }

        public override string Name => this.GetType().Name;
    }
}
