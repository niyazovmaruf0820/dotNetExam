
namespace Domain.Filter;

public class CourseFilter : PaginationFilter
{
    public string? Title { get; set; }
    public string? Instructor { get; set; }
}
