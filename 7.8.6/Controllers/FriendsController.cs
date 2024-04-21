using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class FriendsController : Controller
{
    public IActionResult Create()
    {
        var newDefaultFriend = new Friend()
        {
            Name = "Default name",
            Age = 26,
            Address = new Address()
        };

        return View(newDefaultFriend); // Envía este objeto a la vista
    }

    /* [HttpPost]
    public IActionResult Create(Friend friend)
    {
        if (!string.IsNullOrEmpty(friend.Name) && friend.Age >= 18)
        {
            var serializedFriend = JsonSerializer.Serialize(friend);
            return Content($"Created: {serializedFriend}");
        }
        ViewData["ErrorMessage"] = "Invalid data";
        return View(friend);
    } */

    [HttpPost]
    public IActionResult Create(Friend friend)
    {
        var error = false;
        if (string.IsNullOrEmpty(friend.Name))
        {
            error = true;
            ViewData["NameError"] = "Invalid name";
        }
        if (friend.Age < 18)
        {
            error = true;
            ViewData["AgeError"] = "Enter a valid age (>17)";
        }
        if (error)
        {
            ViewData["ErrorMessage"] = "Invalid data";
            return View(friend);
        }
        else
        {
            var serializedFriend = JsonSerializer.Serialize(friend);
            return Content($"Created: {serializedFriend}");
        }
    }

}