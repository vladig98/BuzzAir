namespace BuzzAir.ExtensionMethods;

internal static class ServicesExtensions
{
    public static IServiceCollection AddCustomAppServices(this IServiceCollection services)
    {
        _ = services.AddScoped<ICityService, CityService>();
        _ = services.AddScoped<IStateService, StateService>();
        _ = services.AddScoped<ICountryService, CountryService>();
        _ = services.AddScoped<ITimezoneService, TimezoneService>();
        _ = services.AddScoped<IFlightService, FlightService>();
        _ = services.AddScoped<IAircraftService, AircraftService>();
        _ = services.AddScoped<IAirportService, AirportService>();
        _ = services.AddScoped<IValidationService, ValidationService>();
        _ = services.AddScoped<IServicesService, ServicesService>();
        _ = services.AddScoped<IBookingService, BookingService>();
        _ = services.AddScoped<IPassengersService, PassengersService>();
        _ = services.AddScoped<IPaymentService, PaymentService>();
        _ = services.AddScoped<ISeatService, SeatService>();
        _ = services.AddScoped<ITravelDocumentService, TravelDocumentService>();

        return services;
    }

    public static IServiceCollection AddSeeders(this IServiceCollection services)
    {
        _ = services.AddKeyedScoped<IDataSeeder, CitySeeder>(GlobalConstants.CITY_SEEDER);
        _ = services.AddKeyedScoped<IDataSeeder, CountrySeeder>(GlobalConstants.COUNTRY_SEEDER);
        _ = services.AddKeyedScoped<IDataSeeder, TimezoneSeeder>(GlobalConstants.TIMEZONE_SEEDER);
        _ = services.AddKeyedScoped<IDataSeeder, AirportSeeder>(GlobalConstants.AIRPORT_SEEDER);
        _ = services.AddKeyedScoped<IDataSeeder, AircraftSeeder>(GlobalConstants.AIRCRAFT_SEEDER);
        _ = services.AddKeyedScoped<IDataSeeder, ServicesSeeder>(GlobalConstants.SERVICES_SEEDER);
        _ = services.AddKeyedScoped<IDataSeeder, FlightsSeeder>(GlobalConstants.FLIGHTS_SEEDER);
        _ = services.AddKeyedScoped<IDataSeeder, RoleSeeder>(GlobalConstants.ROLE_SEEDER);
        _ = services.AddKeyedScoped<IDataSeeder, UserSeeder>(GlobalConstants.USER_SEEDER);
        _ = services.AddKeyedScoped<IDataSeeder, SeatMapSeeder>(GlobalConstants.SEAT_MAP_SEEDER);

        return services;
    }
}
