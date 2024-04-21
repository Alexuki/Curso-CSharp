using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab05.Controllers;

public class PrivateController: Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return Content("This content is only for authenticated users");
    }

    [Authorize(Roles="admin")]
    public IActionResult ForAdmins()
    {
        return Content("This content is only for admins");
    }

    [Authorize("FourCharacters")]
    public IActionResult FourCharacters()
    {
        return Content("This content is only for four characters users");
    }
}