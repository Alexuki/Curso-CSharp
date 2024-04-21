using Microsoft.AspNetCore.Mvc;

public class ProductsController : Controller
{
    [Route("/products/{category}/{id}")]
    public IActionResult Show(int id, string category)
    {
        return Content($"Showing product {id} in category {category}");
    }
}