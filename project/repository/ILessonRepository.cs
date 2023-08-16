using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public interface ILessonRepository
    {
        Task<List<LessonDTO>> GetAllLessonAsync();
        Task<LessonDTO> GetLessonByIdAsync(int id);
        Task AddLessonAsync(LessonDTO lessonDto);
        Task UpdateLessonAsync(LessonDTO lessonDto);
        Task DeleteLessonAsync(int id);
    }
}
