using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCourseAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task AddCourseAsync(CourseDTO courseDto);
        Task UpdateCourseAsync(CourseDTO courseDto);
        Task DeleteCourseAsync(int id);
    }
}
