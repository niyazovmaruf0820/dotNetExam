using Domain.DTOs.CourseDto;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICourseService 
{
    Task<Response<string>> AddCourseAsync(AddCourseDto course);
    Task<PagedResponse<List<GetCourseDto>>> GetCoursesAsync(CourseFilter filter);
    Task<Response<string>> UpadateCourseAsync(UpdateCourseDto course);
    Task<Response<bool>> DeleteCourseAsync(int id);
    Task<Response<GetCourseDto>> GetCourseByIdAsync(int id); 
}
