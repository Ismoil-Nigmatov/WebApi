using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using System.Security.Claims;
using JFA.DependencyInjection;
using Task = System.Threading.Tasks.Task;

namespace project.repository;
[Scoped]
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var userDtos = await _context.User
            .Include(e => e.Courses)
            .Select(e => new UserDTO()
            {
                FullName = e.FullName,
                Email = e.Email,
                CourseIds = e.Courses.Select(course => course.Id).ToList()
            })
            .ToListAsync();

        return userDtos;
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        var firstOrDefaultAsync = await _context.User.Include(e=> e.Courses).FirstOrDefaultAsync(e => e.Id == id);
        UserDTO userDto = new UserDTO();
        userDto.FullName = firstOrDefaultAsync.FullName;
        userDto.Email = firstOrDefaultAsync.Email;
        userDto.CourseIds = firstOrDefaultAsync.Courses.Select(course => course.Id).ToList();

        return userDto;
    }

    public async Task AddUserAsync(User user)
    {
        _context.User.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserDTO userDto)
    {
        var myId = GetMyId();
        var firstOrDefaultAsync = await _context.User.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(myId));

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

    public async Task<List<CourseDTO>> GetUserCourses()
    {
        var myId = GetMyId();
        var user = await _context.User
            .Include(e => e.Courses)
            .FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(myId));

        List<CourseDTO> courseDTOs = user.Courses.Select(course => new CourseDTO
        {
            Id = course.Id,
            ImageUrl = course.ImageUrl,
            Description = course.Description,
            Price = course.Price
        }).ToList();

        return courseDTOs;
    }

    public async Task AddCourseToUser(int courseId)
    {
        var myId = GetMyId();
        var findAsync = await _context.Course.FindAsync(courseId);

        var user = await _context.User
            .Include(e => e.Courses)
            .FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(myId));

        user.Courses.Add(findAsync);

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public string GetMyId() 
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext is not null)
        {
            result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        return result;
    }
}