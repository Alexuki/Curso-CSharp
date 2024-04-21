using System.Security.Claims;
using Lab05.Models.Services;
using Lab05.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab05.Controllers;

public class AccountController : Controller
{
    private readonly IAccountServices _accountServices;

    public AccountController(IAccountServices accountServices)
    {
        _accountServices = accountServices;
    }

    /*
    //(1) Solo retorna una vista
    public IActionResult Login(string returnUrl)
    {
        return View(vm);
    }

    //(2)
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (ModelState.IsValid && vm.Username == vm.Password)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, vm.Username));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal
            );
            return RedirectToAction("Index", "Home");
        }
        return View(vm);
    }


    //(6)
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (ModelState.IsValid && vm.Username == vm.Password)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, vm.Username));
            //(6)
            if (vm.Username == "john")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            }
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal
            );
            return RedirectToAction("Index", "Home");
        }
        return View(vm);
    }
    */

    [AllowAnonymous] //(12)
    public IActionResult Login(string returnUrl)
    {
        var vm = new LoginViewModel();
        return View(vm);
    }

    [AllowAnonymous] //(12)
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (ModelState.IsValid && vm.Username == vm.Password)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, vm.Username));
            if (vm.Username == "john")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            }
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal
            );
            return RedirectToAction("Index", "Home");
        }
        return View(vm);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}