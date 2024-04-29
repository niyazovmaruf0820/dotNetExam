

namespace Domain.Entities;

public class Course:BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Instructor { get; set; }
    public int Credits { get; set; }
    public List<Material>? Materials { get; set; }
    public List<Assignment>? Assignments { get; set; }
    public List<StudentCourse>? StudentCourses { get; set; }
}