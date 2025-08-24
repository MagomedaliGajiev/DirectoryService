using DirectoryService.Domain.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");

        builder.HasKey(p => p.Id).HasName("pk_postions");
        builder.HasIndex(p => p.Name).IsUnique();

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired()
            .HasConversion(p => p.Value, id => new PositionId(id))
            .HasColumnOrder(0);

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(PositionName.NAME_MAX_LENGTH)
            .IsRequired()
            .HasConversion(p => p.Value, v => PositionName.Create(v).Value)
            .HasColumnOrder(1);

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(1000)
            .IsRequired(false)
            .HasColumnOrder(2);

        builder.Property(p => p.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasColumnOrder(3);

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasColumnOrder(4);

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired()
            .HasColumnOrder(5);
    }
}