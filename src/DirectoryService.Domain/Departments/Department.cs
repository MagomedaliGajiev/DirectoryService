using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.Departments;
public class Department
{
    public DepartmentId Id { get; private set; } = null!;

    public DepartmentName Name { get; private set; } = null!;

    public Identifier Identifier { get; private set; } = null!;

    public DepartmentId? ParentId { get; private set; }

    public Path Path { get; private set; } = null!;

    public int Depth { get; private set; }

    public int ChildrenCount { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }
}

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

public record DepartmentName
{
    public const int NAME_MIN_LENGHT = 3;
    public const int NAME_MAX_LENGHT = 150;

    private DepartmentName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<DepartmentName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return GeneralErrors.ValueIsRequired("department name");
        }

        if (value.Length is < NAME_MIN_LENGHT or > NAME_MAX_LENGHT)
        {
            return GeneralErrors.ValueIsInvalid("department name");
        }

        return new DepartmentName(value);
    }
}

public record Path
{
    private Path(string value)
    {
        Value = value;
    }

    private const char SEPARATOR = '/';

    public string Value { get; }

    public static Path CreateParent(Identifier identifier)
    {
        return new Path(identifier.Value);
    }

    public Path CreateChild(Identifier childIdentifier)
    {
        return new Path(Value + SEPARATOR + childIdentifier.Value);
    }

    public static Path From(string value)
    {
        return new Path(value);
    }
}