using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public interface ITestRepository
    {
        Task<List<Test>> GetAllTestAsync();
        Task<Test> GetTestByIdAsync(int id);
        Task AddTestAsync(TestDTO testDto);
        Task UpdateTestAsync(TestDTO testDto);
        Task DeleteTestAsync(int id);
    }
}
