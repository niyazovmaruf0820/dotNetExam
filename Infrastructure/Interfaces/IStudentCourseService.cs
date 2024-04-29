using Domain.DTOs.StudentCourseDto;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IStudentCourseService
{
    Task<Response<string>> AddStudentCourseAsync(AddStudentCourseDto studentCourse);
    Task<PagedResponse<List<GetStudentCourseDto>>> GetStudentCoursesAsync();
    Task<Response<string>> UpadateStudentCourseAsync(UpdateStudentCourseDto studentCourse);
    Task<Response<bool>> DeleteStudentCourseAsync(int id);
    Task<Response<GetStudentCourseDto>> GetStudentCourseByIdAsync(int id); 
}
