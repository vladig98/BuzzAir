namespace BuzzAir.ExtensioMethods;

public static class ValidatorsExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        _ = services.AddScoped<IValidator<CreateCityVM>, CreateCityVMValidator>();
        _ = services.AddScoped<IValidator<EditCityVM>, EditCityVMValidator>();
        _ = services.AddScoped<IValidator<DeleteCityVM>, DeleteCityVMValidator>();
        _ = services.AddScoped<IValidator<RestoreCityVM>, RestoreCityVMValidator>();

        return services;
    }
}
