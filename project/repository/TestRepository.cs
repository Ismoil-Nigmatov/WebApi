using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public class TestRepository : ITestRepository
    {

        private readonly AppDbContext _context;

        public TestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Test>> GetAllTestAsync()
        {
            return await _context.Test.ToListAsync();
        }

        public async Task<Test> GetTestByIdAsync(int id)
        {
           return await _context.Test.FirstOrDefaultAsync(t => t.Id == id) ?? throw new BadHttpRequestException("Not Found");
        }

        public async Task AddTestAsync(TestDTO testDto)
        {
            Test test = new Test();
            test.Question = testDto.Question;
            test.Options = testDto.Options;
            test.RightOption = testDto.RightOption;
            _context.Test.Add(test);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTestAsync(TestDTO testDto)
        {
            var firstOrDefaultAsync = await _context.Test.FirstOrDefaultAsync(t => t.Id == testDto.Id);
            if (firstOrDefaultAsync != null)
            {
                firstOrDefaultAsync.Question = testDto.Question;
                firstOrDefaultAsync.Options = testDto.Options;
                firstOrDefaultAsync.RightOption = testDto.RightOption;
                _context.Entry(firstOrDefaultAsync).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTestAsync(int id)
        {
            var test = await _context.Test.FindAsync(id);
            if (test != null)
            {
                _context.Test.Remove(test);
                await _context.SaveChangesAsync();
            }
        }
    }
}
