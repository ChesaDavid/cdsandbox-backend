namespace cdsandbox.Backend.Models;

public class Project
{
    public string Id{ get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public List<User> Users { get; set; } = new();
    
}