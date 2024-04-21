/*
7.15.6 DEMO: Los filtros Authorize y ResponseCache.
Se ha creado un sitio con una funcionalidad simple de login que está bajo la ruta /Account/Login.
1) En el controlador, Login solo devuelve una vista.
2) En Login con método POST recibe los datos. Si el ViewModel es válido y cumple con que Username y Password son iguales,
crearemos una identidad y redirigirá a Index. Si no, retorna de nuevo la vista del formulario.
Si es correcto:
    - Crea una identidad.
    - Añade un Claim a esa identidad con un nombre de usuario.
    - Creamos un ClaimsPrincipal con la identidad.
    - Logueamos al usuario generando una cookie con el ClaimsPrincipal.
    - Redireccionamos a /Home/Index.
3) Al loguearnos, disponemos de un botón de logout que elimina la cookie del usuario.
Esto funciona porque en la inicialización se han registrado las dependencias para usar autenticación (3.1)
y hemos añadido al pipe de ASP.NET Core los Middlewares necesarios para que funcione (3.2).
Aunque añadimos 2 middlewares, solo estamos registrando uno en el Inyector de Dependencias (3.1) porque las dependencias necesarias
para el middleware de autorización se registran automáticamente en el registro de AddControllersWithViews (3.3).
4) Veremos el funcionamiento del atributo Authorize en el Controller Home, en Privacy().
Esto solo permite acceder al usuario si se ha autenticado previamente. Si no es así, lo redirige al Login. Esto es así
porque hemos añadido un path para forzar un redirect al login en ese caso (4.1).
También se ha añadido un redirect a la misma ruta en caso de que el usuario esté autenticado pero no tenga permisos suficientes para acceder a una ruta en concreto (4.2).
5) El atributo anterior admite que se le pase un parámetro Roles. En este caso, además de estar autenticados con la cookie, necesitamos que el usuario tenga el rol indicado dentro de su cookie.
6) En AccountController añadimos una condición para que añada el rol "Admin" a un usuario si su Username es john.
Para esto, se añade un Claim con el Type Role. Así, el usuario tendrá la cookie de autenticación que contendrá el rol "Admin".
Ahora el usuario autenticado como "john" puede acceder a Privacy() porque tiene ese Claim.
7) Además de usar roles para permitir o denegar el acceso, podemos usar políticas (Policies), mecanismo más potente porque es más configurable.
En HomeController vamos a usar Policy en lugar de Roles. Le daremos el nombre de la política que vamos a usar, y crearemos a continuación: "Family".
8) Definimos la política anterior dentro de Program.cs añadiendo el servicio Authorization. Aunque por defecto va incluido en
AddControllersWithViews, queremos extenderlo con nuestra policy, declarada con una lambda.
Le indicaremos con RequireAssertion que debe validar que viene el contenido y que reciba el contexto. A continuación,
vamos a requerir que el usuario esté autenticado y su nombre sea "smith". En ese caso, el usuario estará dentro de "Family". (8.1)
A diferencia de usar Roles, las Policies nos permiten no hacer que el nombre sea "smith" sino que contenga esa parte de la cadena (8.2).
Permite usar lógica dentro de la Policy, lo que aporta dinamismo. Así, si nos logueamos con "peter smith", se aplicará la política y permitirá acceder. Es por ello que las políticas son más potentes que el uso de roles.
9) En lugar de aplicar Authorize a acciones concretas, podemos hacerlo en el controlador, de forma que aplique a todas sus acciones.
Podemos quitar la anotación anterior de Privacy() y ponerla al nivel de clase.
10) También se puede aplicar la autorización a nivel global para que se aplique a todos los controladores.
Existen varias formas de hacerlo. Una de ellas, es a través del mapeo de endpoints usando el extensor RequireAuthorization().
11) También podemos añadirle la Policy que creamos previamente.
Al hacer este paso, si nos deslogueamos de la aplicación, se producirá un error "ERR_TOO_MANY_REDIRECTS" porque la aplicación entra en un
bucle infinito de redirecciones al intentar acceder a la web ya que /aacount/login está protegida, nos intenta redirigir a /aacount/login...,
y así indefinidamente. Se observa en el error que muestra:
"Parece que la página web de https://localhost:5001/account/login?ReturnUrl=%2Faccount%2Flogin%3FRetunrUrl%3D%252Faccount%252Flogin%253FRetrunUrl%253D%25252Faccount..."
Para solucionar esto cuando trabajamos con Authorize a nivel global, podemos añadir explícitamente el atributo AllowAnonymous. Así, aunque el controlador
requiera autorización, el método en que se aplica el atributo no lo requiere.
12) Añadimos AllowAnonymous a Login tanto GET como POST.
13) Otra alternativa para proteger las aplicaciones a nivel global es usar una específica de MVC.
Eliminamos el RequireAuthorization en MapControllerRoute y modificamos AddControllersWithViews mediante la lambda de configuración.
En Filters añadimos el filtro de autorización y la política. Esto es un filtro global.
14) Veremos el filtro ResponseCache que sirve para añadir a las respuestas enviadas al navegador información sobre cómo deben de ser cacheados
los contenidos que estamos devolviendo.
Para comprobarlo, vamos a la vista Home/Index y mostramos la hora actual en lugar del mensaje "Welcome".
Nos logueamos para que nos muestre la página Home con la hora.
15) Vamos a cachear la hora en el HomeController para que no necesariamente se ejecuten todas las veces
las peticiones. Para ello, añadimos el atributo ResponseCache en la acción Index, el cual permite
especificar un parámetro Duration.
Ahora, al visitar la aplicación, se obtiene el valor cacheado. Al hacer F5, se fuerza el refresco de la página,
y puede obtener de nuevo el valor de hora actual, pero si usamos los enlaces para ir a otras páginas y volver a Home,
o hacemos click en el enlace Home varias veces seguidas, solo se actualiza cuando el valor de duración ha pasado.
16) Hacemos varias peticiones pulsando en el enlace Home y las analizamos en la pestaña de Red de las Developer Tools.
Podemos ver que solo se realizan las peticiones directamente (y no obteniéndose de caché) cuando el tiempo de expiración
ha concluido.

*/

using Lab05.Extensions.Filters;
using Lab05.Models.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddControllersWithViews(); //(3.3)

//(13)
builder.Services.AddControllersWithViews(opt => 
{
    opt.Filters.Add(new AuthorizeFilter("Family"));
});


//(8)
builder.Services.AddAuthorization(opt =>
{
    // Añadir la Policy. También se añade su ejecución con otra lambda
    opt.AddPolicy("Family", policy => 
    {
        policy.RequireAssertion(ctx => 
        {
            return ctx.User.Identity.IsAuthenticated
                    //&& ctx.User.Identity.Name == "smith"; //(8.1)
                    && ctx.User.Identity.Name.ToLower().Contains("smith"); //(8.2)
        });
    });
});



//(3.1)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => 
    {
        opt.AccessDeniedPath = "/account/login"; //(4.2)
        opt.LoginPath = "/account/login"; //(4.1)
    });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

//(3.2)
app.UseAuthentication();
app.UseAuthorization();


//(10)
/* app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); */


//(10) Aplicar Authorize a todos los controladores
/* app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    ).RequireAuthorization(); */


//(11) Aplicar Authorize a todos los controladores junto con la Policy creada previamente
/* app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    ).RequireAuthorization(new AuthorizeAttribute("Family")); */

//(13)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




/* builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddControllersWithViews(config =>
{
    config.Filters.Add(new AuthorizeFilter());
    config.Filters.Add(new HandledByMvcAttribute());
}); */







/* builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("FourCharacters",
        builder =>
        {
            builder.RequireAssertion(cond => cond.User.Identity?.IsAuthenticated == true && cond.User.Identity?.Name?.Length == 4);
        });
}); */







    

app.Run();

