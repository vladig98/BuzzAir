namespace BuzzAir.DataValidators;

public class CreateCityVMValidator : AbstractValidator<CreateCityVM>
{
    public CreateCityVMValidator()
    {
        // Name: required, max length 100
        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("City name is required.")
            .MaximumLength(100)
            .WithMessage("City name must be at most 100 characters.");

        // CountryId: required, must be in the provided list
        _ = RuleFor(x => x.CountryId)
            .NotEmpty()
            .WithMessage("Please select a country.")
            .Must((vm, id) => vm.Countries.Any(c => c.Value == id))
            .WithMessage("Selected country is not valid.");

        // StateId: optional, but if provided must be in the list of states
        _ = RuleFor(x => x.StateId)
            .Cascade(CascadeMode.Stop)
            .Must((vm, id) => string.IsNullOrEmpty(id) || vm.States.Any(s => s.Value == id))
            .WithMessage("Selected state is not valid.");

        // TimezoneId: required, must be in the provided list
        _ = RuleFor(x => x.TimezoneId)
            .NotEmpty()
            .WithMessage("Please select a timezone.")
            .Must((vm, id) => vm.Timezones.Any(t => t.Value == id))
            .WithMessage("Selected timezone is not valid.");
    }
}
