using Microsoft.EntityFrameworkCore;
using cdsandbox.Backend.Models; // Ensure this matches your namespace

namespace cdsandbox.Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ProjectFile> ProjectFiles { get; set; }
    public DbSet<CodeBlock> CodeBlocks { get; set; }
    public DbSet<ExecutionLog> ExecutionLogs { get; set; } 
    public DbSet<Project> Project { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectFile>()
            .HasMany(f => f.Blocks)
            .WithOne()
            .HasForeignKey(b => b.ProjectFileId);

        // Configure User entity with PascalCase column names to match database
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).HasColumnName("Id");
            entity.Property(u => u.Email).HasColumnName("Email");
            entity.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
            entity.Property(u => u.Username).HasColumnName("Username");
            entity.Property(u => u.Color).HasColumnName("Color");
            entity.Property(u => u.IsAI).HasColumnName("IsAI");
        });
    }
    public DbSet<User> Users { get; set; }
}
