using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public interface IFeedbackRepository
    {
        Task<List<FeedbackDTO>> GetAllFeedbackAsync();
        Task<FeedbackDTO> GetFeedbackByIdAsync(int id);
        Task AddFeedbackAsync(FeedbackDTO feedbackDto);
        Task DeleteFeedbackAsync(int id);
    }
}
