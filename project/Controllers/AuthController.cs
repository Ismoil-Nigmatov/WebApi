
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
        private readonly JWTService _jwtService;

        public AuthController(IConfiguration configuration, IUserRepository userRepository,JWTService jwtService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
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
            return Ok(_jwtService.Registration(request));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            try
            {
                return Ok(_jwtService.Login(request));
            }
            catch (Exception e)
            {
                return BadRequest("Incorrect User or Password");
            }
        }

    }
}
