namespace DirectoryService.Domain.Locations;

public record Address
{
    // EF Core
    private Address() { }

    public Address(
        string city,
        string street,
        string houseNumber,
        string? number)
    {
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        Number = number;
    }

    public string City { get; }

    public string Street { get; }

    public string HouseNumber { get; }

    public string? Number { get; }

    public string FullAddress(string locationName) => $"{locationName}({City} {Street} {HouseNumber} {Number})";
}