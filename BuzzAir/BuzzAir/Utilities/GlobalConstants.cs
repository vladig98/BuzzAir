namespace BuzzAir.Utilities;

internal static class GlobalConstants
{
    // Caching keys
    public static readonly CompositeFormat CITY_CACHE_KEY = CompositeFormat.Parse("City:id={0}");
    public static readonly CompositeFormat STATE_CACHE_KEY = CompositeFormat.Parse("State:id={0}");
    public static readonly CompositeFormat COUNTRY_CACHE_KEY = CompositeFormat.Parse("Country:id={0}");
    public static readonly CompositeFormat AIRCRAFT_CACHE_KEY = CompositeFormat.Parse("Aircraft:id={0}");
    public static readonly CompositeFormat AIRPORT_CACHE_KEY = CompositeFormat.Parse("Airport:id={0}");
    public static readonly CompositeFormat FLIGHT_CACHE_KEY = CompositeFormat.Parse("Flight:id={0}");

    // Caching Lists
    public const string CITIES_CACHE_KEY = "City:all";
    public const string STATES_CACHE_KEY = "State:all";
    public const string COUNTRIES_CACHE_KEY = "Country:all";
    public const string AIRCRAFT_ALL_CACHE_KEY = "Aircraft:all";
    public const string AIRPORTS_CACHE_KEY = "Airport:all";
    public const string FLIGHTS_CACHE_KEY = "Flight:all";

    // Roles
    public const string AdminRole = "Admin";

    // Misc
    public const decimal PriceForCabin = 0;
    public const decimal PriceFor20kg = 39;
    public const decimal PriceFor32kg = 72;
    public const double MinimumTicketPrice = 10;
    public const double MaximumTicketPrice = 10_000;
    public const int MinimumFlightLength = 30;
    public const int MaximumFlightLength = 1200;

    public const int CabinKilos = 10;
    public const int TwentyKilos = 10;
    public const int ThrityTwoKilos = 10;

    public const int MinimumNumberOfSeatsForAnAircraft = 50;
    public const int MaximumNumberOfSeatsForAnAircraft = 900;
    public const int ItemsPerPage = 10;
}
