using System.Diagnostics;
using Lab05.Extensions.Filters;
using Microsoft.AspNetCore.Mvc;
using Lab05.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lab05.Controllers;

//[Authorize(Policy="Family")] //(9)
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [ResponseCache(Duration = 10)] //(5)
    public IActionResult Index()
    {
        return View();
    }

    /*
    //(4)
    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }


    //(5)
    [Authorize(Roles="Admin")]
    public IActionResult Privacy()
    {
        return View();
    }


    //(7)
    [Authorize(Policy="Family")]
    public IActionResult Privacy()
    {
        return View();
    }
    */

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