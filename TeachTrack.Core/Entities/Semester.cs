using TeachTrack.Core.Exceptions;
using TeachTrack.Core.Interface;

namespace TeachTrack.Core.Entities;

public class Semester : Entity, IAggregateRoot {
    public string Name { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    
    private Semester() { }

    public Semester(string name, DateTime startDate, DateTime endDate) {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Semester name is required.");

        if (endDate < startDate)
            throw new DomainException("End date cannot be earlier than start date.");
        
        Id = Guid.NewGuid();
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void Reschedule(DateTime newStart, DateTime newEnd) {
        if (newEnd < newStart)
            throw new DomainException("End date cannot be earlier than start date.");

        StartDate = newStart;
        EndDate = newEnd;
    }
}