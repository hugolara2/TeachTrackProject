using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TeachTrack.Core.Entities;

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
