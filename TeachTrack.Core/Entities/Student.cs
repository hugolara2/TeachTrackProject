using System;
using TeachTrack.Core.Interface;
using TeachTrack.Core.ValueObjects;

namespace TeachTrack.Core.Entities;

public class Student : Entity, IAggregateRoot {
    // The "private set" ensures external code cannot mess with the state directly
    public long EnrollmentNumber { get; private set; } // Managed by DB Sequence
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    
    // Foreign Key Relationships
    public Guid AcademicStandingId { get; private set; }
    
    // Navigation Property (optional in Domain, but useful)
    // We strictly use the Interface or Base Class here if needed, but IDs are safer to avoid lazy loading issues.

    private Student() { } // For EF Core

    public Student(string firstName, string lastName, Email email, Guid academicStandingId)
    {
        Id = Guid.NewGuid();
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        Email = email;
        AcademicStandingId = academicStandingId;
        EnrollmentDate = DateTime.UtcNow; // Enforcing UTC
    }

    public void UpdateAcademicStanding(Guid newStandingId)
    {
        // Business Rule: Can we change logic here? e.g. check for probation history?
        AcademicStandingId = newStandingId;
    }

}
