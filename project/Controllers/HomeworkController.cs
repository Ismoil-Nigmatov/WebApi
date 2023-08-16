using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkRepository _homeworkRepository;

        public HomeworkController(IHomeworkRepository homeworkRepository) => _homeworkRepository = homeworkRepository;


        [HttpGet]
        public async Task<ActionResult> GetHomeworks()
        {
            var homeworks = await _homeworkRepository.GetAllHomeworkAsync();
            return Ok(homeworks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHomeworkById(int id)
        {
            var homework = await _homeworkRepository.GetHomeworkByIdAsync(id);

            return Ok(homework);
        }

        [HttpPost]
        public async Task<ActionResult> AddHomework(HomeworkDTO homeworkDto)
        {
            await _homeworkRepository.AddHomeworkAsync(homeworkDto);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteHomework(int id)
        {
            await _homeworkRepository.DeleteHomeworkAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateHomework(HomeworkDTO homeworkDto)
        {
            await _homeworkRepository.UpdateHomeworkAsync(homeworkDto);
            return Ok();
        }
    }
}
