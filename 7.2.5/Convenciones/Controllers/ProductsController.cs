using Microsoft.AspNetCore.Mvc;

public class ProductsController : Controller
{
    // Ruta: /Products/All
    // Ruta: /
    public IActionResult Index() => Content("ProductsController.Index()");

    // Ruta: /Products/Lenovo-yoga
    public new IActionResult View(string id) => Content($"ProductsController.View('{id}')");

    // Ruta: /Products/Category/Ultrabooks
    public IActionResult ByCategory(string category) => Content($"ProductsController.ByCategory('{category}')");

}