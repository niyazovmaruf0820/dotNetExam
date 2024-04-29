using Domain.DTOs.AssigmentDto;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IAssigmentService
{
    Task<Response<string>> AddAssigmentAsync(AddAssigmentDto assigment);
    Task<PagedResponse<List<GetAssigmentDto>>> GetAssigmentAsync(AssigmentFilter filter);
    Task<Response<string>> UpadateAssigmentAsync(UpadateAssigmentDto assigment);
    Task<Response<bool>> DeleteAssigmentAsync(int id);
    Task<Response<GetAssigmentDto>> GetAssigmentByIdAsync(int id); 
}
