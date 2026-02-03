using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;
using TeachTrack.Core.ValueObjects;

namespace TeachTrack.Infrastructure.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher> {
    public void Configure(EntityTypeBuilder<Teacher> builder) {
        builder.ToTable("teacher");

        builder.HasKey(t => t.Id).HasName("teacher_pkey");

        builder.Property(t => t.Id)
            .HasColumnName("teacher_id")
            .HasDefaultValueSql("gen_random_uuid()");

        // Sequence Mapping
        builder.Property(t => t.TeacherCode)
            .HasColumnName("teacher_code")
            .HasDefaultValueSql("nextval('teacher_code_seq'::regclass)");

        builder.Property(t => t.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(50)
            .IsRequired();

        // Value Object Mapping (Email)
        builder.Property(t => t.Email)
            .HasColumnName("email")
            .HasMaxLength(100)
            .IsRequired()
            .HasConversion(
                email => email.Value,             // To DB
                value => Email.Create(value));    // From DB

        builder.Property(t => t.DepartmentId)
            .HasColumnName("department_id");

        // Indexes
        builder.HasIndex(t => t.TeacherCode)
            .HasDatabaseName("teacher_teacher_code_key")
            .IsUnique();

        builder.HasIndex(t => t.Email)
            .HasDatabaseName("teacher_email_key")
            .IsUnique();

        builder.HasIndex(t => t.DepartmentId)
            .HasDatabaseName("idx_teacher_department_id");

        // Relationships
        builder.HasOne<Department>()
            .WithMany() // .WithMany(d => d.Teachers) if you have the collection
            .HasForeignKey(t => t.DepartmentId)
            .HasConstraintName("teacher_department_id_fkey")
            .OnDelete(DeleteBehavior.SetNull); // If Dept is deleted, Teacher remains (but unassigned)
    }
}