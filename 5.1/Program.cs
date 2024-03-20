//5.1 DEMO: Autenticación
/*
Funcionamiento del sistema de autenticación basado en cookies que ofrece ASP.NET Core.
Crearemos aplicación donde los usuarios podrán hacer login y acceder a ocntenido privado.
*/

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
// 1) Registrar dependencias para el middleware de autenticación. Nos devuelve un AuthenticationBuilder que nos permite configurar
// el sistema de autenticación. Aporta varios métodos de extensión, entre ellos el de añadir el uso de cookies.
// Se pueden poner las dos llamadas seguidas en lugar de hacerlo como en estas líneas comnetadas.
// El proveedor de autenticación por cookies viene por defecto en por ASP.NET Core, pero se pueden usar otros usando otros paquetes.
/* 
var auth = builder.Services.AddAuthentication();
auth.AddCookie(); 
*/

builder.Services.AddAuthentication("MyApp") // Al usar nuestro esquema, debemos añadirlo también como método/esquema por defecto para AddAuthentication
                //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme); // Nombre por defecto del esquema "Cookie"
                .AddCookie("MyApp", (opt) => // Uso de nombre personalizado para la cookie, y lambda para configuración
                {
                    opt.Cookie.Name = "Ticket";
                    opt.Cookie.HttpOnly = true; // solo accesible por HTTP
                    opt.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                });


var app = builder.Build();
// 2) Definir la autenticación de usuarios
app.MapGet("/login/{username}", async (HttpContext ctx, string username) => { // Handler asíncrono en que generamos la cookie
    var identity = new ClaimsIdentity("MyApp");
    identity.AddClaim(new Claim(ClaimTypes.Name, username));
    identity.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));
    var principal = new ClaimsPrincipal(identity);
    await ctx.SignInAsync("MyApp", principal);
    return "Logged in!";
});


// 4 ) Añadir un endpoint que permita acceder solo si estamos logueados
app.MapGet("/private", (HttpContext ctx) => 
{
    if (ctx.User.Identity.IsAuthenticated) // Si User tiene una identidad y está autenticada
    {
        return "Secret content!";
    }
    return "Not allowed";
});

// Tras haber obtenido la cookie de autenticación, si vamos a https://localhost:xxxx/private
// se muestra el contenido "Secret content!"
// Si intentamos ir a /private sin tener la cookie, nos pondrá "Not allowed".
// Si hacemos login, sí nos dejará ver el contenido de /private
// La cookie puede verse en herramientas de desarrollo -> Aplicación -> Cookies. Ahí podemos borrarla y,
// si recargamos la página, nos mostrará "Not allowed"


// 5) Añadir endpoint para desloguearse, en lugar de borrar la cookie desde el navegador,
// siendo el propio flujo del usuario el que ejecuta este proceso
app.MapGet("/logout", async (HttpContext ctx) => {
    await ctx.SignOutAsync("MyApp");
    return "Logged out!";
});



app.MapGet("/", () => "Hello World!");

app.Run();

// 3) Arrancamos la aplicación y vamos a https://localhost:xxxx/login/john
// Muestra el mensaje de Log in! y en los encabezados podemos ver la cookie en la entrada "set-cookie" con nombre Ticket
// Ticket=CfDj8OII6...; path=/; secure; samesite=lax; httponly
// Aparece la configuración que le indicamos como httponly
