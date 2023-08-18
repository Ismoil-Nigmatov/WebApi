using Microsoft.IdentityModel.Tokens;
using project.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.repository;

namespace project.Service
{
    public class JWTService
    {

        public readonly IUserRepository _userRepository;

        public JWTService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Registration(UserDTO userDto)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            User user = new User();
            user.FullName = userDto.FullName;
            user.Password = passwordHash;
            user.Email = userDto.Email;

            _userRepository.AddUserAsync(user);
            return user;
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("asfsafsasafjsafjksafksafsafsafsafasfasfafasfsafasfsafsafassaf"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(7),
                signingCredentials: creds,
                issuer: "http://localhost:5069/"
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string Login(LoginDTO loginDto)
        {
            if (loginDto.Email != null)
            {
                var user = _userRepository.GetUserByEmail(loginDto.Email);

                var verify = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Result?.Password);
                if (verify)
                {
                    if (user.Result != null) return CreateToken(user.Result);
                }
                return "wrong";
            }

            return "wrong";
        }

    }
}
