using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Infrastructure.Configurations;

public class StudentDegreeConfiguration : IEntityTypeConfiguration<StudentDegree> {
    public void Configure(EntityTypeBuilder<StudentDegree> builder) {
        builder.ToTable("student_degrees");

        builder.HasKey(e => e.Id).HasName("student_degrees_pkey");

        builder.Property(e => e.Id)
            .HasColumnName("student_degree_id")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(e => e.StudentId).HasColumnName("student_id").IsRequired();
        builder.Property(e => e.DegreeId).HasColumnName("degree_id").IsRequired();
        builder.Property(e => e.MajorId).HasColumnName("major_id").IsRequired();

        builder.Property(e => e.ExpectedGraduationDate)
            .HasColumnName("expected_graduation_date")
            .HasColumnType("date"); // Maps to Postgres DATE

        // Unique Constraint: Student can't have duplicate records for the same degree
        builder.HasIndex(e => new { e.StudentId, e.DegreeId })
            .HasDatabaseName("student_degrees_student_id_degree_id_key")
            .IsUnique();

        // Relationships
        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(e => e.StudentId)
            .HasConstraintName("student_degrees_student_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Degree>()
            .WithMany()
            .HasForeignKey(e => e.DegreeId)
            .HasConstraintName("student_degrees_degree_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Major>()
            .WithMany()
            .HasForeignKey(e => e.MajorId)
            .HasConstraintName("student_degrees_major_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);
    }
}