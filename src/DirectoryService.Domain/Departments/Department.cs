using CSharpFunctionalExtensions;
using DirectoryService.Domain.DepartmentLocations;
using DirectoryService.Domain.DepartmentPositions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.Departments;
public class Department
{
    private readonly List<Department> _childrenDepartments = [];
    private readonly List<DepartmentLocation> _departmentLocations = [];
    private readonly List<DepartmentPosition> _departmentPositions = [];

    private Department(
        DepartmentId id,
        DepartmentId? parentId,
        DepartmentName name,
        Identifier identifier,
        Path path,
        int depth,
        IEnumerable<DepartmentLocation> departmentLocations)
    {
        Id = id;
        ParentId = parentId;
        Name = name;
        Identifier = identifier;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        ChildrenCount = ChildrenDepartments.Count;
        Path = path;
        Depth = depth;
        _departmentLocations = departmentLocations.ToList();
    }

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

    public IReadOnlyList<Department> ChildrenDepartments => _childrenDepartments;

    public IReadOnlyList<DepartmentLocation> DepartmentLocations => _departmentLocations;

    public IReadOnlyList<DepartmentPosition> DepartmentPositions => _departmentPositions;

    public static Result<Department, Error> CreateParent(
        DepartmentName name,
        Identifier identifier,
        IEnumerable<DepartmentLocation> departmentLocations,
        DepartmentId? departmentId = null)
    {
        var departmentLocationsList = departmentLocations.ToList();

        if (departmentLocationsList.Count == 0)
        {
            return Error.Validation("department.location", "Department locations must contain at least one location");
        }

        var path = Path.CreateParent(identifier);
        return new Department(departmentId ?? new DepartmentId(Guid.NewGuid()), name, identifier, path, 0, departmentLocationsList);
    }

    public static Result<Department, Error> CreateChild(
        DepartmentName name,
        Identifier identifier,
        Department parent,
        IEnumerable<DepartmentLocation> departmentLocations,
        DepartmentId? departmentId = null)
    {
        var departmentLocationsList = departmentLocations.ToList();

        if (departmentLocationsList.Count == 0)
        {
            return Error.Validation("department.location", "Department locations must contain at least one location");
        }

        var path = parent.Path.CreateChild(identifier);

        return new Department(departmentId ?? new DepartmentId(Guid.NewGuid()), name, identifier, path, parent.Depth + 1, departmentLocationsList);
    }
}