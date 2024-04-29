using Domain.DTOs.FeedbackDto;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IFeedbackService
{
    Task<Response<string>> AddFeedbackAsync(AddFeedbackDto feedback);
    Task<PagedResponse<List<GetFeedbackDto>>> GetGroupsAsync(FeedbackFilter filter);
    Task<Response<string>> UpadateFeedbackAsync(UpdateFeedbackDto feedback);
    Task<Response<bool>> DeleteFeedbackAsync(int id);
    Task<Response<GetFeedbackDto>> GetFeedbackByIdAsync(int id); 
}
