using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class CourseOffering
{
    public Guid CourseOfferId { get; set; }

    public Guid CourseId { get; set; }

    public Guid TeacherId { get; set; }

    public Guid SemesterId { get; set; }

    public Guid? ScheduleId { get; set; }

    public Guid? RoomId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Room? Room { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public virtual Semester Semester { get; set; } = null!;

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    public virtual Teacher Teacher { get; set; } = null!;
}
