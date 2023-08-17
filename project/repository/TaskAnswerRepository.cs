using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public class TaskAnswerRepository : ITaskAnswerRepository
    {
        private readonly AppDbContext _context;

        public TaskAnswerRepository(AppDbContext context) => _context = context;

        public async Task<List<TaskAnswerDTO>> GetAllTaskAnswerAsync()
        {
            var taskAnswerDtos = await _context.TaskAnswers
                .Include(e => e.Task)
                .Select(e => new TaskAnswerDTO()
                {
                    Id = e.Id,
                    Answer = e.Answer,
                    fileUrl = e.fileUrl,
                    TaskId = e.Task.Id
                })
                .ToListAsync();

            return taskAnswerDtos;
        }

        public async Task<TaskAnswerDTO> GetTaskAnswerByIdAsync(int id)
        {
            var firstOrDefaultAsync = await _context.TaskAnswers
                .Include(e => e.Task)
                .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("TaskAnswer not found");
            TaskAnswerDTO taskAnswerDto = new TaskAnswerDTO();
            taskAnswerDto.Id = firstOrDefaultAsync.Id;
            taskAnswerDto.Answer = firstOrDefaultAsync.Answer;
            taskAnswerDto.fileUrl = firstOrDefaultAsync.fileUrl;
            taskAnswerDto.TaskId = firstOrDefaultAsync.Task.Id;

            return taskAnswerDto;
        }

        public async Task AddTaskAnswerAsync(TaskAnswerDTO taskAnswerDto)
        {
            TaskAnswer taskAnswer = new TaskAnswer();
            taskAnswer.Answer = taskAnswerDto.Answer;
            taskAnswer.fileUrl = taskAnswerDto.fileUrl;
            taskAnswer.Task = await _context.Task.FindAsync(taskAnswerDto.TaskId) ?? throw new BadHttpRequestException("Task not found");

            _context.TaskAnswers.Add(taskAnswer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAnswerAsync(TaskAnswerDTO taskAnswerDto)
        {
            var findAsync = await _context.TaskAnswers.FindAsync(taskAnswerDto.Id);
            findAsync.fileUrl = taskAnswerDto.fileUrl;
            findAsync.Answer = taskAnswerDto.Answer;
            findAsync.Task = await _context.Task.FindAsync(taskAnswerDto.TaskId) ?? throw new BadHttpRequestException("Task not found");

            _context.Entry(findAsync).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var taskAnswer = await _context.TaskAnswers.FindAsync(id);
            if (taskAnswer != null)
            {
                _context.TaskAnswers.Remove(taskAnswer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
