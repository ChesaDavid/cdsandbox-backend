using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cdsandbox.Backend.Data;
using cdsandbox.Backend.DTOs;
using BCrypt.Net;

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
}