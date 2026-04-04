namespace cdsandbox.Backend.Models;

public class CodeBlock
{
    public int Id { get; set; }
    
    public string Content { get; set; } = string.Empty;
    public string AuthorType { get; set; } = "Human";
    public string AuthorName { get; set; } = string.Empty;
    public int Order { get; set; }

    public bool IsAccepted { get; set; } = true;
    public int ProjectFileId { get; set; }
}