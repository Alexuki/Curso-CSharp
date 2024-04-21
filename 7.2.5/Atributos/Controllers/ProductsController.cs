using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class ProductsController : Controller
{
    // Ruta: /Products/All
    // Ruta: /
    [Route("/")]
    [Route("all")]
    public IActionResult Index() => Content($"ProductsController.Index()");

    // Ruta: /Products/Lenovo-yoga
    [Route("{id}")]
    public IActionResult View(string id) => Content($"ProductsController.View('{id}')");

    // Ruta: /Products/Category/Computers
    [Route("category/{category:endsWith(ers)}")]
    public IActionResult ByCategory(string category) => Content($"ProductsController.ByCategory('{category}')");
}