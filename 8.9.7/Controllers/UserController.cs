using Microsoft.AspNetCore.Mvc;


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
        var vm = new UserViewModel()
        {
            UserTypes = UserType.GetUserTypes()
        };
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
            user.UserTypes = UserType.GetUserTypes();
            return View(user);
        }
        // TODO: Almacenar los datos en el Modelo
        return Content("OK");
    }

    public IActionResult NickExists(string nickName)
    {
        return Json(!_userServices.Exists(nickName));
    }
}