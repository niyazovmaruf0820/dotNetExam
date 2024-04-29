

namespace Domain.Entities;

public class Student:BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<Assignment>? Assignments { get; set; }
    public List<Feedback>? Feedbacks { get; set; }
    public List<Submission>? Submissions { get; set; }
    public List<StudentCourse>? StudentCourses { get; set; }
}