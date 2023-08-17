﻿using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using System.Security.Claims;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == id) ?? throw new BadHttpRequestException("User not found");
        }

        public async Task AddUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            var myId = GetMyId();
            var firstOrDefaultAsync = await _context.User.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(myId));

            if (firstOrDefaultAsync != null)
            {
                firstOrDefaultAsync.FullName = userDto.FullName;
                firstOrDefaultAsync.Email = userDto.Email;
                firstOrDefaultAsync.Password = userDto.Password;
                _context.Entry(firstOrDefaultAsync).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CourseDTO>> GetUserCourses()
        {
            var myId = GetMyId();
            var user = await _context.User
                .Include(e => e.Courses)
                .FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(myId))   ?? throw new BadHttpRequestException("User Not found");

            List<CourseDTO> courseDtos = user.Courses.Select(course => new CourseDTO
            {
                Id = course.Id,
                ImageUrl = course.ImageUrl,
                Description = course.Description,
                Price = course.Price
            }).ToList();

            return courseDtos;
        }

        public async Task AddCourseToUser(int courseId)
        {
            var myId = GetMyId();
            var findAsync = await _context.Course.FindAsync(courseId)?? throw new BadHttpRequestException("Course not found");

            var user = await _context.User.Include(e => e.Courses).FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(myId)) ?? throw new BadHttpRequestException("User not found");

            user.Courses.Add(findAsync);

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public string GetMyId()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext is not null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return result ?? throw new BadHttpRequestException("User Id not found");
        }
    }
}
