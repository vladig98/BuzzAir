namespace BuzzAir.DataValidators.TimezoneValidators;

public class CreateTimezoneVMValidator : AbstractValidator<CreateTimezoneVM>
{
    private readonly ITimezoneService _timezoneService;

    public CreateTimezoneVMValidator(ITimezoneService timezoneService)
    {
        _timezoneService = timezoneService;

        // Name: required, max length 100
        _ = RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Timezone name is required.")
                .MaximumLength(100)
                .WithMessage("Timezone name must be at most 100 characters.");

        // Identifier: required, max length 100
        _ = RuleFor(x => x.Identifier)
                .NotEmpty()
                .WithMessage("Timezone identifier is required.")
                .MaximumLength(100)
                .WithMessage("Timezone identifier must be at most 100 characters.");

        // Abbreviation: required, max length 10
        _ = RuleFor(x => x.Abbreviation)
                .NotEmpty()
                .WithMessage("Timezone abbreviation is required.")
                .MaximumLength(10)
                .WithMessage("Timezone abbreviation must be at most 10 characters.");

        // Offset: required (can't be default TimeSpan)
        _ = RuleFor(x => x.Offset)
                .NotEqual(default(TimeSpan))
                .WithMessage("Timezone offset is required.");

        // Optional: Check uniqueness if needed
        _ = RuleFor(x => x.Identifier)
                .MustAsync(async (identifier, ct) => !await _timezoneService.ExistsByIdentifierAsync(identifier, ct))
                .WithMessage("A timezone with this identifier already exists.");
    }
}
