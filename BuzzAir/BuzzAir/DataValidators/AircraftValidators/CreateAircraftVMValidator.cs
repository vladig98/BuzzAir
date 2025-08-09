namespace BuzzAir.DataValidators.AircraftValidators;

public class CreateAircraftVMValidator : AbstractValidator<CreateAircraftVM>
{
    public CreateAircraftVMValidator()
    {
        // Name: required, trimmed, max length 100
        _ = RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Aircraft name is required.")
                .MaximumLength(100)
                .WithMessage("Aircraft name must be at most 100 characters.");

        // NumberOfSeats: required and must be > 0
        _ = RuleFor(x => x.NumberOfSeats)
                .GreaterThan(0)
                .WithMessage("Number of seats must be greater than 0.");
    }
}
