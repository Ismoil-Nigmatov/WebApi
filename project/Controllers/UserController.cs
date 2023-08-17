using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.repository;
using Task = project.Entity.Task;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserDTO userDto)
        {
            await _userRepository.UpdateUserAsync(userDto);
            return Ok();
        }

        [HttpPut("{courseId}")]
        public async Task<ActionResult> AddCourse(int courseId)
        {
            await _userRepository.AddCourseToUser(courseId);
            return Ok();
        }

        [HttpGet("courses")]
        public async Task<ActionResult> GetUserCourses()
        {
            var userCourses = await _userRepository.GetUserCourses();
            return Ok(userCourses);
        }
    }
}
