namespace Domain.Entities;

public class Feedback : BaseEntity
{
    public int AssignmentId { get; set;}
    public int StudentId { get; set; }
    public string? Text { get; set; }
    public Assignment? Assignment { get; set; }
    public Student? Student { get; set; }
}
