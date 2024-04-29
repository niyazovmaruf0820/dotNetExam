using Domain.DTOs.MaterialDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class MaterialController(IMaterialService _materialService)
{
    [HttpGet("get-Materials")]
    public async Task<PagedResponse<List<GetMaterialDto>>> GetMaterialsAsync([FromQuery]MaterialFilter filter)
    {
        return await _materialService.GetMaterialsAsync(filter);
    }
    
    
    [HttpGet("{MaterialId:int}")]
    public async Task<Response<GetMaterialDto>> GetMaterialByIdAsync([FromBody]int materialId)
    {
        return await _materialService.GetMaterialByIdAsync(materialId);
    }
    
    [HttpPost("create-Material")]
    public async Task<Response<string>> AddMaterialAsync([FromBody]AddMaterialDto materialDto)
    {
        return await _materialService.AddMaterialAsync(materialDto);
    }
    
    [HttpPut("update-Material")]
    public async Task<Response<string>> UpdateMaterialAsync([FromBody]UpdateMaterialDto materialDto)
    {
        return await _materialService.UpadateMaterialAsync(materialDto);
    }
    
    [HttpDelete("{MaterialId:int}")]
    public async Task<Response<bool>> DeleteMaterialAsync([FromBody]int materialId)
    {
        return await _materialService.DeleteMaterialAsync(materialId);
    }
}
