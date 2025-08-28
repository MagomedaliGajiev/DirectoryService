using DirectoryService.Contracts.Locations.Commands.CreateLocation;
using FluentValidation;

namespace DirectoryService.Application.Locations.Commands.CreateLocation;
public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required").WithErrorCode("name.required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters").WithErrorCode("name.minlength")
            .MaximumLength(120).WithMessage("Name must not exceed 120 characters").WithErrorCode("name.maxlength");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Address is required").WithErrorCode("address.required");

        RuleFor(x => x.Address.City)
            .NotEmpty().WithMessage("City is required").WithErrorCode("address.city.required");

        RuleFor(x => x.Address.Street)
            .NotEmpty().WithMessage("Street is required").WithErrorCode("address.street.required");

        RuleFor(x => x.Address.HouseNumber)
            .NotEmpty().WithMessage("HouseNumber is required").WithErrorCode("address.housenumber.required");

        RuleFor(x => x.Timezone)
            .NotEmpty().WithMessage("Timezone is required").WithErrorCode("timezone.required")
            .Must(BeValidTimezone).WithMessage("Invalid timezone").WithErrorCode("timezone.invalid");
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
