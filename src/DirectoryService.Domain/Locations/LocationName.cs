using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.Locations;

public record LocationName
{
    private LocationName(string value)
    {
        Value = value;
    }

    public const int NAME_MIN_LENGTH = 3;
    public const int NAME_MAX_LENGTH = 120;

    public string Value { get; }

    public static Result<LocationName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return GeneralErrors.ValueIsRequired("value");

        if (value.Length is < NAME_MAX_LENGTH or > NAME_MAX_LENGTH)
            return GeneralErrors.ValueIsInvalid(value);

        return new LocationName(value);
    }
}
