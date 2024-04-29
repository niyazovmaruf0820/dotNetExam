namespace Domain.DTOs.StudentDto;

public class GetStudentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}