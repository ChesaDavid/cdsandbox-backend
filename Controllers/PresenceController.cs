using cdsandbox.Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace cdsandbox.Backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using cdsandbox.Backend.Models;

[ApiController]
[Route("api/[controller]")]
public class PresenceController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PresenceController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet("active-users")]
    public async Task<IActionResult> GetActiveUsers()    {
        var users = await _context.Users
            .Select(u => new 
            { 
                u.Username, 
                u.Color, 
                u.IsAI,
                u.Email
            })
            .ToListAsync();

        return Ok(users);
    }

}