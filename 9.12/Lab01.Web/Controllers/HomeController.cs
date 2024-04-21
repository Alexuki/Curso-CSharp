using Microsoft.AspNetCore.Mvc;

namespace Lab01.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Error()
    {
        return View();
    }
}