namespace Domain.DTOs.FeedbackDto;

public class UpdateFeedbackDto
{
    public int Id { get; set; }
    public int AssignmentId { get; set;}
    public int StudentId { get; set; }
    public string? Text { get; set; }
}
