using Microsoft.AspNetCore.Mvc;

public class BindController : Controller
{
    [Route("/bind/test/{i}")]
    public IActionResult Test(int i, bool b, string s, double d, int[] array)
    {
        return Content($"i={i}, b={b}, s={s}, d={d}, array={string.Join(",", array)}");
    }
}