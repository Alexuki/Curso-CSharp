using Microsoft.AspNetCore.Mvc;

namespace Lab01.Features.Math.Controllers;

public class MathController: Controller
{
    [Route("math/multiplication/{number:int}")]
    public IActionResult Multiplication(int number)
    {
        return View(number);
    }
}