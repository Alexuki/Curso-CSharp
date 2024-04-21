using Microsoft.AspNetCore.Mvc;

public class FriendsController : Controller
{
    public IActionResult Create()
    {
        //return View();
        return View(new Friend()); //(10)
    }

    [HttpPost]
    public IActionResult Create(Friend friend)
    {
        if (!ModelState.IsValid)
        {
            //return Content("Not valid"); 
            //return View(); //(9)
            return View(friend); //(10)
        }
        return Content($"Name: {friend.Name}, Age: {friend.Age}, Email: {friend.Email}"); // Para ver el contenido
    }
}