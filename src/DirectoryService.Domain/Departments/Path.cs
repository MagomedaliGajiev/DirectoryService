namespace DirectoryService.Domain.Departments;

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