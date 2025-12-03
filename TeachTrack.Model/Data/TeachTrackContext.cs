using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TeachTrack.Core.Entities;
using TeachTrack.Model.Data.Models;

namespace TeachTrack.Model.Data;

public partial class TeachTrackContext : DbContext {
    public TeachTrackContext() { }

    public TeachTrackContext(DbContextOptions<TeachTrackContext> options)
        : base(options) { }
    
    public DbSet<Student> Students { get; set; }
    
    public DbSet<Department> Departments { get; set; }
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Major> Majors { get; set; }
    
    public virtual DbSet<Degree> Degrees { get; set; }
    
    public virtual DbSet<DegreeType> DegreeTypes { get; set; }
    
    public virtual DbSet<Room> Rooms { get; set; }
    
    public virtual DbSet<AcademicStanding> AcademicStandings { get; set; }

    public virtual DbSet<CourseOffering> CourseOfferings { get; set; }
    
    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<StudentClass> StudentClasses { get; set; }

    public virtual DbSet<StudentDegree> StudentDegrees { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<DayOfWeek>();

        modelBuilder.Entity<AcademicStanding>(entity =>
        {
            entity.HasKey(e => e.AcademicStandingId).HasName("academic_standing_pkey");

            entity.ToTable("academic_standing");

            entity.HasIndex(e => e.StandingName, "academic_standing_standing_name_key").IsUnique();

            entity.Property(e => e.AcademicStandingId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("academic_standing_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.StandingName)
                .HasMaxLength(50)
                .HasColumnName("standing_name");
        });

        modelBuilder.Entity<CourseOffering>(entity =>
        {
            entity.HasKey(e => e.CourseOfferId).HasName("course_offering_pkey");

            entity.ToTable("course_offering");

            entity.HasIndex(e => new { e.CourseId, e.TeacherId, e.SemesterId }, "course_offering_course_id_teacher_id_semester_id_key").IsUnique();

            entity.HasIndex(e => e.CourseId, "idx_course_offering_course_id");

            entity.HasIndex(e => e.RoomId, "idx_course_offering_room_id");

            entity.HasIndex(e => e.ScheduleId, "idx_course_offering_schedule_id");

            entity.HasIndex(e => e.SemesterId, "idx_course_offering_semester_id");

            entity.HasIndex(e => e.TeacherId, "idx_course_offering_teacher_id");

            entity.Property(e => e.CourseOfferId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("course_offer_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.SemesterId).HasColumnName("semester_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseOfferings)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_offering_course_id_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.CourseOfferings)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("course_offering_room_id_fkey");

            entity.HasOne(d => d.Schedule).WithMany(p => p.CourseOfferings)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("course_offering_schedule_id_fkey");

            entity.HasOne(d => d.Semester).WithMany(p => p.CourseOfferings)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_offering_semester_id_fkey");

            entity.HasOne(d => d.Teacher).WithMany(p => p.CourseOfferings)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_offering_teacher_id_fkey");
        });
        
        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("student_classes_pkey");

            entity.ToTable("student_classes");

            entity.HasIndex(e => e.CourseOfferId, "idx_student_classes_course_offer_id");

            entity.HasIndex(e => e.StudentId, "idx_student_classes_student_id");

            entity.HasIndex(e => new { e.StudentId, e.CourseOfferId }, "student_classes_student_id_course_offer_id_key").IsUnique();

            entity.Property(e => e.EnrollmentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("enrollment_id");
            entity.Property(e => e.CourseOfferId).HasColumnName("course_offer_id");
            entity.Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("enrollment_date");
            entity.Property(e => e.Grade)
                .HasPrecision(5, 2)
                .HasColumnName("grade");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.CourseOffer).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.CourseOfferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_classes_course_offer_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_classes_student_id_fkey");
        });

        modelBuilder.Entity<StudentDegree>(entity =>
        {
            entity.HasKey(e => e.StudentDegreeId).HasName("student_degrees_pkey");

            entity.ToTable("student_degrees");

            entity.HasIndex(e => e.DegreeId, "idx_student_degrees_degree_id");

            entity.HasIndex(e => e.MajorId, "idx_student_degrees_major_id");

            entity.HasIndex(e => e.StudentId, "idx_student_degrees_student_id");

            entity.HasIndex(e => new { e.StudentId, e.DegreeId }, "student_degrees_student_id_degree_id_key").IsUnique();

            entity.Property(e => e.StudentDegreeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("student_degree_id");
            entity.Property(e => e.DegreeId).HasColumnName("degree_id");
            entity.Property(e => e.ExpectedGraduationDate).HasColumnName("expected_graduation_date");
            entity.Property(e => e.MajorId).HasColumnName("major_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Degree).WithMany(p => p.StudentDegrees)
                .HasForeignKey(d => d.DegreeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_degrees_degree_id_fkey");

            entity.HasOne(d => d.Major).WithMany(p => p.StudentDegrees)
                .HasForeignKey(d => d.MajorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_degrees_major_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentDegrees)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_degrees_student_id_fkey");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("teacher_pkey");

            entity.ToTable("teacher");

            entity.HasIndex(e => e.DepartmentId, "idx_teacher_department_id");

            entity.HasIndex(e => e.Email, "teacher_email_key").IsUnique();

            entity.HasIndex(e => e.TeacherCode, "teacher_teacher_code_key").IsUnique();

            entity.Property(e => e.TeacherId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("teacher_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.TeacherCode)
                .HasDefaultValueSql("nextval('teacher_code_seq'::regclass)")
                .HasColumnName("teacher_code");

            entity.HasOne(d => d.Department).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("teacher_department_id_fkey");
        });
        modelBuilder.HasSequence("course_code_seq")
            .StartsAt(170120L)
            .IncrementsBy(3);
        modelBuilder.HasSequence("degree_code_seq")
            .StartsAt(2499L)
            .IncrementsBy(3);
        modelBuilder.HasSequence("department_code_seq").StartsAt(21510L);
        modelBuilder.HasSequence("major_code_seq").StartsAt(321120L);
        modelBuilder.HasSequence("student_enrollment_seq").StartsAt(215111111110L);
        modelBuilder.HasSequence("teacher_code_seq").StartsAt(215122L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
