using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Locations;

namespace DirectoryService.Domain.DepartmentLocations;
public class DepartmentLocation
{
    // EF Core
    public DepartmentLocation()
    { }

    public DepartmentLocation(LocationId locationId, DepartmentId departmentId)
    {
        Id = new DepartmentLocationId(Guid.NewGuid());
        LocationId = locationId;
        DepartmentId = departmentId;

    }

    public DepartmentLocationId Id { get; private set; }

    public LocationId LocationId { get; private set; }

    public DepartmentId DepartmentId { get; private set; }
}
