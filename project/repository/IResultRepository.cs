using System.Security.Claims;
using project.Dto;

namespace project.repository
{
    public interface IResultRepository
    {
        Task<List<ResultDTO>> GetAllResultAsync();
        Task<ResultDTO> GetResultByIdAsync(int id);
        Task AddResultAsync(ClaimsPrincipal claims , ResultDTO resultDto);
        Task DeleteResultAsync(int id);
    }
}
