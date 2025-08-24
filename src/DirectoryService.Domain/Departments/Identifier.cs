using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.Departments;

public class Identifier
{
    private Identifier(string value)
    {
        Value = value;
    }

    public const int IDENTIFIER_MIN_LENGHT = 3;

    public const int IDENTIFIER_MAX_LENGHT = 150;

    private static readonly Regex _identifierRegex = new("^[a-zA-Z]+$", RegexOptions.Compiled);

    public string Value { get; }

    public static Result<Identifier, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return GeneralErrors.ValueIsRequired("identifier");
        }

        if (!_identifierRegex.IsMatch(value))
        {
            return GeneralErrors.ValueIsInvalid("identifier");
        }

        if (value.Length is < IDENTIFIER_MIN_LENGHT or > IDENTIFIER_MAX_LENGHT)
        {
            return GeneralErrors.ValueIsInvalid("identifier");
        }

        return new Identifier(value);
    }
}