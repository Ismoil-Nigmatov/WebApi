using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public interface ITaskAnswerRepository
    {
        Task<List<TaskAnswerDTO>> GetAllTaskAnswerAsync();
        Task<TaskAnswerDTO> GetTaskAnswerByIdAsync(int id);
        Task AddTaskAnswerAsync(TaskAnswerDTO taskAnswerDto);
        Task UpdateTaskAnswerAsync(TaskAnswerDTO taskAnswerDto);
        Task DeleteUserAsync(int id);
    }
}
