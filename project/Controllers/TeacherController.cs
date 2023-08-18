using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }


        [HttpGet]
        public async Task<ActionResult> GetTeachers()
        {
            var teachers = await _teacherRepository.GetAllTeacherAsync();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeacherById(int id)
        {
            var teacher= await _teacherRepository.GetTeacherByIdAsync(id);

            return Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult> AddTeacher(TeacherDTO teacherDto)
        {
            await _teacherRepository.AddTeacherAsync(teacherDto);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            await _teacherRepository.DeleteTeacherAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTeacher(TeacherDTO teacherDto)
        {
            await _teacherRepository.UpdateTeacherAsync(teacherDto);
            return Ok();
        }
    }
}
