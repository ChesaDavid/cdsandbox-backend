using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cdsandbox.Backend.Data;   
namespace cdsandbox.Backend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ProjectsController(ApplicationDbContext context)
    {
        _context = context;
    }
   
    [HttpPost("make")]
    public async Task<IActionResult> MakeProject([FromBody] DTOs.ProjectRequest request)
    {
        var project = new Models.Project
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Users = new List<Models.User>
            {
                
            },
            DockerContainer: new Models.DockerContainer
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.DockerContainer.Name,
                Image = request.DockerContainer.Image,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
        _context.Project.Add(project);
        await _context.SaveChangesAsync();
        return Ok(project);
    }

}