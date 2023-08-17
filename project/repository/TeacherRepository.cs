using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public class TeacherRepository : ITeacherRepository
    {

        private readonly AppDbContext _context;

        public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Teacher>> GetAllTeacherAsync()
        {
           return await _context.Teacher.ToListAsync();
        }

        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {
            return await _context.Teacher.FirstOrDefaultAsync(teacher => teacher.Id == id) ?? throw new BadHttpRequestException("Teacher not found");
        }

        public async Task AddTeacherAsync(TeacherDTO teacherDto)
        {
            Teacher teacher = new Teacher();
            teacher.Name = teacherDto.Name;
            teacher.ImageUrl = teacherDto.ImageUrl;
            teacher.Type = teacherDto.Type;
            _context.Teacher.Add(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeacherAsync(TeacherDTO teacherDto)
        {
            var findAsync = await _context.Teacher.FindAsync(teacherDto.Id);
            if (findAsync != null)
            {
                findAsync.ImageUrl = teacherDto.ImageUrl;
                findAsync.Name = teacherDto.Name;
                findAsync.Type = teacherDto.Type;
                _context.Entry(findAsync).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var teacher= await _context.Teacher.FindAsync(id);
            if (teacher != null)
            {
                _context.Teacher.Remove(teacher);
                await _context.SaveChangesAsync();
            }
        }
    }
}
