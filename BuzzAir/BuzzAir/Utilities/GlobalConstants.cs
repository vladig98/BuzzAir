namespace BuzzAir.Utilities
{
    public static class GlobalConstants
    {
        // Caching keys
        public const string CITY_CACHE_KEY = "City:id={0}";
        public const string STATE_CACHE_KEY = "State:id={0}";
        public const string COUNTRY_CACHE_KEY = "Country:id={0}";
        public const string AIRCRAFT_CACHE_KEY = "Aircraft:id={0}";
        public const string AIRPORT_CACHE_KEY = "Airport:id={0}";
        public const string FLIGHT_CACHE_KEY = "Flight:id={0}";

        // Caching Lists
        public const string CITIES_CACHE_KEY = "City:all";
        public const string STATES_CACHE_KEY = "State:all";
        public const string COUNTRIES_CACHE_KEY = "Country:all";
        public const string AIRCRAFT_ALL_CACHE_KEY = "Aircraft:all";
        public const string AIRPORTS_CACHE_KEY = "Airport:all";
        public const string FLIGHTS_CACHE_KEY = "Flight:all";

        // Caching Deleted Lists
        public const string CITIES_DELETED_CACHE_KEY = "City:deleted:all";
        public const string STATES_DELETED_CACHE_KEY = "State:deleted:all";
        public const string COUNTRIES_DELETED_CACHE_KEY = "Country:deleted:all";
        public const string AIRCRAFT_DELETED_ALL_CACHE_KEY = "Aircraft:deleted:all";
        public const string AIRPORTS_DELETED_CACHE_KEY = "Airport:deleted:all";
        public const string FLIGHTS_DELETED_CACHE_KEY = "Flight:deleted:all";

        // Misc
        public const decimal PriceForCabin = 0;
        public const decimal PriceFor20kg = 39;
        public const decimal PriceFor32kg = 72;

        public const int CabinKilos = 10;
        public const int TwentyKilos = 10;
        public const int ThrityTwoKilos = 10;

        public const string PriceFor20kgBag = "39";
        public const string PriceFor32kgBag = "72";
        public const decimal SeatPrice = 12;
        public const decimal PriorityPrice = 10;
        public const decimal OnTimeArrivalPrice = 10;
        public const decimal FlexibilityPrice = 10;
        public const decimal AirportCheckInPrice = 10;
        public const int MinimumNumberOfSeatsForAnAircraft = 50;
        public const int MaximumNumberOfSeatsForAnAircraft = 900;
        public const string AdminRole = "Admin";
        public const string UserRole = "User";
        public const string OneWayTicket = "One Way Ticket";
        public const string TimeFormat = "HH:mm";
        public const string DateFormat = "dd MMM yyyy";
        public const string BoardingPassFormat = "{0} {1} - {2} {3} {4} - {5}";
        public const string OriginDestinationFormat = "{0} - {1}";
        public const string DateTimeFormat = "MM/dd/yyyy hh:mm tt";
        public const string FlightFormat = " Number: {0}{8} Route: {1} - {2}{8} Date: {3} - {4}{8} Time: {5} - {6}{8} Price: F2";
        public const string PassengerFormat = " Name: {0}{3} Gender: {1}{3} Services: {2}";
        public const string ServicesFormat = "{1}          - {0}";
        public const string ServicesWithChoicesFormat = "{0} {1}";
        public const string BookingPriceFormat = "{0} {1:F2}";
        public const int ItemsPerPage = 10;
    }
}
