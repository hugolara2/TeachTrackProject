using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Infrastructure.Configurations;

public class StudentClassConfiguration : IEntityTypeConfiguration<StudentClass> {
    public void Configure(EntityTypeBuilder<StudentClass> builder) {
        builder.ToTable("student_classes");

        // Map inherited Id to enrollment_id
        builder.HasKey(e => e.Id).HasName("student_classes_pkey");

        builder.Property(e => e.Id)
            .HasColumnName("enrollment_id")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(e => e.StudentId)
            .HasColumnName("student_id")
            .IsRequired();

        builder.Property(e => e.CourseOfferId)
            .HasColumnName("course_offer_id")
            .IsRequired();

        // Postgres 'date' vs C# 'DateTime'
        builder.Property(e => e.EnrollmentDate)
            .HasColumnName("enrollment_date")
            .HasColumnType("date") 
            .HasDefaultValueSql("CURRENT_DATE")
            .IsRequired();

        builder.Property(e => e.Grade)
            .HasColumnName("grade")
            .HasPrecision(5, 2); // Matches NUMERIC(5,2)

        // Unique Constraint: A student cannot take the same course offering twice
        builder.HasIndex(e => new { e.StudentId, e.CourseOfferId })
            .HasDatabaseName("student_classes_student_id_course_offer_id_key")
            .IsUnique();

        // Relationships
        builder.HasOne<Student>()
            .WithMany() // .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId)
            .HasConstraintName("student_classes_student_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<CourseOffering>()
            .WithMany()
            .HasForeignKey(e => e.CourseOfferId)
            .HasConstraintName("student_classes_course_offer_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);
    }
}