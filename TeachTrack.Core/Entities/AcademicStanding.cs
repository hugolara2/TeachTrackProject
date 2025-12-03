using System.Data;
using TeachTrack.Core.Exceptions;
using TeachTrack.Core.Interfaces;

namespace TeachTrack.Core.Entities;

public class AcademicStanding : Entity, IAggregateRoot {
    public string StandingName { get; private set; }
    public string Description { get; private set; }
    
    private AcademicStanding() { }
    
    public AcademicStanding(string standingName, string description) {
        if (string.IsNullOrEmpty(standingName)) throw new DomainException("Name cannot be empty");
            
        Id = Guid.NewGuid();
        StandingName = standingName;
        Description = description;
    }

    public void ChangeName(string newName) {
        if (string.IsNullOrEmpty(newName)) throw new DomainException("Name cannot be empty");
        
        StandingName = newName;
    }
}