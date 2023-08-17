
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project.Data;
using project.Dto;
using project.Entity;
using project.repository;
using project.Service;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;
        private readonly JWTService _jwtService;

        public AuthController(IConfiguration configuration, IUserRepository userRepository,AppDbContext context, JWTService jwtService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _context = context;
            _jwtService = jwtService;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMyName()
        {
            return Ok(_userRepository.GetMyId());

        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDTO request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User user = new User();
            user.FullName = request.FullName;
            user.Password = passwordHash;
            user.Email = request.Email;

            _userRepository.AddUserAsync(user);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            var user = await _context.User.FirstOrDefaultAsync(e => e.Email == request.Email);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var verify = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (verify)
            {
                string token = _jwtService.CreateToken(user);
                return Ok(token);
            }
            else
            {
                return BadRequest("Wrong password");
            }
        }

    }
}
