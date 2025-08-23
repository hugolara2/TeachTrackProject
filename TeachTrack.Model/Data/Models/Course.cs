using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class Course
{
    public Guid CourseId { get; set; }

    public int CourseCode { get; set; }

    public string CourseTitle { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Credits { get; set; }

    public Guid DepartmentId { get; set; }

    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();

    public virtual Department Department { get; set; } = null!;
}
