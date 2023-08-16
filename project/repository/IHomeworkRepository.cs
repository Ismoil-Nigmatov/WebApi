using project.Dto;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public interface IHomeworkRepository
    {
        Task<List<HomeworkDTO>> GetAllHomeworkAsync();
        Task<HomeworkDTO> GetHomeworkByIdAsync(int id);
        Task AddHomeworkAsync(HomeworkDTO homeworkDto);
        Task UpdateHomeworkAsync(HomeworkDTO homeworkDto);
        Task DeleteHomeworkAsync(int id);
    }
}
