using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Model.Persistence.Configurations;

public class MajorConfiguration : IEntityTypeConfiguration<Major> {
    public void Configure(EntityTypeBuilder<Major> builder) {
        builder.ToTable("major");

        builder.HasKey(m => m.Id).HasName("major_pkey");

        builder.Property(m => m.Id)
            .HasColumnName("major_id")
            .HasDefaultValueSql("gen_random_uuid()");

        // Sequence Mapping
        builder.Property(m => m.Code)
            .HasColumnName("major_code")
            .HasDefaultValueSql("nextval('major_code_seq'::regclass)");

        builder.Property(m => m.Name)
            .HasColumnName("major_name")
            .HasMaxLength(100)
            .IsRequired();
            
        // Explicitly map the Foreign Key property
        builder.Property(m => m.DepartmentId)
            .HasColumnName("department_id")
            .IsRequired();

        builder.HasIndex(m => m.Code).IsUnique();
        builder.HasIndex(m => m.Name).IsUnique();
    }
}