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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectFile>()
            .HasMany(f => f.Blocks)
            .WithOne()
            .HasForeignKey(b => b.ProjectFileId);
    }
    public DbSet<User> Users { get; set; }
}