using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class Teacher
{
    public Guid TeacherId { get; set; }

    public long TeacherCode { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid? DepartmentId { get; set; }

    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();

    public virtual Department? Department { get; set; }
}
