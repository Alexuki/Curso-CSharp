using Microsoft.AspNetCore.Mvc;
public class HomeController : Controller
{
    public IActionResult Index() => Content("Hello from Home/Index");
    // Si no se provee otra ruta, la llamada a localhost:xxxx va al controlador Home, y a su acción Index
}