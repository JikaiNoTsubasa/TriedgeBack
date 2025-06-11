using Microsoft.EntityFrameworkCore;
using triedge_api.Database.Models;

namespace triedge_api.Database;

public class TriContext(DbContextOptions<TriContext> options) : DbContext(options)
{

    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
        modelBuilder.Entity<User>().HasMany(u => u.Blogs).WithOne(b => b.Owner).HasForeignKey(b => b.OwnerId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Blog>().HasIndex(b => b.Identifier).IsUnique();
        modelBuilder.Entity<Blog>().HasIndex(b => b.Slug).IsUnique();
        modelBuilder.Entity<Blog>().HasMany(b => b.Categories).WithMany(c => c.Blogs);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));
        
        IConfiguration Config = configBuilder.Build();
        string connectionString = Config.GetConnectionString("Default") ?? "";

        string centralConnectionString = Config.GetConnectionString("Default") ?? "";
        optionsBuilder.UseNpgsql(connectionString);
    }
}