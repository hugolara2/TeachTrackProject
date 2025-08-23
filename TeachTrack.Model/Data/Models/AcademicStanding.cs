using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class AcademicStanding
{
    public Guid AcademicStandingId { get; set; }

    public string StandingName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
