using Lab01.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab01.Controllers;

public class BlogController : Controller
{
    private readonly IBlogServices _blogServices;

    public BlogController(IBlogServices blogServices)
    {
        _blogServices = blogServices;
    }

    public IActionResult Index()
    {
        var posts = _blogServices.GetLatestPosts(10); // 10 posts más recientes
        return View(posts); // "Index" por defecto
    }

    [Route("blog/archive/{year}/{month}")]
    public IActionResult Archive(int year, int month)
    {
        var posts = _blogServices.GetPostsByDate(year, month);
        return View(posts);
    }

    [Route("blog/{slug}")]
    public IActionResult ViewPost(string slug)
    {
        var post = _blogServices.GetPost(slug);
        if (post == null)
            return NotFound();
        else
            return View(post);
    }

    [Route("blog/archiveindex")]
    public IActionResult ArchiveIndex()
    {
        var archiveIndex = _blogServices.GetArchiveIndex();
        return View(archiveIndex);
    }
}