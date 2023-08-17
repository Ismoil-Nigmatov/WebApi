using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository;
[Scoped]
public class EducationRepository : IEducationRepository
{
    private readonly AppDbContext _context;

    public EducationRepository(AppDbContext context) => _context = context;

    public async Task<List<EducationDTO>> GetAllEducationAsync()
    {
        var educationDtos = await _context.Education
            .Include(e => e.Course)
            .Select(e => new EducationDTO
            {
                Id = e.Id,
                Title = e.Title,
                End = e.End,
                Description = e.Description,
                CourseId = e.Course.Id
            })
            .ToListAsync();

        return educationDtos;
    }

    public async Task<EducationDTO> GetEducationByIdAsync(int id)
    {
        var firstOrDefaultAsync = await _context.Education
            .Include(e => e.Course) // Eagerly load the Course navigation property
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");

        EducationDTO educationDto = new EducationDTO();
        educationDto.Id = id;
        educationDto.Title = firstOrDefaultAsync.Title;
        educationDto.Description = firstOrDefaultAsync.Description;
        educationDto.End = firstOrDefaultAsync.End;
        educationDto.CourseId = firstOrDefaultAsync.Course.Id;
        return educationDto;
    }

    public async Task AddEducationAsync(EducationDTO educationDto)
    {
        Education education = new Education();
        education.Description = educationDto.Description;
        education.End = educationDto.End;
        education.Title = educationDto.Title;
        education.Course = await _context.Course.FindAsync(educationDto.CourseId);
        Console.WriteLine(education.Course);
        _context.Education.Add(education);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEducationAsync(EducationDTO educationDto)
    {
        var findAsync = await _context.Education.FindAsync(educationDto.Id);
        if (findAsync != null)
        {
            findAsync.Description = educationDto.Description;
            findAsync.End = educationDto.End;
            findAsync.Title = educationDto.Title;
            findAsync.Course = await _context.Course.FindAsync(educationDto.CourseId);
            _context.Entry(findAsync).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteEducationAsync(int id)
    {
        var education = await _context.Education.FindAsync(id);
        if (education != null)
        {
            _context.Education.Remove(education);
            await _context.SaveChangesAsync();
        }
    }
}