namespace TeachTrack.Core.Entities;

public class Major : Entity {
    public int Code { get; private set; }
    public string Name { get; private set; }
    public Guid DepartmentId { get; private set; }

    internal Major(string name, Guid departmentId) {
        Id = Guid.NewGuid();
        Name = name;
        DepartmentId = departmentId;
    }
    
    private Major() { }
}