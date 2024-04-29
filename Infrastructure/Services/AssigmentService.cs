using AutoMapper;
using Domain.DTOs.AssigmentDto;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;

namespace Infrastructure.Services;

public class AssigmentService : IAssigmentService
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public AssigmentService(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<Response<string>> AddAssigmentAsync(AddAssigmentDto assigment)
    {
        try
        {
            var existingAssigment = await context.Assignments.FirstOrDefaultAsync(x => x.Title == assigment.Title);
            if (existingAssigment != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Assigment already exists");
            var mapped = mapper.Map<Assignment>(assigment);

            await context.Assignments.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Assigment");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteAssigmentAsync(int id)
    {
        try
        {
            var assigments = await context.Assignments.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (assigments == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Assigments not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetAssigmentDto>> GetAssigmentByIdAsync(int id)
    {
        try
        {
            var assigments = await context.Assignments.FirstOrDefaultAsync(x => x.Id == id);
            if (assigments == null)
                return new Response<GetAssigmentDto>(HttpStatusCode.BadRequest, "Assigment not found");
            var mapped = mapper.Map<GetAssigmentDto>(assigments);
            return new Response<GetAssigmentDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetAssigmentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetAssigmentDto>>> GetAssigmentAsync(AssigmentFilter filter)
    {
        try
        {
            var Assigments = context.Assignments.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                Assigments = Assigments.Where(x => x.Title!.ToLower().Contains(filter.Title.ToLower()));
 

            var response = await Assigments
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = Assigments.Count();

            var mapped = mapper.Map<List<GetAssigmentDto>>(response);
            return new PagedResponse<List<GetAssigmentDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetAssigmentDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetAssigmentDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpadateAssigmentAsync(UpadateAssigmentDto assigment)
    {
        try
        {
            var mapped = mapper.Map<Assignment>(assigment);
            context.Assignments.Update(mapped);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "Assigments not found");
            return new Response<string>("Assigments updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
