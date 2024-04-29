using Domain.Responses;

namespace Domain.Filter;

public class FeedbackFilter : PaginationFilter
{
    public int AssignmentId { get; set;}
    public int StudentId { get; set; }
    public string? Text { get; set; }
}
