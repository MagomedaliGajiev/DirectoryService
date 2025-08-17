using CSharpFunctionalExtensions;
using DirectoryService.Domain.DepartmentLocations;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.Locations;
public class Location
{
    //EF Core
    private Location()
    { }

    private List<Address> _addresses = [];
    private List<DepartmentLocation> _departmentLocations = [];

    public Location(LocationName name, TimeZone timeZone, Address address)
    {
        Id = new LocationId(Guid.NewGuid());
        Name = name;
        TimeZone = timeZone;
        IsActive = true;
        _addresses.Add(address);
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public LocationId Id { get; private set; }

    public LocationName Name { get; private set; }

    public TimeZone TimeZone { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyList<Address> Addresses => _addresses;

    public IReadOnlyList<DepartmentLocation> DepartmentLocations => _departmentLocations;

    public UnitResult<Error> AddAddress(Address address)
    {
        if (Addresses.Contains(address))
        {
            return Error.Validation("location.address", "Address already exists", "Address");
        }

        _addresses.Add(address);
        return UnitResult.Success<Error>();
    }
}