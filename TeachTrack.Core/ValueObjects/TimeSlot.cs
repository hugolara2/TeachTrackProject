using TeachTrack.Core.Exceptions;

namespace TeachTrack.Core.ValueObjects;

public record TimeSlot {
    public DayOfWeek Day { get; }
    public TimeSpan Start { get; }
    public TimeSpan End { get; }
    
    public TimeSlot(DayOfWeek day, TimeSpan start, TimeSpan end) {
        if (end <= start) throw new DomainException("End time must be after start time.");
        Day = day;
        Start = start;
        End = end;
    }
}