using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TeachTrack.Model.Data.Models;

namespace TeachTrack.Model.Data;

public partial class FixItContext : DbContext
{
    public FixItContext()
    {
    }

    public FixItContext(DbContextOptions<FixItContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicStanding> AcademicStandings { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseOffering> CourseOfferings { get; set; }

    public virtual DbSet<Degree> Degrees { get; set; }

    public virtual DbSet<DegreeType> DegreeTypes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

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

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("course_pkey");

            entity.ToTable("course");

            entity.HasIndex(e => e.CourseCode, "course_course_code_key").IsUnique();

            entity.Property(e => e.CourseId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("course_id");
            entity.Property(e => e.CourseCode)
                .HasDefaultValueSql("nextval('course_code_seq'::regclass)")
                .HasColumnName("course_code");
            entity.Property(e => e.CourseTitle)
                .HasMaxLength(100)
                .HasColumnName("course_title");
            entity.Property(e => e.Credits)
                .HasPrecision(4, 2)
                .HasColumnName("credits");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasOne(d => d.Department).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_department_id_fkey");
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

        modelBuilder.Entity<Degree>(entity =>
        {
            entity.HasKey(e => e.DegreeId).HasName("degree_pkey");

            entity.ToTable("degree");

            entity.HasIndex(e => e.Code, "degree_code_key").IsUnique();

            entity.HasIndex(e => e.DegreeName, "degree_degree_name_key").IsUnique();

            entity.HasIndex(e => e.DepartmentId, "idx_degree_department_id");

            entity.HasIndex(e => e.TypeId, "idx_degree_type_id");

            entity.Property(e => e.DegreeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("degree_id");
            entity.Property(e => e.Code)
                .ValueGeneratedOnAdd()
                .HasColumnName("code");
            entity.Property(e => e.DegreeName)
                .HasMaxLength(100)
                .HasColumnName("degree_name");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Degrees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("degree_department_id_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Degrees)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("degree_type_id_fkey");
        });

        modelBuilder.Entity<DegreeType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("degree_type_pkey");

            entity.ToTable("degree_type");

            entity.HasIndex(e => e.Code, "degree_type_code_key").IsUnique();

            entity.HasIndex(e => e.TypeName, "degree_type_type_name_key").IsUnique();

            entity.Property(e => e.TypeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("type_id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("department_pkey");

            entity.ToTable("department");

            entity.HasIndex(e => e.DepartmentCode, "department_department_code_key").IsUnique();

            entity.HasIndex(e => e.DepartmentName, "department_department_name_key").IsUnique();

            entity.Property(e => e.DepartmentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("department_id");
            entity.Property(e => e.DepartmentCode)
                .HasDefaultValueSql("nextval('department_code_seq'::regclass)")
                .HasColumnName("department_code");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .HasColumnName("department_name");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.MajorId).HasName("major_pkey");

            entity.ToTable("major");

            entity.HasIndex(e => e.DepartmentId, "idx_major_department_id");

            entity.HasIndex(e => e.MajorCode, "major_major_code_key").IsUnique();

            entity.HasIndex(e => e.MajorName, "major_major_name_key").IsUnique();

            entity.Property(e => e.MajorId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("major_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.MajorCode)
                .HasDefaultValueSql("nextval('major_code_seq'::regclass)")
                .HasColumnName("major_code");
            entity.Property(e => e.MajorName)
                .HasMaxLength(100)
                .HasColumnName("major_name");

            entity.HasOne(d => d.Department).WithMany(p => p.Majors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("major_department_id_fkey");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("room_pkey");

            entity.ToTable("room");

            entity.HasIndex(e => e.RoomNumber, "room_room_number_key").IsUnique();

            entity.Property(e => e.RoomId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("room_id");
            entity.Property(e => e.Building)
                .HasMaxLength(50)
                .HasColumnName("building");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.RoomNumber)
                .HasMaxLength(20)
                .HasColumnName("room_number");
        });
        
        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("schedule_pkey");

            entity.ToTable("schedule");

            entity.Property(e => e.ScheduleId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("schedule_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.SemesterId).HasName("semester_pkey");

            entity.ToTable("semester");

            entity.HasIndex(e => e.Name, "semester_name_key").IsUnique();

            entity.Property(e => e.SemesterId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("semester_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("student_pkey");

            entity.ToTable("student");

            entity.HasIndex(e => e.AcademicStandingId, "idx_student_academic_standing_id");

            entity.HasIndex(e => e.Email, "student_email_key").IsUnique();

            entity.HasIndex(e => e.EnrollmentNumber, "student_enrollment_number_key").IsUnique();

            entity.Property(e => e.StudentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("student_id");
            entity.Property(e => e.AcademicStandingId).HasColumnName("academic_standing_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("enrollment_date");
            entity.Property(e => e.EnrollmentNumber)
                .HasDefaultValueSql("nextval('student_enrollment_seq'::regclass)")
                .HasColumnName("enrollment_number");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");

            entity.HasOne(d => d.AcademicStanding).WithMany(p => p.Students)
                .HasForeignKey(d => d.AcademicStandingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_academic_standing_id_fkey");
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
