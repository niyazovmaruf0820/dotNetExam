namespace Domain.Entities;

public class Assignment : BaseEntity
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Course? Course { get; set; }
    public Student? Student { get; set; }
    public List<Submission>? Submissions { get; set; }
    public List<Feedback>? Feedbacks { get; set; }
}
