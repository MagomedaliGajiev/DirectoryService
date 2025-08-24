namespace DirectoryService.Domain.Positions;
public class Position
{
    // EF Core
    private Position()
    { }

    public Position(
    PositionName name,
    string? description)
    {
        Id = new PositionId(Guid.NewGuid());
        Name = name;
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public PositionId Id { get; private set; }

    public PositionName Name { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }
}