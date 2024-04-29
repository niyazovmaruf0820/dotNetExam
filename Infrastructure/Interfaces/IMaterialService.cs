using Domain.DTOs.MaterialDto;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IMaterialService
{
    Task<Response<string>> AddMaterialAsync(AddMaterialDto material);
    Task<PagedResponse<List<GetMaterialDto>>> GetMaterialsAsync(MaterialFilter filter);
    Task<Response<string>> UpadateMaterialAsync(UpdateMaterialDto material);
    Task<Response<bool>> DeleteMaterialAsync(int id);
    Task<Response<GetMaterialDto>> GetMaterialByIdAsync(int id); 
}
