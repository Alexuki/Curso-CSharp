using Lab01.Models.Entities;

namespace Lab01.Models.Repositories;

public static class DataInitializer
{
    public static List<Post> CreateFakePostsWithComments(string source)
    {
        var posts = new List<Post>
        {
            new Post()
            {
                Title = $"Welcome to MVC ({source})", Slug = "welcome-to-MVC", Author = "jmaguilar", Text = "Hi! Welcome to MVC!", Date = new DateTime(2016, 01, 01)
            },
            new Post() { Title = $"Second post ({source})", Slug = "second-post", Author = "jmaguilar", Text = "This is my second post :)", Date = new DateTime(2016, 01, 10)},
            new Post() { Title = $"Another post ({source})", Slug = "another-post", Author = "jmaguilar", Text = "Wow, this is my third post!", Date = new DateTime(2016, 03, 15)},
        };
        for (int i = 1; i < 5; i++)
        {
            posts.Add(new Post() { Title = $"Post number {i} ({source})", Slug = $"post-number-{i}", Author = "jmaguilar", Text = $"Text of post #{i}", Date = new DateTime(2016, 06, 01).AddDays(i) });
        }

        var rnd = new Random();
        foreach (var post in posts)
        {
            for (int i = 0; i < rnd.Next(4); i++)
            {
                post.Comments.Add(new Comment()
                {
                    Author = $"user{rnd.Next(1000)}",
                    Date = post.Date.AddDays(rnd.Next(0, 100)),
                    Text = $"Hello, your post {post.Title} looks great!",
                });
            }
        }
        return posts;

    }
}