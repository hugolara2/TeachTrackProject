using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class Semester
{
    public Guid SemesterId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();
}
