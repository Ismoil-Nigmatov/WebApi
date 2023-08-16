using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllTeacherAsync();
        Task<Teacher> GetTeacherByIdAsync(int id);
        Task AddTeacherAsync(TeacherDTO teacherDto);
        Task UpdateTeacherAsync(TeacherDTO teacherDto);
        Task DeleteTeacherAsync(int id);
    }
}
