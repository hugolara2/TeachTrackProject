using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Model.Persistence.Configurations;

public class DegreeConfiguration : IEntityTypeConfiguration<Degree> {
    public void Configure(EntityTypeBuilder<Degree> builder) {
        // 1. Table Mapping
        builder.ToTable("degree");

        // 2. Primary Key
        builder.HasKey(d => d.Id).HasName("degree_pkey");

        builder.Property(d => d.Id)
            .HasColumnName("degree_id")
            .HasDefaultValueSql("gen_random_uuid()");

        // 3. Properties & Sequences
        builder.Property(d => d.Code)
            .HasColumnName("code")
            .HasDefaultValueSql("nextval('degree_code_seq'::regclass)"); // Explicit sequence mapping

        builder.Property(d => d.Name)
            .HasColumnName("degree_name") // Mapped from 'Name' in Domain to 'degree_name' in DB
            .HasMaxLength(100)
            .IsRequired();

        // 4. Foreign Keys (Columns)
        builder.Property(d => d.DepartmentId)
            .HasColumnName("department_id")
            .IsRequired();

        builder.Property(d => d.DegreeTypeId)
            .HasColumnName("type_id") // Maps Domain 'DegreeTypeId' to DB column 'type_id'
            .IsRequired();

        // 5. Indexes
        builder.HasIndex(d => d.Code)
            .HasDatabaseName("degree_code_key")
            .IsUnique();

        builder.HasIndex(d => d.Name)
            .HasDatabaseName("degree_degree_name_key")
            .IsUnique();

        builder.HasIndex(d => d.DepartmentId)
            .HasDatabaseName("idx_degree_department_id");

        builder.HasIndex(d => d.DegreeTypeId)
            .HasDatabaseName("idx_degree_type_id");

        // 6. Relationships
        // Even without navigation properties in the Domain Entity, we define the FK constraints here
        // so EF Core knows about the relationship at the database level.
        
        builder.HasOne<Department>()
            .WithMany() // Assuming Department doesn't have a specific collection of Degrees
            .HasForeignKey(d => d.DepartmentId)
            .HasConstraintName("degree_department_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<DegreeType>()
            .WithMany()
            .HasForeignKey(d => d.DegreeTypeId)
            .HasConstraintName("degree_type_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);
    }
}