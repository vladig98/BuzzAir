namespace BuzzAir.ExtensionMethods;

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

        // Flights
        _ = services.AddScoped<IValidator<CreateFlightVM>, CreateFlightVMValidator>();
        _ = services.AddScoped<IValidator<EditFlightVM>, EditFlightVMValidator>();
        _ = services.AddScoped<IValidator<DeleteFlightVM>, DeleteFlightVMValidator>();
        _ = services.AddScoped<IValidator<RestoreFlightVM>, RestoreFlightVMValidator>();

        // Aircraft
        _ = services.AddScoped<IValidator<CreateAircraftVM>, CreateAircraftVMValidator>();
        _ = services.AddScoped<IValidator<EditAircraftVM>, EditAircraftVMValidator>();
        _ = services.AddScoped<IValidator<DeleteAircraftVM>, DeleteAircraftVMValidator>();
        _ = services.AddScoped<IValidator<RestoreAircraftVM>, RestoreAircraftVMValidator>();

        // Airports
        _ = services.AddScoped<IValidator<CreateAirportVM>, CreateAirportVMValidator>();
        _ = services.AddScoped<IValidator<EditAirportVM>, EditAirportVMValidator>();
        _ = services.AddScoped<IValidator<DeleteAirportVM>, DeleteAirportVMValidator>();
        _ = services.AddScoped<IValidator<RestoreAirportVM>, RestoreAirportVMValidator>();

        return services;
    }
}
