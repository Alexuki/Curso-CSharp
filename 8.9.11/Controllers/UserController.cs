using Lab03.Models.Entities;
using Lab03.Models.Services;
using Lab03.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace Lab03.Controllers;

public class UserController : Controller
{
    private readonly IUserServices _userServices;

    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }


    [Route("/profile")]
    public IActionResult Edit()
    {
        var vm = new UserViewModel();
        ViewBag.UserTypes = UserType.GetUserTypes();
        return View(vm);
    }

    [Route("/profile")]
    [HttpPost]
    public IActionResult Edit(UserViewModel user)
    {
        if (ModelState.IsValid && _userServices.Exists(user.Nickname))
            ModelState.AddModelError("nickname", "User already exists!");

        if (!ModelState.IsValid)
        {
            ViewBag.UserTypes = UserType.GetUserTypes();
            return View(user);
        }
        return Content("OK");
    }

    public IActionResult NickExists(string nickName)
    {
        return Json(!_userServices.Exists(nickName));
    }
}