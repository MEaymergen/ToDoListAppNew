using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAppWebApi.Models;
using static ToDoAppWebApi.Data.AuthDbContext;

namespace ToDoAppWebApi.Controllers
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
        public async Task<IActionResult> Register([FromBody] User model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email || u.UserName == model.UserName))
            {
                return BadRequest("Username or email already in use.");
            }
            if (string.IsNullOrEmpty(model.UserName))
            {
                return BadRequest("Username is required.");
            }

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Basit bir başarı mesajı dönelim, JWT token oluşturmaya gerek yok
            return Ok(new { message = "Login Successfull!",
                user = new
                {
                    userId = user.Id,
                    email = user.Email
                }
            });
        }
    }
}
