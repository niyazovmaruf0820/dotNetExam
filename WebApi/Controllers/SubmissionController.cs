using Domain.DTOs.AssigmentDto;
using Domain.DTOs.SubmissionDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class SubmissionController(ISubmissionService _submissionService)
{
    [HttpGet("get-Submissinons")]
    public async Task<PagedResponse<List<GetSubmiisionDto>>> GetSubmissinonsAsync([FromQuery]SubmissionFilter filter)
    {
        return await _submissionService.GetSubmissionsAsync(filter);
    }
    
    
    [HttpGet("{SubmissinonId:int}")]
    public async Task<Response<GetSubmiisionDto>> GetSubmissinonByIdAsync([FromBody]int submissinonId)
    {
        return await _submissionService.GetSubmissionByIdAsync(submissinonId);
    }
    
    [HttpPost("create-Submissinon")]
    public async Task<Response<string>> AddSubmissinonAsync([FromBody]AddSubmissionDto submissinonDto)
    {
        return await _submissionService.AddSubmissionAsync(submissinonDto);
    }
    
    [HttpPut("update-Submissinon")]
    public async Task<Response<string>> UpdateSubmissinonAsync([FromBody]UpdateSubmissionDto submissinonDto)
    {
        return await _submissionService.UpadateSubmissionAsync(submissinonDto);
    }
    
    [HttpDelete("{SubmissinonId:int}")]
    public async Task<Response<bool>> DeleteSubmissinonAsync([FromBody]int submissinonId)
    {
        return await _submissionService.DeleteSubmissionAsync(submissinonId);
    }   
}
