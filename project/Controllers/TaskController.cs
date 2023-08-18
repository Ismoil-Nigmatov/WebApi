using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.Entity.ENUMS;
using project.repository;
using Task = project.Entity.Task;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetTasks()
        {
            var tasks = await _taskRepository.GetAllTaskAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTaskById(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult> AddTask(TaskDTO taskDto)
        {
            await _taskRepository.AddTaskAsync(taskDto);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> ChangeProcess(int id, EProcess process)
        {
            await _taskRepository.UpdateTaskProcess(id, process);
            return Ok();
        }
    }
}
