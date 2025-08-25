using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain.Positions;

public record PositionName
{
    public const int NAME_MIN_LENGTH = 3;
    public const int NAME_MAX_LENGTH = 100;

    private PositionName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PositionName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return GeneralErrors.ValueIsRequired("position name");

        if (value.Length is < NAME_MIN_LENGTH or > NAME_MAX_LENGTH)
            return GeneralErrors.ValueIsInvalid("position name");

        return new PositionName(value);
    }
}