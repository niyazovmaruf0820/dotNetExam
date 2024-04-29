namespace Domain.Filter;

public class MaterialFilter : PaginationFilter
{
    public int CourseId { get; set; }
    public string? Title { get; set; }
}
