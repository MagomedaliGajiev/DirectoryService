using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.Locations;
public class Location
{
    public LocationId Id { get; private set; }

    public LocationName Name { get; private set; }

    public Address Address { get; private set; }

    public TimeZone TimeZone { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdateAt { get; private set; }
}

public record LocationName
{
    private LocationName(string value)
    {
        Value = value;
    }

    public const int NAME_MIN_LENGTH = 3;
    public const int NAME_MAX_LENGTH = 120;

    public string Value { get; }

    public static Result<LocationName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return GeneralErrors.ValueIsRequired("value");

        if (value.Length is < NAME_MAX_LENGTH or > NAME_MAX_LENGTH)
            return GeneralErrors.ValueIsInvalid(value);

        return new LocationName(value);
    }
}

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