using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(int id);
        Task<List<CourseDTO>> GetUserCourses();
        Task AddCourseToUser(int courseId);
        Task<User?> GetUserByEmail(string email);
        string GetMyId();
    }
}
