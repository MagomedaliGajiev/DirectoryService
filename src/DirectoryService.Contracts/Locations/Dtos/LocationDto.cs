namespace DirectoryService.Contracts.Locations.Dtos;

public class LocationDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public AddressDto Address { get; set; } = null!;

    public string Timezone { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}