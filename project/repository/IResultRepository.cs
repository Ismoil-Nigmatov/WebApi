using project.Dto;

namespace project.repository
{
    public interface IResultRepository
    {
        Task<List<ResultDTO>> GetAllResultAsync();
        Task<ResultDTO> GetResultByIdAsync(int id);
        Task AddResultAsync(ResultDTO resultDto);
        Task DeleteResultAsync(int id);
    }
}
