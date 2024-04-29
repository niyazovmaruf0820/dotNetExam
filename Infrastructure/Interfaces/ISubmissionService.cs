using Domain.DTOs.SubmissionDto;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ISubmissionService
{
    Task<Response<string>> AddSubmissionAsync(AddSubmissionDto submission);
    Task<PagedResponse<List<GetSubmiisionDto>>> GetSubmissionsAsync(SubmissionFilter filter);
    Task<Response<string>> UpadateSubmissionAsync(UpdateSubmissionDto submission);
    Task<Response<bool>> DeleteSubmissionAsync(int id);
    Task<Response<GetSubmiisionDto>> GetSubmissionByIdAsync(int id); 
}
