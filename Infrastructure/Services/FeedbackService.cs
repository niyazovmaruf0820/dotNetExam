using AutoMapper;
using Domain.DTOs.FeedbackDto;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;

namespace Infrastructure.Services;

public class FeedbackService : IFeedbackService
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public FeedbackService(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<Response<string>> AddFeedbackAsync(AddFeedbackDto feedback)
    {
        try
        {
            var existingFeedback = await context.Feedbacks.FirstOrDefaultAsync(x => x.Text == feedback.Text);
            if (existingFeedback != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Feedback already exists");
            var mapped = mapper.Map<Feedback>(feedback);

            await context.Feedbacks.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Feedback");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteFeedbackAsync(int id)
    {
        try
        {
            var feedbacks = await context.Feedbacks.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (feedbacks == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Feedbacks not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetFeedbackDto>> GetFeedbackByIdAsync(int id)
    {
        try
        {
            var feedbacks = await context.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);
            if (feedbacks == null)
                return new Response<GetFeedbackDto>(HttpStatusCode.BadRequest, "Feedback not found");
            var mapped = mapper.Map<GetFeedbackDto>(feedbacks);
            return new Response<GetFeedbackDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetFeedbackDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetFeedbackDto>>> GetGroupsAsync(FeedbackFilter filter)
    {
        try
        {
            var feedbacks = context.Feedbacks.AsQueryable();

            if (filter?.AssignmentId != null)
                feedbacks = feedbacks.Where(x => x.AssignmentId == filter.AssignmentId);
            if (filter?.StudentId != null)
                feedbacks = feedbacks.Where(x => x.StudentId == filter.StudentId);
            if (!string.IsNullOrEmpty(filter?.Text))
                feedbacks = feedbacks.Where(x => x.Text!.ToLower().Contains(filter.Text.ToLower()));

            var response = await feedbacks
                .Skip((filter!.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = feedbacks.Count();

            var mapped = mapper.Map<List<GetFeedbackDto>>(response);
            return new PagedResponse<List<GetFeedbackDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetFeedbackDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetFeedbackDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpadateFeedbackAsync(UpdateFeedbackDto Feedback)
    {
        try
        {
            var mapped = mapper.Map<Feedback>(Feedback);
            context.Feedbacks.Update(mapped);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "Feedbacks not found");
            return new Response<string>("Feedbacks updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
