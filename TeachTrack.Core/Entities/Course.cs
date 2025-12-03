using TeachTrack.Core.Exceptions;
using TeachTrack.Core.Interface;

namespace TeachTrack.Core.Entities;

public class Course : Entity, IAggregateRoot {
    public int CourseCode { get; private set; } // Managed by DB Sequence
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Credits { get; private set; }
    public Guid DepartmentId { get; private set; }

    private Course() { }

    public Course(string title, string description, decimal credits, Guid departmentId) {
        if (credits <= 0) throw new DomainException("Credits must be greater than zero.");
        
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Credits = credits;
        DepartmentId = departmentId;
    }

    public void UpdateCredits(decimal newCredits) {
        if (newCredits <= 0) throw new DomainException("Credits must be positive.");
        Credits = newCredits;
    }
}