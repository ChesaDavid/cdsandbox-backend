namespace cdsandbox.Backend.Models;

public class User
{
    public int id { get; set; }
    public string Email { get; set; } = string.Empty;
    
    public string PasswordHash { get; set; } = string.Empty;
    
    public string Color { get; set; } = "#3d86f7";
    public bool IsAI { get; set; } = false;
    public string Username { get; set; } = string.Empty;
}