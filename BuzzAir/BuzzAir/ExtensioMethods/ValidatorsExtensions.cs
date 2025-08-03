namespace BuzzAir.ExtensioMethods;

public static class ValidatorsExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        _ = services.AddScoped<IValidator<CreateCityVM>, CreateCityVMValidator>();

        return services;
    }
}
