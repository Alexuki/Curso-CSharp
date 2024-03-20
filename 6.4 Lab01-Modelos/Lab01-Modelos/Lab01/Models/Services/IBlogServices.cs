using Lab01.Models.Entities;

namespace Lab01.Models.Services;

public interface IBlogServices
{
    IEnumerable<Post> GetLatestPosts(int max);
    IEnumerable<Post> GetPostsByDate(int year, int month);
    Post GetPost(string slug);
    IEnumerable<ArchiveEntry> GetArchiveIndex();
}