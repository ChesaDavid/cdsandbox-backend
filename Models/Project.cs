using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdsandbox.Backend.Models;

[Table("Projects")]
public class Project
{
    [Key]
    [Column("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    
    [Column("Name")]
    public string Name { get; set; } = string.Empty;

    [Column("Description")]
    public string Description { get; set; } = string.Empty;

    [Column("OwnerId")]
    public string OwnerId { get; set; } = string.Empty; 

    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("EntryCode")]
    public string EntryCode { get; set; } = string.Empty;

   }