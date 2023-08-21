using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository;

public class HomeworkRepository : IHomeworkRepository
{

    private readonly AppDbContext _context;

    public HomeworkRepository(AppDbContext context) => _context = context;


    public async Task<List<HomeworkDTO>> GetAllHomeworkAsync()
    {
        var homeworkDtos = await _context.Homework
            .Include(e => e.Task)
            .Select(e => new HomeworkDTO()
            {
                Id = e.Id,
                Description = e.Description, 
                TaskId = e.Task.Id,
                ImageUrl = e.ImageUrl
            })
            .ToListAsync();

        return homeworkDtos;
    }

    public async Task<HomeworkDTO> GetHomeworkByIdAsync(int id)
    {
        var firstOrDefaultAsync = await _context.Homework
            .Include(e => e.Task)
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");

        HomeworkDTO homeworkDto = new HomeworkDTO();
        homeworkDto.Id = id;
        homeworkDto.Description = firstOrDefaultAsync.Description;
        homeworkDto.ImageUrl = firstOrDefaultAsync.ImageUrl;
        homeworkDto.TaskId = firstOrDefaultAsync.Task.Id;

        return homeworkDto;
    }

    public async Task AddHomeworkAsync(HomeworkDTO homeworkDto)
    {
        Homework homework = new Homework();
        homework.Description = homeworkDto.Description;
        homework.ImageUrl = homeworkDto.ImageUrl;
        homework.Task = await _context.Task.FindAsync(homeworkDto.TaskId) ?? throw new BadHttpRequestException("Task not found");

        _context.Homework.Add(homework);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateHomeworkAsync(HomeworkDTO homeworkDto)
    {
        var findAsync = await _context.Homework.FindAsync(homeworkDto.Id) ?? throw new BadHttpRequestException("Homework not found");
        findAsync.Description = homeworkDto.Description;
        findAsync.ImageUrl = homeworkDto.ImageUrl;
        findAsync.Task = await _context.Task.FindAsync(homeworkDto.TaskId) ?? throw new BadHttpRequestException("Task not found");

        _context.Entry(findAsync).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteHomeworkAsync(int id)
    {
        var homework = await _context.Homework.FindAsync(id);
        if (homework != null)
        {
            _context.Homework.Remove(homework);
            await _context.SaveChangesAsync();
        }
    }
}