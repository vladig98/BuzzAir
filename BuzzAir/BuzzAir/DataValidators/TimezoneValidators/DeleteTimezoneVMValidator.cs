namespace BuzzAir.DataValidators.TimezoneValidators;

public class DeleteTimezoneVMValidator : AbstractValidator<DeleteTimezoneVM>
{
    public DeleteTimezoneVMValidator()
    {
        // Id is required (hidden field, but must round-trip)
        _ = RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Invalid city identifier.");

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
    }
}
