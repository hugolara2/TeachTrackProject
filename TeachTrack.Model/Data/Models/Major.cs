using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class Major
{
    public Guid MajorId { get; set; }

    public int MajorCode { get; set; }

    public string MajorName { get; set; } = null!;

    public Guid DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<StudentDegree> StudentDegrees { get; set; } = new List<StudentDegree>();
}
