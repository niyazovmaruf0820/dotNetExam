using Domain.DTOs.AssigmentDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class AssigmentController(IAssigmentService _assigmentService) : ControllerBase
{
    [HttpGet("get-Assigments")]
    public async Task<PagedResponse<List<GetAssigmentDto>>> GetAssigmentsAsync([FromQuery]AssigmentFilter filter)
    {
        return await _assigmentService.GetAssigmentAsync(filter);
    }
    
    
    [HttpGet("{AssigmentId:int}")]
    public async Task<Response<GetAssigmentDto>> GetAssigmentByIdAsync([FromBody]int assigmentId)
    {
        return await _assigmentService.GetAssigmentByIdAsync(assigmentId);
    }
    
    [HttpPost("create-Assigment")]
    public async Task<Response<string>> AddAssigmentAsync([FromBody]AddAssigmentDto assigmentDto)
    {
        return await _assigmentService.AddAssigmentAsync(assigmentDto);
    }
    
    [HttpPut("update-Assigment")]
    public async Task<Response<string>> UpdateAssigmentAsync([FromBody]UpadateAssigmentDto assigmentDto)
    {
        return await _assigmentService.UpadateAssigmentAsync(assigmentDto);
    }
    
    [HttpDelete("{AssigmentId:int}")]
    public async Task<Response<bool>> DeleteAssigmentAsync([FromBody]int AssigmentId)
    {
        return await _assigmentService.DeleteAssigmentAsync(AssigmentId);
    }
}
