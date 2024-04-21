using Microsoft.AspNetCore.Mvc;

public class MathController : Controller
{
    [Route("/math/multiplication/{n}")]
    public IActionResult Multiplication(int n)
    {
        return View(n);
    }
}