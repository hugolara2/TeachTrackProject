using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class Degree
{
    public Guid DegreeId { get; set; }

    public int Code { get; set; }

    public string DegreeName { get; set; } = null!;

    public Guid TypeId { get; set; }

    public Guid DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<StudentDegree> StudentDegrees { get; set; } = new List<StudentDegree>();

    public virtual DegreeType Type { get; set; } = null!;
}
