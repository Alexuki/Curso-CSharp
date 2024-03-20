using Lab01.Models.Entities;
using Lab01.Models.Repositories;

namespace Lab01.Models.Services;

public class BlogServices : IBlogServices
{
    private readonly IPostsRepository _postsRepository;

    public BlogServices(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }

    public IEnumerable<Post> GetLatestPosts(int max)
    {
        var posts = from post in _postsRepository.AllPostsWithComments()
            orderby post.Date descending
            select post;
        return posts.Take(max).ToList();
    }

    public IEnumerable<Post> GetPostsByDate(int year, int month)
    {
        var posts = from post in _postsRepository.AllPostsWithComments()
            where post.Date.Month == month && post.Date.Year == year
            orderby post.Date descending
            select post;
        return posts.ToList();
    }

    public Post GetPost(string slug)
    {
        return _postsRepository.AllPostsWithComments()
            .FirstOrDefault(post => post.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
    }


    public IEnumerable<ArchiveEntry> GetArchiveIndex()
    {
        var d = from post in _postsRepository.AllPostsWithComments()
            group post by new { post.Date.Year, post.Date.Month } into p
            orderby p.Key.Year descending, p.Key.Month descending
            select new ArchiveEntry { Month = p.Key.Month, Year = p.Key.Year, PostCount = p.Count() };
        return d.ToList();
    }
}