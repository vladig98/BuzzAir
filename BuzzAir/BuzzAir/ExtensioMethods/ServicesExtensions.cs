namespace BuzzAir.ExtensioMethods;

internal static class ServicesExtensions
{
    public static IServiceCollection AddCustomAppServices(this IServiceCollection services)
    {
        _ = services.AddScoped<IStateService, StateService>();
        _ = services.AddScoped<ICountryService, CountryService>();
        _ = services.AddScoped<ITimezoneService, TimezoneService>();

        return services;
    }
}
