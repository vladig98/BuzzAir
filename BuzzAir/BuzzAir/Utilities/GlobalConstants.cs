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
    public const string ADMIN_ROLE = "Admin";
    public const string USER_ROLE = "Admin";

    // Keyed Services
    public const string AIRCRAFT_SEEDER = nameof(AIRCRAFT_SEEDER);
    public const string AIRPORT_SEEDER = nameof(AIRPORT_SEEDER);
    public const string CITY_SEEDER = nameof(CITY_SEEDER);
    public const string COUNTRY_SEEDER = nameof(COUNTRY_SEEDER);
    public const string TIMEZONE_SEEDER = nameof(TIMEZONE_SEEDER);
    public const string SERVICES_SEEDER = nameof(SERVICES_SEEDER);
    public const string FLIGHTS_SEEDER = nameof(FLIGHTS_SEEDER);
    public const string ROLE_SEEDER = nameof(ROLE_SEEDER);
    public const string USER_SEEDER = nameof(USER_SEEDER);
    public const string SEAT_MAP_SEEDER = nameof(SEAT_MAP_SEEDER);
}
