

namespace Domain.DTOs.StudentDto;

public class UpdateStudentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}