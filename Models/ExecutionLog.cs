namespace cdsandbox.Backend.Models;

public class ExecutionLog
{
    public int Id { get; set; }
    
    public string Output { get; set; } = string.Empty;
    public int ProjectFileId { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string StreamType { get; set; } = "stdout"; 
}