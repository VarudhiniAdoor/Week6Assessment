using Microsoft.AspNetCore.Mvc;
using DeptApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeptApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly DeptDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(DeptDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var obj = _context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (obj == null) return Unauthorized();

            var tokenString = GenerateWebToken(obj);
            return Ok(new { token = tokenString });
        }

        private string GenerateWebToken(User user)
        {
            string role = _context.Roles.FirstOrDefault(r => r.RoleId == user.RoleId)?.RoleName ?? "User";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
