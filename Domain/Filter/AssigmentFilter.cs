namespace Domain.Filter;

public class AssigmentFilter : PaginationFilter
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public string? Title { get; set; }
}
