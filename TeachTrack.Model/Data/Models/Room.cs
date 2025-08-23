using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class Room
{
    public Guid RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public string? Building { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();
}
