using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Positions;

namespace DirectoryService.Domain.DepartmentPositions;
public class DepartmentPosition
{
    //EF Core
    private DepartmentPosition()
    { }

    public DepartmentPosition(PositionId positionId, DepartmentId departmentId)
    {
        Id = new DepartmentPositionId(Guid.NewGuid());
        PositionId = positionId;
        DepartmentId = departmentId;
    }

    public DepartmentPositionId Id { get; private set; }

    public PositionId PositionId { get; private set; }

    public DepartmentId DepartmentId { get; private set; }
}