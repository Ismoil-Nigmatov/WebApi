﻿using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{

    public class ResultRepository : IResultRepository
    {
        private readonly AppDbContext _context;

        public ResultRepository(AppDbContext context) => _context = context;

        public async Task<List<ResultDTO>> GetAllResultAsync()
        {
            var resultDtos = await _context.Result
                .Include(e => e.User)
                .Include(e => e.Education)
                .Select(e => new ResultDTO()
                {
                    Id = e.Id,
                    Url = e.Url,
                    UserId = e.User.Id,
                    EducationId = e.Education.Id
                })
                .ToListAsync();

                return resultDtos;
        }

        public async Task<ResultDTO> GetResultByIdAsync(int id)
        {
            var firstOrDefaultAsync = await _context.Result
                .Include(e => e.User)
                .Include(e => e.Education)
                .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");

            ResultDTO resultDto = new ResultDTO();
            resultDto.Id = id;
            resultDto.Url = firstOrDefaultAsync.Url;
            resultDto.UserId = firstOrDefaultAsync.User.Id;
            resultDto.EducationId = firstOrDefaultAsync.Education.Id;

            return resultDto;
        }

        public async Task AddResultAsync(ClaimsPrincipal principal ,ResultDTO resultDto)
        {
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Result result = new Result();
        result.Url = resultDto.Url;
        result.Education = await _context.Education.FindAsync(resultDto.EducationId) ?? throw new BadHttpRequestException("Education not found");
        result.User = await _context.User.FindAsync(Convert.ToInt32(userId)) ?? throw new BadHttpRequestException("User not found");
        _context.Result.Add(result);
        await _context.SaveChangesAsync();
        }

        public async Task DeleteResultAsync(int id)
        {
            var result = await _context.Result.FindAsync(id);
            if (result != null)
            {
                _context.Result.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}
