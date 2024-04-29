namespace Domain.Entities;

public class Submission : BaseEntity
{
    public int AssignmentId { get; set;}
    public int StudentId { get; set; }
    public DateTime SubmissionDate { get; set;}
    public string? Content { get; set; }
    public Student? Student { get; set; }
    public Assignment? Assignment { get; set; }
}
