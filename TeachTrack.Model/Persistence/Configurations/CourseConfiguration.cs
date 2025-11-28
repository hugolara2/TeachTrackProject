using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Model.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course> {
    public void Configure(EntityTypeBuilder<Course> builder) {
        builder.ToTable("course");

        builder.HasKey(c => c.Id).HasName("course_pkey");

        builder.Property(c => c.Id)
            .HasColumnName("course_id")
            .HasDefaultValueSql("gen_random_uuid()");

        // Sequence Mapping
        builder.Property(c => c.CourseCode)
            .HasColumnName("course_code")
            .HasDefaultValueSql("nextval('course_code_seq'::regclass)");

        builder.Property(c => c.Title)
            .HasColumnName("course_title")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnName("description");

        // Numeric Precision for Credits (NUMERIC(4,2))
        builder.Property(c => c.Credits)
            .HasColumnName("credits")
            .HasPrecision(4, 2)
            .IsRequired();

        builder.Property(c => c.DepartmentId)
            .HasColumnName("department_id");

        builder.HasIndex(c => c.CourseCode).IsUnique();

        // Relationship: A Course belongs to a Department
        builder.HasOne<Department>()
            .WithMany() // or .WithMany(d => d.Courses) if you added that collection to Department
            .HasForeignKey(c => c.DepartmentId)
            .HasConstraintName("course_department_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);
    }
}