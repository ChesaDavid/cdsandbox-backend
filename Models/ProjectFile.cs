namespace cdsandbox.Backend.Models;

public class ProjectFile
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty; 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<CodeBlock> Blocks { get; set; } = new();
}