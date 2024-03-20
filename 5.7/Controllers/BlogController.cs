using Microsoft.AspNetCore.Mvc;
using _5._7.Models.Services;

namespace video_blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogServices _blogServices;
        public BlogController(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }
        public IActionResult Index()
        {
            var posts = _blogServices.GetLatestPosts(10);
            return View("Index", posts); // Devolver una vista con los posts
        }
        public IActionResult Archive(int year, int month)
        {
            var posts = _blogServices.GetPostsByDate(year, month);
            return View(posts); // Devolver una vista con los posts
        }

        //[Route("blog/{slug}")] // Ruta original
        [Route("view-post/{slug}")] // 5.8c: Cambio de ruta de acceso a los posts individuales -> En Index.cshtml dejarán de funcionar los enlaces harcodeados
        public IActionResult ViewPost(string slug)
        {
            var post = _blogServices.GetPost(slug);
            if (post is null)
            {
                return NotFound();
            }
            return View(post);
        }
    }
}