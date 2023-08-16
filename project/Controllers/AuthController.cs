
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project.Data;
using project.Dto;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;

        public AuthController(IConfiguration configuration, IUserRepository userRepository,AppDbContext context)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _context = context;
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
                string token = CreateToken(user);
                return Ok(token);
            }
            else
            {
                return BadRequest("Wrong password");
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my top secretdhhdhdhdhheehchecf,;mnklbcvmnlkcmcvklnmcvknmcvknmcvknmcvlncvmknlcvnk"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds,
                issuer:"asd"
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
