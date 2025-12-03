using TeachTrack.Core.Exceptions;

namespace TeachTrack.Core.Entities;

public class StudentClass : Entity {
    // The PK in your DB is 'enrollment_id', so we map the inherited 'Id' to it.
    public Guid StudentId { get; private set; }
    public Guid CourseOfferId { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public decimal? Grade { get; private set; } // Nullable: Grade comes later

    // EF Core Constructor
    private StudentClass() { }

    public StudentClass(Guid studentId, Guid courseOfferId) {
        if (studentId == Guid.Empty) throw new DomainException("Student ID is required.");
        if (courseOfferId == Guid.Empty) throw new DomainException("Course Offering ID is required.");

        Id = Guid.NewGuid();
        StudentId = studentId;
        CourseOfferId = courseOfferId;
        EnrollmentDate = DateTime.UtcNow; // Default to now
        Grade = null; // No grade on creation
    }

    public void AssignGrade(decimal grade) {
        // Validation: Assuming a 0-10 or 0-100 scale. Adjust as needed.
        if (grade < 0 || grade > 100)
            throw new DomainException("Grade must be between 0 and 100.");

        Grade = grade;
    }
}