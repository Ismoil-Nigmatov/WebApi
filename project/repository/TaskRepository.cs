using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity.ENUMS;

namespace project.repository;
[Scoped]
public class TaskRepository : ITaskRepository
{

    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<List<TaskDTO>> GetAllTaskAsync()
    {
        var taskDtos = await _context.Task
            .Include(e => e.Lesson)
            .Select(e => new TaskDTO()
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                LessonId = e.Lesson.Id,
                DateTime = e.DateTime,
                Process = e.Process
            })
            .ToListAsync();

        return taskDtos;
    }

    public async Task<TaskDTO> GetTaskByIdAsync(int id)
    {
        var firstOrDefaultAsync = await _context.Task
            .Include(e => e.Lesson)
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");

        TaskDTO taskDto = new TaskDTO();
        taskDto.Id = id;
        taskDto.Description = firstOrDefaultAsync.Description;
        taskDto.Title = firstOrDefaultAsync.Title;
        taskDto.LessonId = firstOrDefaultAsync.Lesson.Id;
        taskDto.DateTime = firstOrDefaultAsync.DateTime;
        taskDto.Process = firstOrDefaultAsync.Process;

        return taskDto;
    }

    public async Task AddTaskAsync(TaskDTO taskDto)
    {
        Entity.Task task = new Entity.Task();
        task.Title = taskDto.Title;
        task.Description = taskDto.Description;
        task.Lesson = await _context.Lesson.FindAsync(taskDto.LessonId);
        task.Process = EProcess.NOTSTARTED;
        _context.Task.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskDTO taskDto)
    {
        var findAsync = await _context.Task.FindAsync(taskDto.Id);
        if (findAsync != null)
        {
            findAsync.Title = taskDto.Title;
            findAsync.Description = taskDto.Description;
            _context.Entry(findAsync).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _context.Task.FindAsync(id);
        if (task != null)
        {
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateTaskProcess(int id, EProcess process)
    {
        var findAsync = await _context.Task.FindAsync(id);
        if (findAsync != null)
        {
            findAsync.Process = process;
            findAsync.DateTime = DateTime.UtcNow.AddDays(3);
            _context.Entry(findAsync).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}