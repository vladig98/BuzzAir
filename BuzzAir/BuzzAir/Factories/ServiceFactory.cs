namespace BuzzAir.Factories
{
    public static class ServiceFactory
    {
        internal static ServiceViewModel CreateViewModel(Service service)
        {
            ServiceViewModel viewModel = new()
            {
                Name = service.Name,
                Price = service.Price,
                Kilos = service is Baggage baggage ? (int)baggage.Kilos : 0,
                Seat = service is Seat seat ? seat.SeatType.ToString() : SeatType.Normal.ToString()
            };

            return viewModel;
        }

        public static AirportCheckIn CreateAirportCheckIn()
        {
            AirportCheckIn airportCheckIn = new();

            return airportCheckIn;
        }

        public static Baggage CreateBaggage(BaggageType baggageType)
        {
            Baggage baggage = new()
            {
                Kilos = baggageType switch
                {
                    BaggageType.Cabin => 10,
                    BaggageType.Twenty_KG => 20,
                    BaggageType.ThirtyTwo_KG => 32,
                    _ => 0M,
                }
            };

            return baggage;
        }

        public static Seat CreateSeat()
        {
            Seat seat = new()
            {
                SeatType = SeatType.Extra_Leg_Room
            };

            return seat;
        }

        public static OnTimeArrival CreateOnTimeArrival()
        {
            OnTimeArrival onTimeArrival = new();

            return onTimeArrival;
        }

        public static Flexibility CreateFlexibility()
        {
            Flexibility flexibility = new();

            return flexibility;
        }

        public static Priority CreatePriority()
        {
            Priority priority = new();

            return priority;
        }

        public static ServiceViewModel CreateAirportCheckInViewModel()
        {
            AirportCheckIn airportCheckIn = new();
            ServiceViewModel viewModel = new()
            {
                Name = airportCheckIn.Name,
                Price = airportCheckIn.Price,
                URL = "<i class=\"fa-solid fa-plane-circle-check\"></i>"
            };

            return viewModel;
        }

        public static ServiceViewModel CreateBaggageViewModel()
        {
            Baggage baggage = new();
            ServiceViewModel viewModel = new()
            {
                Name = baggage.Name,
                Price = baggage.Price,
                URL = "<i class=\"fa-solid fa-suitcase\"></i>"
            };

            return viewModel;
        }

        public static ServiceViewModel CreateSeatViewModel()
        {
            Seat seat = new();
            ServiceViewModel viewModel = new()
            {
                Name = seat.Name,
                Price = seat.Price,
                URL = "<i class=\"fa-solid fa-chair\"></i>"
            };

            return viewModel;
        }

        public static ServiceViewModel CreatePriorityViewModel()
        {
            Priority priority = new();
            ServiceViewModel viewModel = new()
            {
                Name = priority.Name,
                Price = priority.Price,
                URL = "<i class=\"fa-solid fa-van-shuttle\"></i>"
            };

            return viewModel;
        }

        public static ServiceViewModel CreateOnTimeArrivalViewModel()
        {
            OnTimeArrival onTimeArrival = new();
            ServiceViewModel viewModel = new()
            {
                Name = onTimeArrival.Name,
                Price = onTimeArrival.Price,
                URL = "<i class=\"fa-solid fa-clock\"></i>"
            };

            return viewModel;
        }

        public static ServiceViewModel CreateFlexibilityViewModel()
        {
            Flexibility flexibility = new();
            ServiceViewModel viewModel = new()
            {
                Name = flexibility.Name,
                Price = flexibility.Price,
                URL = "<i class=\"fa-solid fa-shuffle\"></i>"
            };

            return viewModel;
        }
    }
}
