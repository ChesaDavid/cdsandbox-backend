    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    namespace cdsandbox.Backend.Models;

    [Table("Users")]
    public class User
    {
        [Key] [Column("id")] public string Id { get; set; } = Guid.NewGuid().ToString("N");
        
        [Column("Email")]
        public string Email { get; set; } = string.Empty;
        
        [Column("PasswordHash")]
        public string PasswordHash { get; set; } = string.Empty;
        
        [Column("Color")]
        public string Color { get; set; } = "#3d86f7";
        [Column("IsAI")]
        public bool IsAI { get; set; } = false;
        [Column("Username")]
        public string Username { get; set; } = string.Empty;
    }