using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class FriendsController : Controller
{
    public IActionResult Create()
    {
        return View();
    }

    /* [HttpPost]
    public IActionResult Create(Friend friend)
    {
        //return Content($"Name= {friend.Name}, Age= {friend.Age}, Email= {friend.Email}");

        return Content($"Name= {friend.Name}, Age= {friend.Age}, Email= {friend.Email},"
            + $"Street= {friend.Address.Street}, City= {friend.Address.City}");

    } */


    // 10) Exigir bindeado del parámetro y comprobar estado
    [HttpPost]
    public IActionResult Create([BindRequired]Friend friend)
    {
        if(!ModelState.IsValid)
        {
            // Mostrar este texto si no hay un binding válido
            return Content("Error");
        }
        return Content($"Name= {friend.Name}, Age= {friend.Age}, Email= {friend.Email},"
            + $"Street= {friend.Address.Street}, City= {friend.Address.City}");
    }
}