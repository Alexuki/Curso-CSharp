using _5._7.Models.Entities;
using _5._7.Models.Services;

namespace _5._7.Models.Services
{
    public class BlogServices : IBlogServices
    {
        /*
        Para este ejemplo, creamos una colección de Post en memoria cuyos valores se dan en el constructor.
        */
        private static readonly List<Post> Posts;

        static BlogServices()
        {
            Posts = new List<Post>
        {
            new Post() {Title = "Welcome to MVC", Slug = "welcome-to-mvc",
                        Author = "jmaguilar", Text = "Hi! Welcome to MVC!",
                        Date = new DateTime(2016, 01, 01)},
            new Post() {Title = "Second post", Slug = "second-post",
                        Author = "jmaguilar", Text = "Hello, this is my second post :)",
                        Date = new DateTime(2016, 01, 10)},
            new Post() {Title = "Another post", Slug = "another-post",
                        Author = "jmaguilar", Text = "Wow, this is my third post!",
                        Date = new DateTime(2016, 03, 15)},
        };

            for (int i = 1; i < 5; i++)
            {
                Posts.Add(
                    new Post()
                    {
                        Title = $"Post number {i}",
                        Slug = $"Post-number-{i}",
                        Author = "jmaguilar",
                        Text = $"Text of post #{i}",
                        Date = new DateTime(2016, 06, 01).AddDays(i)
                    }
                );
            }

            //5.8 Añadir comentarios a los Posts
            var rnd = new Random();
            foreach (var post in Posts)
            {
                for (int i = 0; i < rnd.Next(4); i++)
                {
                    post.Comments.Add(new Comment()
                    {
                        Author = $"user{rnd.Next(1000)}",
                        Date = post.Date.AddDays(rnd.Next(0, 100)),
                        Text = $"Hello, your post {post.Title} looks great!"
                    });
                }
            }


        }


        public IEnumerable<Post> GetLatestPosts(int max)
        {
            // Consulta LINQ: De los post de la colección, ordenarlos por fecha y no squedamos con ellos
            var posts = from post in Posts
                        orderby post.Date descending
                        select post;

            // Devolver solo parte de los elementos de la lista anterior
            return posts.Take(max).ToList();
        }

        /*
        La forma de obtener el post usando la condición post.Slug == slug no es la más óptima porque puede depender de condiciones culturales, o de mayúsculas y
        minúsculas que la comparación pueda fallar, por tanto se usa el método Equals.
        */
        public Post GetPost(string slug)
        {
            return Posts.FirstOrDefault(post => post.Slug.Equals(slug,
            StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<Post> GetPostsByDate(int year, int month)
        {
            var posts = from post in Posts
                        where post.Date.Year == year && post.Date.Month == month
                        orderby post.Date descending
                        select post;

            return posts.ToList();
        }
        
    }
}