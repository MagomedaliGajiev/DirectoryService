namespace DirectoryService.Contracts.Locations.Dtos;

public class AddressDto
{
    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string HouseNumber { get; set; } = null!;

    public string? Number { get; set; }
}
