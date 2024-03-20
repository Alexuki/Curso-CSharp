using Lab01.Models.Entities;

namespace Lab01.Models.Repositories.InMemory;

public class InMemoryPostsRepository: IPostsRepository
{
    private static List<Post> Posts { get; set; }
    static InMemoryPostsRepository()
    {
        Posts = DataInitializer.CreateFakePostsWithComments("memory");
    }

    public IEnumerable<Post> AllPostsWithComments()
    {
        return Posts.AsQueryable();
    }
}