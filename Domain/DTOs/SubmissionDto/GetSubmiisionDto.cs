namespace Domain.DTOs.SubmissionDto;

public class GetSubmiisionDto
{
    public int Id { get; set; }
    public int AssignmentId { get; set;}
    public int StudentId { get; set; }
    public DateTime SubmissionDate { get; set;}
    public string? Content { get; set; }
}
