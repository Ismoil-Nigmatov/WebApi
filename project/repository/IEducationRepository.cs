using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public interface IEducationRepository
    {
        Task<List<EducationDTO>> GetAllEducationAsync();
        Task<EducationDTO> GetEducationByIdAsync(int id);
        Task AddEducationAsync(EducationDTO educationDto);
        Task UpdateEducationAsync(EducationDTO educationDto);
        Task DeleteEducationAsync(int id);
    }
}
