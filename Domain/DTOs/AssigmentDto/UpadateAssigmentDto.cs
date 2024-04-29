namespace Domain.DTOs.AssigmentDto;

public class UpadateAssigmentDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}
