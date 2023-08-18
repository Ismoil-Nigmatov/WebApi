using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository) => _feedbackRepository = feedbackRepository;

        [HttpGet]
        public async Task<ActionResult> GetFeedbacks()
        {
            var feedbacks = await _feedbackRepository.GetAllFeedbackAsync();
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetFeedbackById(int id)
        {
            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(id);

            return Ok(feedback);
        }

        [HttpPost]
        public async Task<ActionResult> AddFeedback(FeedbackDTO feedbackDto)
        {
            await _feedbackRepository.AddFeedbackAsync(User, feedbackDto);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteFeedback(int id)
        {
            await _feedbackRepository.DeleteFeedbackAsync(id);
            return Ok();
        }
    }
}
