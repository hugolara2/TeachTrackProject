using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class Department
{
    public Guid DepartmentId { get; set; }

    public int DepartmentCode { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();

    public virtual ICollection<Major> Majors { get; set; } = new List<Major>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
