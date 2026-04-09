using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cdsandbox.Backend.Data;
using BCrypt.Net;
using cdsandbox.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = cdsandbox.Backend.DTOs.LoginRequest;

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

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Console.WriteLine($"Login attempt for: {request.Email}");
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());
        if (user == null)
        {
            Console.WriteLine("--- Error: User not found.");
            return Unauthorized("Invalid email or password.");
        }
        bool isPassworValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPassworValid)
        {
            Console.WriteLine("--- Failure: Password invalid.");
            return Unauthorized("Invalid email or password.");
        }
        Console.WriteLine("--- Success: User successfully logged in.");
        return Ok(new {
            user.Id,
            user.Username,
            user.Email,
            user.Color
        });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] cdsandbox.Backend.DTOs.RegisterRequest request)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());
        if (existingUser != null)
        {
            return BadRequest(new {message = "User already exists!"});
        }
        var newUser = new User
        {   
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Color = "#3d86f7"
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully!" });
    }
}