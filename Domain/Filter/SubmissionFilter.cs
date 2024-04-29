namespace Domain.Filter;

public class SubmissionFilter : PaginationFilter
{
    public int AssignmentId { get; set;}
    public int StudentId { get; set; }
}
