using Lab01.Models.Entities;

namespace Lab01.Models.Repositories;

public interface IPostsRepository
{
    IEnumerable<Post> AllPostsWithComments();
}