using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository;
[Scoped]
public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetAllCourseAsync()
    {
        return await _context.Course.ToListAsync();
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        return await _context.Course.FirstOrDefaultAsync(course => course.Id == id) ?? throw new BadHttpRequestException("Not Found");
    }

    public async Task AddCourseAsync(CourseDTO courseDto)
    {
        Course course = new Course();
        course.ImageUrl = courseDto.ImageUrl;
        course.Description = courseDto.Description;
        course.Price = courseDto.Price;
        _context.Course.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCourseAsync(CourseDTO courseDto)
    {
        var findAsync = await _context.Course.FindAsync(courseDto.Id);
        if (findAsync != null)
        {
            findAsync.ImageUrl = courseDto.ImageUrl;
            findAsync.Description = courseDto.Description;
            findAsync.Price = courseDto.Price;
            _context.Entry(findAsync).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _context.Course.FindAsync(id);
        if (course != null)
        {
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}