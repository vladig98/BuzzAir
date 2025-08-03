namespace BuzzAir.Utilities;

public sealed class ValidationService(IServiceProvider serviceProvider) : IValidationService
{
    public async Task ValidateAsync<T>(T item, ModelStateDictionary modelState, CancellationToken token = default)
    {
        IValidator<T>? validator = serviceProvider.GetService<IValidator<T>>();

        if (validator is null || modelState is null)
        {
            return;
        }

        FluentValidation.Results.ValidationResult result = await validator.ValidateAsync(item, token);
        foreach (ValidationFailure error in result.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}
