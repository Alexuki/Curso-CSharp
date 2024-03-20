using Lab01.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab01.Models.Repositories.Database;

public class BlogDataContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=" + Path.Combine(AppContext.BaseDirectory, "Blog.db"));
    }
}