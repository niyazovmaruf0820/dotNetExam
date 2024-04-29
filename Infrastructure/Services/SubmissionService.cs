using AutoMapper;
using Domain.DTOs.SubmissionDto;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;

namespace Infrastructure.Services;

public class SubmissionService : ISubmissionService
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public SubmissionService(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<Response<string>> AddSubmissionAsync(AddSubmissionDto submission)
    {
        try
        {
            var mapped = mapper.Map<Submission>(submission);

            await context.Submissions.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Submission");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteSubmissionAsync(int id)
    {
        try
        {
            var submissions = await context.Submissions.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (submissions == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Submissions not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetSubmiisionDto>> GetSubmissionByIdAsync(int id)
    {
        try
        {
            var submissions = await context.Submissions.FirstOrDefaultAsync(x => x.Id == id);
            if (submissions == null)
                return new Response<GetSubmiisionDto>(HttpStatusCode.BadRequest, "Submission not found");
            var mapped = mapper.Map<GetSubmiisionDto>(submissions);
            return new Response<GetSubmiisionDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetSubmiisionDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetSubmiisionDto>>> GetSubmissionsAsync(SubmissionFilter filter)
    {
        try
        {
            var submissions = context.Submissions.AsQueryable();

            if (filter?.AssignmentId != null)
                submissions = submissions.Where(x => x.AssignmentId == filter.AssignmentId);
            if (filter?.StudentId != null)
                submissions = submissions.Where(x => x.StudentId == filter.StudentId);

            var response = await submissions
                .Skip((filter!.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = submissions.Count();

            var mapped = mapper.Map<List<GetSubmiisionDto>>(response);
            return new PagedResponse<List<GetSubmiisionDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetSubmiisionDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetSubmiisionDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpadateSubmissionAsync(UpdateSubmissionDto submission)
    {
        try
        {
            var mapped = mapper.Map<Submission>(submission);
            context.Submissions.Update(mapped);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "Submissions not found");
            return new Response<string>("Submissions updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
