using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain.Locations;

public record TimeZone
{
    private TimeZone(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<TimeZone, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return GeneralErrors.ValueIsRequired(value);

        var isValid = TimeZoneInfo.TryFindSystemTimeZoneById(value, out _);

        if (!isValid)
            return GeneralErrors.ValueIsInvalid(value);

        return new TimeZone(value);
    }
}