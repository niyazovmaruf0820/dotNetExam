using AutoMapper;
using Domain.DTOs.MaterialDto;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;

namespace Infrastructure.Services;

public class MaterialService : IMaterialService
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public MaterialService(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<Response<string>> AddMaterialAsync(AddMaterialDto material)
    {
        try
        {
            var existingMaterial = await context.Materials.FirstOrDefaultAsync(x => x.Title == material.Title);
            if (existingMaterial != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Material already exists");
            var mapped = mapper.Map<Material>(material);

            await context.Materials.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Material");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteMaterialAsync(int id)
    {
        try
        {
            var materials = await context.Materials.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (materials == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Materials not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetMaterialDto>> GetMaterialByIdAsync(int id)
    {
        try
        {
            var materials = await context.Materials.FirstOrDefaultAsync(x => x.Id == id);
            if (materials == null)
                return new Response<GetMaterialDto>(HttpStatusCode.BadRequest, "Material not found");
            var mapped = mapper.Map<GetMaterialDto>(materials);
            return new Response<GetMaterialDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetMaterialDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetMaterialDto>>> GetMaterialsAsync(MaterialFilter filter)
    {
        try
        {
            var materials = context.Materials.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                materials = materials.Where(x => x.Title!.ToLower().Contains(filter.Title.ToLower()));
            if (filter?.CourseId != null)
                materials = materials.Where(x => x.CourseId == filter.CourseId);

            var response = await materials
                .Skip((filter!.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = materials.Count();

            var mapped = mapper.Map<List<GetMaterialDto>>(response);
            return new PagedResponse<List<GetMaterialDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetMaterialDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetMaterialDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpadateMaterialAsync(UpdateMaterialDto material)
    {
        try
        {
            var mapped = mapper.Map<Material>(material);
            context.Materials.Update(mapped);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "Materials not found");
            return new Response<string>("Materials updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
