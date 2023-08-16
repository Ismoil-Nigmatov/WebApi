using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public class LessonRepository : ILessonRepository
    {

        private readonly AppDbContext _context;

        public LessonRepository(AppDbContext context) => _context = context;

        public async Task<List<LessonDTO>> GetAllLessonAsync()
        {
            var lessonDtos = await _context.Lesson
                .Include(e => e.Course)
                .Select(e => new LessonDTO()
                {
                    Id = e.Id,
                    Title = e.Title,
                    CourseId = e.Course.Id,
                    VideoUrl = e.VideoUrl,
                    Information = e.Information
                })
                .ToListAsync();
            return lessonDtos;
        }

        public async Task<LessonDTO> GetLessonByIdAsync(int id)
        {
            var firstOrDefaultAsync = await _context.Lesson
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
            LessonDTO lessonDto = new LessonDTO();
            lessonDto.Id = id;
            lessonDto.Title = firstOrDefaultAsync.Title;
            lessonDto.Information = firstOrDefaultAsync.Information;
            lessonDto.VideoUrl = firstOrDefaultAsync.VideoUrl;
            lessonDto.CourseId = firstOrDefaultAsync.Course.Id;

            return lessonDto;

        }

        public async Task AddLessonAsync(LessonDTO lessonDto)
        {
            Lesson lesson = new Lesson();
            lesson.Title = lessonDto.Title;
            lesson.Information = lessonDto.Information;
            lesson.VideoUrl = lessonDto.VideoUrl;
            lesson.Course = await _context.Course.FindAsync(lessonDto.CourseId);
            _context.Lesson.Add(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLessonAsync(LessonDTO lessonDto)
        {
            var findAsync = await _context.Lesson.FindAsync(lessonDto.Id);
            if (findAsync != null)
            {
                findAsync.Title = lessonDto.Title;
                findAsync.Information = lessonDto.Information;
                findAsync.VideoUrl = lessonDto.VideoUrl;
                findAsync.Course = await _context.Course.FindAsync(lessonDto.CourseId);
                _context.Entry(findAsync).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteLessonAsync(int id)
        {
            var lesson = await _context.Lesson.FindAsync(id);
            if (lesson != null)
            {
                _context.Lesson.Remove(lesson);
                await _context.SaveChangesAsync();
            }
        }
    }
}
