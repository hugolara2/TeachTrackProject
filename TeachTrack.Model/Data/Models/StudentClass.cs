using System;
using System.Collections.Generic;

namespace TeachTrack.Model.Data.Models;

public partial class StudentClass
{
    public Guid EnrollmentId { get; set; }

    public Guid StudentId { get; set; }

    public Guid CourseOfferId { get; set; }

    public decimal? Grade { get; set; }

    public DateOnly EnrollmentDate { get; set; }

    public virtual CourseOffering CourseOffer { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
