using TeachTrack.Core.Exceptions;
using TeachTrack.Core.Interfaces;

namespace TeachTrack.Core.Entities;

public class CourseOffering : Entity, IAggregateRoot {
    // Required relationships (Core definition)
    public Guid CourseId { get; private set; }
    public Guid TeacherId { get; private set; }
    public Guid SemesterId { get; private set; }

    // Optional relationships (Logistics)
    public Guid? RoomId { get; private set; }
    public Guid? ScheduleId { get; private set; }

    // EF Core Constructor
    private CourseOffering() { }

    public CourseOffering(Guid courseId, Guid teacherId, Guid semesterId) {
        if (courseId == Guid.Empty) throw new DomainException("Course is required.");
        if (teacherId == Guid.Empty) throw new DomainException("Teacher is required.");
        if (semesterId == Guid.Empty) throw new DomainException("Semester is required.");

        Id = Guid.NewGuid();
        CourseId = courseId;
        TeacherId = teacherId;
        SemesterId = semesterId;
    }

    public void AssignRoom(Guid roomId) {
        if (roomId == Guid.Empty) throw new DomainException("Invalid Room ID.");
        RoomId = roomId;
    }

    public void AssignSchedule(Guid scheduleId) {
        if (scheduleId == Guid.Empty) throw new DomainException("Invalid Schedule ID.");
        ScheduleId = scheduleId;
    }
}