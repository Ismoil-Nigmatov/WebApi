using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonController(ILessonRepository lessonRepository) => _lessonRepository = lessonRepository;


        [HttpGet]
        public async Task<ActionResult> GetLessons()
        {
            var lessons = await _lessonRepository.GetAllLessonAsync();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetLessonById(int id)
        {
            var lesson = await _lessonRepository.GetLessonByIdAsync(id);
            return Ok(lesson);
        }

        [HttpPost]
        public async Task<ActionResult> AddLesson(LessonDTO lessonDto)
        {
            await _lessonRepository.AddLessonAsync(lessonDto);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteLesson(int id)
        {
            await _lessonRepository.DeleteLessonAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLesson(LessonDTO lessonDto)
        {
            await _lessonRepository.UpdateLessonAsync(lessonDto);
            return Ok();
        }
    }
}
