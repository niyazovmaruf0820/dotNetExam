using System.Text.RegularExpressions;


namespace Domain.DTOs.CourseDto;

public class AddCourseDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Instructor { get; set; }
    public int Credits { get; set; }
}
