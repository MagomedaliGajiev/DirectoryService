using DirectoryService.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeZone = DirectoryService.Domain.Locations.TimeZone;

namespace DirectoryService.Infrastructure.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.HasKey(x => x.Id).HasName("pk_locations");

        builder.Property(l => l.Id)
            .HasColumnName("id")
            .IsRequired()
            .HasConversion(l => l.Value, id => new LocationId(id))
            .HasColumnOrder(0);

        builder.Property(l => l.Name)
            .HasColumnName("name")
            .HasMaxLength(LocationName.NAME_MAX_LENGTH)
            .IsRequired()
            .HasConversion(l => l.Value, v => LocationName.Create(v).Value)
            .HasColumnOrder(1);

        builder.Property(l => l.TimeZone)
            .HasColumnName("timezone")
            .IsRequired()
            .HasConversion(l => l.Value, tz => TimeZone.Create(tz).Value)
            .HasColumnOrder(2);

        builder.Property(l => l.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasColumnOrder(3);

        builder.Property(l => l.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasColumnOrder(4);

        builder.Property(l => l.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired()
            .HasColumnOrder(5);

        builder.OwnsMany(l => l.Addresses, ab =>
        {
            ab.ToJson("addresses");
        });
    }
}