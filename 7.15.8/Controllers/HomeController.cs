using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _7._15._8.Models;

namespace _7._15._8.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [ResponseCache(Duration = 10)] //(18)
    public IActionResult Index()
    {
        return View();
    }

    [SimpleCache]
    public IActionResult GetTime()
    {
        return Content(DateTime.Now.ToString("T"));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
