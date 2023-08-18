using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController :ControllerBase
    {
        private readonly IEducationRepository _educationRepository;

        public EducationController(IEducationRepository educationRepository) => _educationRepository = educationRepository;

        [HttpGet]
        public async Task<ActionResult> GetEducations()
        {
            var educations = await _educationRepository.GetAllEducationAsync();
            return Ok(educations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEducationById(int id)
        {
            var education = await _educationRepository.GetEducationByIdAsync(id);

            return Ok(education);
        }

        [HttpPost]
        public async Task<ActionResult> AddEducation(EducationDTO educationDto)
        {
            await _educationRepository.AddEducationAsync(educationDto);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteEducation(int id)
        {
            await _educationRepository.DeleteEducationAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEducation(EducationDTO educationDto)
        {
            await _educationRepository.UpdateEducationAsync(educationDto);
            return Ok();
        }
    }
}
