using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cdsandbox.Backend.Data;
using cdsandbox.Backend.Models;
using BCrypt.Net;
using LoginRequest = cdsandbox.Backend.DTOs.LoginRequest;
using RegisterRequest = cdsandbox.Backend.DTOs.RegisterRequest;

namespace cdsandbox.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid email or password.");
        }
        
        return Ok(new {
            user.Id,
            user.Username,
            user.Email,
            user.Color
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Username))
        {
            return BadRequest("Email, password, and username are required.");
        }

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
        {
            return BadRequest("Email already in use.");
        }
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = request.Email,
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Color = "#3d86f7",
            IsAI = false
            
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new {
            user.Id,
            user.Username,
            user.Email,
            user.Color
        });
    }
}
