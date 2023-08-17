using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public class FeedbackRepository : IFeedbackRepository
    {

        private readonly AppDbContext _context;

        public FeedbackRepository(AppDbContext context) => _context = context;


        public async Task<List<FeedbackDTO>> GetAllFeedbackAsync()
        {
            var feedbackDtos = await _context.Feedback
                .Include(e => e.User)
                .Include(e => e.Education)
                .Select(e => new FeedbackDTO()
                {
                    Id = e.Id,
                    Description = e.Description,
                    UserId = e.User.Id,
                    EducationId = e.Education.Id
                })
                .ToListAsync();
    public async Task<List<FeedbackDTO>> GetAllFeedbackAsync()
    {
        var feedbackDtos = await _context.Feedback
            .Include(e => e.User)
            .Include(e => e.Education)
            .Select(e => new FeedbackDTO()
            {
                Id = e.Id,
                Description = e.Description,
                EducationId = e.Education.Id,
                UserId = e.User.Id
            })
            .ToListAsync();

            return feedbackDtos;
        }

    public async Task<FeedbackDTO> GetFeedbackByIdAsync(int id)
    {
        var firstOrDefaultAsync = await _context.Feedback
            .Include(e => e.User)
            .Include(e => e.Education)
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");

        FeedbackDTO feedbackDto = new FeedbackDTO();
        feedbackDto.Id = id;
        feedbackDto.Description = firstOrDefaultAsync.Description;
        feedbackDto.UserId = firstOrDefaultAsync.User.Id;
        feedbackDto.EducationId = firstOrDefaultAsync.Education.Id;

            return feedbackDto;
        }

    public async Task AddFeedbackAsync(FeedbackDTO feedbackDto)
    {
        Feedback feedback = new Feedback();
        feedback.Description = feedbackDto.Description;
        feedback.Education = await _context.Education.FindAsync(feedbackDto.EducationId);
        feedback.User = await _context.User.FindAsync(feedbackDto.UserId);
        _context.Feedback.Add(feedback);
        await _context.SaveChangesAsync();
    }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedback.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}
