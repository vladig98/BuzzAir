﻿using BuzzAir.Utilities;

namespace BuzzAir.Models.DbModels.Services
{
    public class Priority : Service
    {
        public override decimal Price { get; set; } = GlobalConstants.PriorityPrice;

        public override string Name { get; set; }

        public Priority()
        {
            Name = GetType().Name;
        }
    }
}