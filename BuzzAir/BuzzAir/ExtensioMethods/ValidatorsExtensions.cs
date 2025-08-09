namespace BuzzAir.ExtensioMethods;

public static class ValidatorsExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        // Cities
        _ = services.AddScoped<IValidator<CreateCityVM>, CreateCityVMValidator>();
        _ = services.AddScoped<IValidator<EditCityVM>, EditCityVMValidator>();
        _ = services.AddScoped<IValidator<DeleteCityVM>, DeleteCityVMValidator>();
        _ = services.AddScoped<IValidator<RestoreCityVM>, RestoreCityVMValidator>();

        // States
        _ = services.AddScoped<IValidator<CreateStateVM>, CreateStateVMValidator>();
        _ = services.AddScoped<IValidator<EditStateVM>, EditStateVMValidator>();
        _ = services.AddScoped<IValidator<DeleteStateVM>, DeleteStateVMValidator>();
        _ = services.AddScoped<IValidator<RestoreStateVM>, RestoreStateVMValidator>();

        // Countries
        _ = services.AddScoped<IValidator<CreateCountryVM>, CreateCountryVMValidator>();
        _ = services.AddScoped<IValidator<EditCountryVM>, EditCountryVMValidator>();
        _ = services.AddScoped<IValidator<DeleteCountryVM>, DeleteCountryVMValidator>();
        _ = services.AddScoped<IValidator<RestoreCountryVM>, RestoreCountryVMValidator>();

        // Timezones
        _ = services.AddScoped<IValidator<CreateTimezoneVM>, CreateTimezoneVMValidator>();
        _ = services.AddScoped<IValidator<EditTimezoneVM>, EditTimezoneVMValidator>();
        _ = services.AddScoped<IValidator<DeleteTimezoneVM>, DeleteTimezoneVMValidator>();
        _ = services.AddScoped<IValidator<RestoreTimezoneVM>, RestoreTimezoneVMValidator>();

        return services;
    }
}
