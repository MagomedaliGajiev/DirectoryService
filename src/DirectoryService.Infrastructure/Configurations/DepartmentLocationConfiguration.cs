using DirectoryService.Domain.DepartmentLocations;
using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("department_locations");

        builder.HasKey(dl => dl.Id).HasName("pk_deparment_locations");

        builder.Property(dl => dl.Id)
            .HasColumnName("id")
            .IsRequired()
            .HasConversion(dl => dl.Value, id => new DepartmentLocationId(id))
            .HasColumnOrder(0);

        builder.Property(dl => dl.LocationId)
            .HasColumnName("location_id")
            .IsRequired()
            .HasConversion(dl => dl.Value, v => new LocationId(v))
            .HasColumnOrder(1);

        builder.Property(dl => dl.DepartmentId)
            .HasColumnName("location_id")
            .IsRequired()
            .HasConversion(dl => dl.Value, v => new DepartmentId(v))
            .HasColumnOrder(3);
    }
}
