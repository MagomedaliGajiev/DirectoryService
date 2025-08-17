using DirectoryService.Domain.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Path = DirectoryService.Domain.Departments.Path;

namespace DirectoryService.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasKey(d => d.Id).HasName("pk_departments");

        builder.Property(d => d.Id)
            .HasConversion(di => di.Value, id => new DepartmentId(id))
            .HasColumnName("id")
            .HasColumnOrder(0);

        builder.Property(d => d.Name)
            .HasConversion(d => d.Value, name => DepartmentName.Create(name).Value)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(DepartmentName.NAME_MAX_LENGHT)
            .HasColumnOrder(1);

        builder.Property(d => d.Identifier)
            .HasColumnName("identifier")
            .IsRequired()
            .HasMaxLength(Identifier.IDENTIFIER_MAX_LENGHT)
            .HasConversion(iden => iden.Value, iden => Identifier.Create(iden).Value)
            .HasColumnOrder(2);

        builder.Property(d => d.ParentId)
            .HasColumnName("parent_id")
            .IsRequired(false)
            .HasConversion(di => di!.Value, di => new DepartmentId(di))
            .HasColumnOrder(3);

        builder.Property(d => d.Path)
            .HasColumnName("path")
            .HasColumnType("ltree")
            .IsRequired()
            .HasConversion(p => p.Value, p => Path.Create(p))
            .HasColumnOrder(4);

        builder.Property(d => d.Depth)
            .HasColumnName("depth")
            .IsRequired()
            .HasColumnOrder(5);

        builder.Property(d => d.ChildrenCount)
            .HasColumnName("children_count")
            .IsRequired()
            .HasColumnOrder(6);

        builder.Property(d => d.IsActive)
            .IsRequired()
            .HasColumnName("is_active")
            .HasColumnOrder(7);

        builder.Property(d => d.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at")
            .HasColumnOrder(8);

        builder.Property(d => d.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at")
            .HasColumnOrder(9);

        builder.HasMany(d => d.ChildrenDepartments)
            .WithOne()
            .IsRequired()
            .HasForeignKey(d => d.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
