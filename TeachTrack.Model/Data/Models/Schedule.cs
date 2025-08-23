using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class Schedule
{
    public Guid ScheduleId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
    
    public DayOfWeek DayOfWeek { get; set; }

    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();
}
