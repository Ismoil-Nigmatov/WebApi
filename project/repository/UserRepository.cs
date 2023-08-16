using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            var firstOrDefaultAsync = await _context.User.FirstOrDefaultAsync(u => u.Id == userDto.Id);

            if (firstOrDefaultAsync != null)
            {
                firstOrDefaultAsync.FullName = userDto.FullName;
                firstOrDefaultAsync.Email = userDto.Email;
                firstOrDefaultAsync.Password = userDto.Password;
                _context.Entry(firstOrDefaultAsync).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CourseDTO>> GetUserCourses(int id)
        {
            var user = await _context.User
                .Include(e => e.Courses)
                .FirstOrDefaultAsync(e => e.Id == id);

            List<CourseDTO> courseDTOs = user.Courses.Select(course => new CourseDTO
            {
                Id = course.Id,
                ImageUrl = course.ImageUrl,
                Description = course.Description,
                Price = course.Price
            }).ToList();

            return courseDTOs;
        }

        public async Task AddCourseToUser( int userId , int courseId)
        {
            var findAsync = await _context.Course.FindAsync(courseId);

            var user = await _context.User.Include(e => e.Courses).FirstOrDefaultAsync(e => e.Id == userId);

            user.Courses.Add(findAsync);

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
