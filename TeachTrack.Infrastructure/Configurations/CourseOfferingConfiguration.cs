using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Infrastructure.Configurations;

public class CourseOfferingConfiguration : IEntityTypeConfiguration<CourseOffering> {
    public void Configure(EntityTypeBuilder<CourseOffering> builder) {
        // 1. Table & Key
        builder.ToTable("course_offering");

        builder.HasKey(e => e.Id).HasName("course_offering_pkey");

        builder.Property(e => e.Id)
            .HasColumnName("course_offer_id")
            .HasDefaultValueSql("gen_random_uuid()");

        // 2. Required Foreign Keys
        builder.Property(e => e.CourseId).HasColumnName("course_id").IsRequired();
        builder.Property(e => e.TeacherId).HasColumnName("teacher_id").IsRequired();
        builder.Property(e => e.SemesterId).HasColumnName("semester_id").IsRequired();

        // 3. Optional Foreign Keys
        builder.Property(e => e.RoomId).HasColumnName("room_id");         // Nullable in DB
        builder.Property(e => e.ScheduleId).HasColumnName("schedule_id"); // Nullable in DB

        // 4. Unique Constraint (Business Logic enforcement)
        builder.HasIndex(e => new { e.CourseId, e.TeacherId, e.SemesterId })
            .HasDatabaseName("course_offering_course_id_teacher_id_semester_id_key")
            .IsUnique();

        // 5. Explicit Relationships
        // Note: We use .WithMany() assuming the parent entities don't explicitly need 
        // a collection of offerings. If they do, change .WithMany() to .WithMany(x => x.Offerings)
        
        builder.HasOne<Course>()
            .WithMany()
            .HasForeignKey(e => e.CourseId)
            .HasConstraintName("course_offering_course_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Teacher>() // You will create this Entity next?
            .WithMany()
            .HasForeignKey(e => e.TeacherId)
            .HasConstraintName("course_offering_teacher_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Semester>()
            .WithMany()
            .HasForeignKey(e => e.SemesterId)
            .HasConstraintName("course_offering_semester_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Room>()
            .WithMany()
            .HasForeignKey(e => e.RoomId)
            .HasConstraintName("course_offering_room_id_fkey")
            .OnDelete(DeleteBehavior.SetNull); // If room is deleted, offering acts as "unassigned"

        builder.HasOne<Schedule>()
            .WithMany()
            .HasForeignKey(e => e.ScheduleId)
            .HasConstraintName("course_offering_schedule_id_fkey")
            .OnDelete(DeleteBehavior.SetNull); // If schedule is deleted, offering becomes unscheduled
    }
}