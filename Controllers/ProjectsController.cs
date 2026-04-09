using cdsandbox.Backend.Data;
using cdsandbox.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cdsandbox.Backend.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProjectsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] Project project)
    {
        
        if (string.IsNullOrEmpty(project.Name))
        {
            return BadRequest("Project name is required");
        }
        Console.WriteLine("Starting creating project {0}", project.Name);
        if (string.IsNullOrEmpty(project.EntryCode))
        {
            return BadRequest("Entry Code is required");
        }
        Console.WriteLine("Creating project {0}", project.Name);
        project.CreatedAt = DateTime.UtcNow;
        try
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return Ok(project);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("user/{ownerId}")]
    public async Task<IActionResult> GetUserProjects([FromRoute] string ownerId)
    {
        var projects = await _context.Projects
            .Where(p => p.OwnerId == ownerId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
        return Ok(projects);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(string id, [FromBody] Project projectUpdate)
    {
        var existingProject = await _context.Projects.FindAsync(id);

        if (existingProject == null)
        {
            return NotFound("Project not found.");
        }
        existingProject.Description = projectUpdate.Description;
        existingProject.EntryCode = projectUpdate.EntryCode;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(existingProject);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error updating project: {ex.Message}");
        }
    }
}
