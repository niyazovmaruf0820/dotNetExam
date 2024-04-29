using Domain.DTOs.StudentDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class StudentController(IStudentService _studentService):ControllerBase
{


    [HttpGet("get-students")]
    public async Task<PagedResponse<List<GetStudentDto>>> GetStudentsAsync([FromQuery]StudentFilter filter)
    {
        return await _studentService.GetStudentsAsync(filter);
    }
    
    
    [HttpGet("{studentId:int}")]
    public async Task<Response<GetStudentDto>> GetStudentByIdAsync([FromBody]int studentId)
    {
        return await _studentService.GetStudentByIdAsync(studentId);
    }
    
    [HttpPost("create-student")]
    public async Task<Response<string>> AddStudentAsync([FromBody]AddStudentDto studentDto)
    {
        return await _studentService.CreateStudentAsync(studentDto);
    }
    
    [HttpPut("update-student")]
    public async Task<Response<string>> UpdateStudentAsync([FromBody]UpdateStudentDto studentDto)
    {
        return await _studentService.UpdateStudentAsync(studentDto);
    }
    
    [HttpDelete("{studentId:int}")]
    public async Task<Response<bool>> DeleteStudentAsync([FromBody]int studentId)
    {
        return await _studentService.DeleteStudentAsync(studentId);
    }
}