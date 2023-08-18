
using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController :ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        [HttpGet]
        public async Task<ActionResult> GetCourses()
        {
            var courses = await _courseRepository.GetAllCourseAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCourseById(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);

            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult> AddCourse(CourseDTO courseDto)
        {
            await _courseRepository.AddCourseAsync(courseDto);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            await _courseRepository.DeleteCourseAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCourse(CourseDTO courseDto)
        {
            await _courseRepository.UpdateCourseAsync(courseDto);
            return Ok();
        }
    }
}
