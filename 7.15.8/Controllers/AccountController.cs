using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



public class AccountController : Controller
{
    private readonly IAccountServices _accountServices;

    public AccountController(IAccountServices accountServices)
    {
        _accountServices = accountServices;
    }

    [AllowAnonymous] //(15)
    public IActionResult Login(string returnUrl)
    {
        var vm = new LoginViewModel();
        return View(vm);
    }

    [AllowAnonymous] //(15)
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (ModelState.IsValid && _accountServices.CheckCredentials(vm.Username, vm.Password))
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, vm.Username));

            var principal = new ClaimsPrincipal(identity);

            //(11)
            foreach (var roleName in _accountServices.GetRolesForUser(vm.Username))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal
            );
            return RedirectToAction("Index", "Home");
        }
        vm.ErrorMessage = "Not authorized";
        return View(vm);
    }


    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}