using TeachTrack.Core.Exceptions;
using TeachTrack.Core.Interfaces;

namespace TeachTrack.Core.Entities;

public class Teacher : Entity, IAggregateRoot {
    public long TeacherCode { get; private set; } // Managed by DB Sequence
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public Guid? DepartmentId { get; private set; } // Nullable in your SQL

    // EF Core Constructor
    private Teacher() { }

    public Teacher(string firstName, string lastName, Email email, Guid? departmentId) {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("First name is required.");
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Last name is required.");

        if (email is null)
            throw new DomainException("Email is required.");

        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DepartmentId = departmentId;
    }

    public void AssignDepartment(Guid departmentId) {
        if (departmentId == Guid.Empty) throw new DomainException("Invalid Department ID.");
        DepartmentId = departmentId;
    }
}