using TeachTrack.Core.Exceptions;

namespace TeachTrack.Core.Entities;

public class DegreeType : Entity {
    public int Code { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    private DegreeType() { }
    
    public DegreeType(int code, string name, string description) {
        if(string.IsNullOrWhiteSpace(name))
            throw new DomainException($"Degree sType name is required");
        
        Code = code;
        Name = name;
        Description = description;
    }
    
    public void UpdateDescription(string description) {
        Description = description;
    }
}