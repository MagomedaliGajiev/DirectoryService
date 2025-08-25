using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain.Departments;

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