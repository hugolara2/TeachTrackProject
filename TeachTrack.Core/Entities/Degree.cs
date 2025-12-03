using TeachTrack.Core.Exceptions;
using TeachTrack.Core.Interface;

namespace TeachTrack.Core.Entities;

public class Degree : Entity, IAggregateRoot {
    public int Code { get; private set; }
    public string Name { get; private set; }
    
    public Guid DegreeTypeId { get; private set; }
    public Guid DepartmentId { get; private set; }
    
    private Degree() { }

    public Degree(string name, Guid degreeTypeId, Guid departmentId) {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Degree name is required.");

        if (degreeTypeId == Guid.Empty)
            throw new DomainException("Degree Type is required.");

        if (departmentId == Guid.Empty)
            throw new DomainException("Department is required.");

        Id = Guid.NewGuid();
        Name = name;
        DegreeTypeId = degreeTypeId;
        DepartmentId = departmentId;
    }
    
    public void Rename(string newName) {
        if (string.IsNullOrWhiteSpace(newName))
            throw new DomainException("Degree name is required.");

        if (Name == newName)
            return;
        Name = newName;
    }
}