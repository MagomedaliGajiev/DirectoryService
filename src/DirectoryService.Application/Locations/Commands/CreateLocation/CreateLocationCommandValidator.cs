using DirectoryService.Contracts.Locations.Commands.CreateLocation;
using FluentValidation;

namespace DirectoryService.Application.Locations.Commands.CreateLocation;
public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters")
            .MaximumLength(120).WithMessage("Name must not exceed 120 characters");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Address is required");

        RuleFor(x => x.Address.City)
            .NotEmpty().WithMessage("City is required");

        RuleFor(x => x.Address.Street)
            .NotEmpty().WithMessage("Street is required");

        RuleFor(x => x.Address.HouseNumber)
            .NotEmpty().WithMessage("HouseNumber is required");

        RuleFor(x => x.Timezone)
            .NotEmpty().WithMessage("Timezone is required")
            .Must(BeValidTimezone).WithMessage("Invalid timezone");
    }

    private bool BeValidTimezone(string timezone)
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById(timezone) != null;
        }
        catch
        {

            return false;
        }
    }
}
