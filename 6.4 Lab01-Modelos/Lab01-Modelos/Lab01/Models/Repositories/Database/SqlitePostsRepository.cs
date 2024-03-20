using Lab01.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab01.Models.Repositories.Database;

public class SqlitePostsRepository : IPostsRepository
{
    private readonly BlogDataContext _ctx;

    public SqlitePostsRepository(BlogDataContext ctx)
    {
        _ctx = ctx;
    }

    public IEnumerable<Post> AllPostsWithComments()
    {
        return _ctx.Posts.Include(p => p.Comments);
    }

    public static void InitializeDatabase()
    {
        // Start with a clean database
        using var ctx = new BlogDataContext();
        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();
        ctx.Posts.AddRange(DataInitializer.CreateFakePostsWithComments("Sqlite"));
        ctx.SaveChanges();
    }

}