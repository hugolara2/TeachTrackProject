using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class StudentDegree
{
    public Guid StudentDegreeId { get; set; }

    public Guid StudentId { get; set; }

    public Guid DegreeId { get; set; }

    public Guid MajorId { get; set; }

    public DateOnly? ExpectedGraduationDate { get; set; }

    public virtual Degree Degree { get; set; } = null!;

    public virtual Major Major { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
