// Manejador para las peticiones relativas al login
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

public static class LoginHandlers
{
    public static async Task GetLoginPageAsync(HttpContext context)
    {
        var body = @"
            <h1>Login</h1>
            <form method='post' action='/login'>
                Username:  <input type='text' name='username'><br>
                Password: <input type='password' name='password'><br>
                <hr>
                <input type='submit' value='Login' >
            </form>
        ";
        await PageUtils.SendPageAsync(context, "Login", body);
    }

    public static async Task DoLoginAsync(HttpContext context)
    {
        // Obtener los datos del formulario y comprobar credenciales.
        // a) Si las credenciales son incorrectas, mostramos una página de error
        // b) Si las credenciales son correctas, establecemos la cookie y redirigimos a /home

        // Por simplificar, podemos considerar correctas las credenciales
        // cuando username == password


        string? username = context.Request.Form["username"];
        string? password = context.Request.Form["password"];
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || username != password)
        {
            var body = @"
                    <h1>Invalid credentials</h1>
                    <p>Please <a href='/login'>try again</a>.</p>
                ";
            await PageUtils.SendPageAsync(context, "Invalid credentials", body);
            return;
        }

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.Name, username));
        var principal = new ClaimsPrincipal(identity);
        await context.SignInAsync(principal);
        context.Response.Redirect("/home");
    }

    public static async Task DoLogoutAsync(HttpContext context)
    {
        await context.SignOutAsync();
        context.Session.Clear();
        var body = @"
                    <h1>Logged out</h1>
                    <p><a href='/home'>Back to home</a>.</p>
                ";
        await PageUtils.SendPageAsync(context, "Logged out", body);
    }

}
