using DirectoryService.Domain.DepartmentPositions;
using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("department_positions");

        builder.HasKey(dp => dp.Id).HasName("pk_department_positions");

        builder.Property(dp => dp.Id)
            .HasColumnName("id")
            .IsRequired()
            .HasConversion(dl => dl.Value, id => new DepartmentPositionId(id))
            .HasColumnOrder(0);

        builder.Property(dp => dp.PositionId)
            .HasColumnName("position_id")
            .IsRequired()
            .HasConversion(dl => dl.Value, v => new PositionId(v))
            .HasColumnOrder(1);

        builder.Property(dp => dp.DepartmentId)
            .HasColumnName("department_id")
            .IsRequired()
            .HasConversion(dl => dl.Value, v => new DepartmentId(v))
            .HasColumnOrder(2);
    }
}
