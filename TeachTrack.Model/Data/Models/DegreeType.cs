using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class DegreeType
{
    public Guid TypeId { get; set; }

    public int Code { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();
}
