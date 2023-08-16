using project.Dto;
using project.Entity.ENUMS;

namespace project.repository
{
    public interface ITaskRepository
    {
        Task<List<TaskDTO>> GetAllTaskAsync();
        Task<TaskDTO> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskDTO taskDto);
        Task UpdateTaskAsync(TaskDTO taskDto);
        Task DeleteTaskAsync(int id);
        Task UpdateTaskProcess(int id, EProcess process);
    }
}
