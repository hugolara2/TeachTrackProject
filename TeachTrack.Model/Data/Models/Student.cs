using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class Student
{
    public Guid StudentId { get; set; }

    public long EnrollmentNumber { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid AcademicStandingId { get; set; }

    public DateOnly EnrollmentDate { get; set; }

    public virtual AcademicStanding AcademicStanding { get; set; } = null!;

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    public virtual ICollection<StudentDegree> StudentDegrees { get; set; } = new List<StudentDegree>();
}
