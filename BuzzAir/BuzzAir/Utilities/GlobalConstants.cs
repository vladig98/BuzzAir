namespace BuzzAir.Utilities
{
    public static class GlobalConstants
    {
        public const decimal PriceFor20kg = 39;
        public const decimal PriceFor32kg = 72;
        public const string PriceFor20kgBag = "39";
        public const string PriceFor32kgBag = "72";
        public const decimal SeatPrice = 12;
        public const decimal PriorityPrice = 10;
        public const decimal OnTimeArrivalPrice = 10;
        public const decimal FlexibilityPrice = 10;
        public const decimal AirportCheckInPrice = 10;
        public const int MinimumNumberOfSeatsForAnAircraft = 50;
        public const int MaximumNumberOfSeatsForAnAircraft = 350;
        public const string AdminRole = "Admin";
        public const string UserRole = "User";
        public const string OneWayTicket = "One Way Ticket";
        public const string TimeFormat = "HH:mm";
        public const string DateFormat = "dd MMM yyyy";
        public const string BoardingPassFormat = "{0} {1} - {2} {3} {4} - {5}";
        public const string OriginDestinationFormat = "{0} - {1}";
        public const string DateTimeFormat = "MM/dd/yyyy hh:mm tt";
        public const string FlightFormat = " Number: {0}{8} Route: {1} - {2}{8} Date: {3} - {4}{8} Time: {5} - {6}{8} Price: ${7:F2}";
        public const string PassengerFormat = " Name: {0}{3} Gender: {1}{3} Services: {2}";
        public const string ServicesFormat = "{1}          - {0}";
        public const string ServicesWithChoicesFormat = "{0} {1}";
        public const string BookingPriceFormat = "{0} {1:F2}";
    }
}
