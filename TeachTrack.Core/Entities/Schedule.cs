using TeachTrack.Core.Exceptions;
using TeachTrack.Core.Interface;

namespace TeachTrack.Core.Entities;

public class Schedule : Entity, IAggregateRoot {
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    
    private Schedule() { }
    
    public Schedule(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime) {
        if(startTime >= endTime) 
            throw new ArgumentException("Start time must be before end time.");
        
        if (startTime.Hours < 6 || endTime.Hours > 22)
            throw new DomainException("Schedule must be within operating hours (06:00 - 22:00).");
        
        Id = Guid.NewGuid();
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
    }
    
    public bool OverlapsWith(Schedule other) {
        if (DayOfWeek != other.DayOfWeek) return false;
        
        return StartTime < other.EndTime && other.StartTime < EndTime;
    }
}