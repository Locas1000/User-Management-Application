using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Task4.Server.Data;
using Task4.Server.Models;

namespace Task4.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Status = "Active",
                LastLoginTime = DateTime.UtcNow,
                RegistrationTime = DateTime.UtcNow
            };

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "This email is already taken." });
            }

            return Ok(new { message = "Registration successful", userId = user.Id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var hasedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.PasswordHash == hashedPassword);

            if (user == null) return Unauthorized(new { message = "Invalid email or password." });
            if (user.Status == "Blocked") return Unauthorized(new { message = "Your account is blocked." });

            user.LastLoginTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new { token = "dummy-token-" + user.Id, user = new { user.Id, user.Name, user.Email, user.Status } });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }

    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}