namespace BuzzAir.Utilities.Interfaces;

public interface IValidationService
{
    Task ValidateAsync<T>(T item, ModelStateDictionary modelState, CancellationToken token = default);
}
