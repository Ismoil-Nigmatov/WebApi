using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAnswerController : ControllerBase
    {
        private readonly ITaskAnswerRepository _taskAnswerRepository;

        public TaskAnswerController(ITaskAnswerRepository taskAnswerRepository)
        {
            _taskAnswerRepository = taskAnswerRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetTaskAnswers()
        {
            var taskAnswers = await _taskAnswerRepository.GetAllTaskAnswerAsync();
            return Ok(taskAnswers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTaskAnswerById(int id)
        {
            var taskAnswer = await _taskAnswerRepository.GetTaskAnswerByIdAsync(id);

            return Ok(taskAnswer);
        }

        [HttpPost]
        public async Task<ActionResult> AddTaskAnswer(TaskAnswerDTO taskAnswerDto)
        {
            await _taskAnswerRepository.AddTaskAnswerAsync(taskAnswerDto);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteTaskAnswer(int id)
        {
            await _taskAnswerRepository.DeleteUserAsync(id);
            return Ok();
        }


        [HttpPut]
        public async Task<ActionResult> UpdateTaskAnswer(TaskAnswerDTO taskAnswerDto)
        {
            await _taskAnswerRepository.UpdateTaskAnswerAsync(taskAnswerDto);
            return Ok();
        }
    }
}
