using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department> {
    public void Configure(EntityTypeBuilder<Department> builder) {
        builder.ToTable("department");
        
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .HasColumnName("department_id")
            .HasDefaultValueSql("gen_random_uuid()");

        // Sequence Mapping
        builder.Property(d => d.Code)
            .HasColumnName("department_code")
            .HasDefaultValueSql("nextval('department_code_seq'::regclass)");

        builder.Property(d => d.Name)
            .HasColumnName("department_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.Description)
            .HasColumnName("description");

        builder.HasIndex(d => d.Code).IsUnique();
        builder.HasIndex(d => d.Name).IsUnique();

        // One-to-Many Relationship with Majors
        // We configure the navigation to be accessible via the backing field if needed
        builder.HasMany(d => d.Majors)
            .WithOne() 
            .HasForeignKey("DepartmentId") // Shadow property or explicit property in Major
            .HasConstraintName("major_department_id_fkey")
            .OnDelete(DeleteBehavior.Restrict); // Don't delete department if majors exist
    }
}