using TeachTrack.Core.Exceptions;

namespace TeachTrack.Core.Entities;

public class StudentDegree : Entity {
    public Guid StudentId { get; private set; }
    public Guid DegreeId { get; private set; }
    public Guid MajorId { get; private set; }
    public DateTime? ExpectedGraduationDate { get; private set; }

    // EF Core Constructor
    private StudentDegree() { }

    public StudentDegree(Guid studentId, Guid degreeId, Guid majorId, DateTime? expectedGraduation = null) {
        if (studentId == Guid.Empty) throw new DomainException("Student ID is required.");
        if (degreeId == Guid.Empty) throw new DomainException("Degree ID is required.");
        if (majorId == Guid.Empty) throw new DomainException("Major ID is required.");

        Id = Guid.NewGuid();
        StudentId = studentId;
        DegreeId = degreeId;
        MajorId = majorId;
        ExpectedGraduationDate = expectedGraduation;
    }

    public void UpdateGraduationDate(DateTime newDate) {
        if (newDate < DateTime.UtcNow)
            throw new DomainException("Expected graduation date cannot be in the past.");
        
        ExpectedGraduationDate = newDate;
    }
}