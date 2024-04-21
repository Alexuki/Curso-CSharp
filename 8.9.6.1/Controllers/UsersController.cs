using Microsoft.AspNetCore.Mvc;


public class UsersController : Controller
{
    public IActionResult Create()
    {
        return View(new CreateUserViewModel());
    }

    [HttpPost]
    public IActionResult Create(CreateUserViewModel vm)
    {
        if(!ModelState.IsValid)
        {
            return View(vm);
        }
        //TODO: Salvar los datos del usuario

        return Content("Ok");

    }


}