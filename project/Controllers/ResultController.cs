using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultRepository _resultRepository;

        public ResultController(IResultRepository resultRepository) => _resultRepository = resultRepository;

        [HttpGet]
        public async Task<ActionResult> GetResults()
        {
            var results = await _resultRepository.GetAllResultAsync();
            return Ok(results ?? new List<ResultDTO>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetResultById(int id)
        {
            var feedback = await _resultRepository.GetResultByIdAsync(id);

            return Ok(feedback);
        }

        [HttpPost]
        public async Task<ActionResult> AddResult(ResultDTO resultDto)
        {
            await _resultRepository.AddResultAsync(resultDto);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteResult(int id)
        {
            await _resultRepository.DeleteResultAsync(id);
            return Ok();
        }
    }
}
