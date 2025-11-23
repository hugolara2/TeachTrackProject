using TeachTrack.Core.Exceptions;
using TeachTrack.Core.Interface;

namespace TeachTrack.Core.Entities;

public class Department : Entity, IAggregateRoot {
    private readonly List<Major> _majors = new();

    public int Code { get; private set; } // Sequence
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    // Encapsulated Collection
    public IReadOnlyCollection<Major> Majors => _majors.AsReadOnly();

    private Department() { }

    public Department(string name, string description) {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }

    public void AddMajor(string majorName) {
        if (_majors.Any(m => m.Name == majorName))
            throw new DomainException($"Major '{majorName}' already exists in this department.");

        _majors.Add(new Major(majorName, this.Id));
    }
}