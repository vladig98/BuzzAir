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

        return services;
    }
}
