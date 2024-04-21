/*
PRÁCTICA 5: Filtros

- 1. Autorización de aplicaciones
- 1.1 Preparación de infraestructura
1) Crear aplicación MVC sin opciones de autenticación porque añadiremos sistema de seguridad desde cero de forma manual.
En la clase de inicialización, configurar el servicio de autenticación.
2) Añadir AccountController con método Login que retorna vista de formulario.
3) Añadir view model que se usará en la vista haciendo que ssea tipada.
4) Crear acción Login que recibe los datos del formulario.
5) Añadir AccountServices junto con su interfaz e inyectarla en el controlador. Implementar método CheckCredentials que retorn true
si el nombre de usuario y contraseña coinciden.
6) Si las credenciales no son correctas, establecer un mensaje en la propiedad ErrorMessage del view model y mostrarla en la vista.
7) Editar Layout y añadir enlace al formulario de login si el usuario no está autenticado. Si lo está, mostrar enlace con su nombre
y que dirija a acción Logout que se debe implementar en AccountController.

- 1.2 El filtro [Authorize]
8) Crear Controlador Private con una acción Index que retorne una vista o un ContentResult con
caulquier mensaje, y decorado con el atributo Authorize.
9) Comprobar que solo se puede acceder a /private estando autenticados, y que redirige
a la página de identificación en caso de no estarlo.
10) Añadir a AccountServices un método para obtener los roles de un usuario.
11) Acudir al punto donde creamos la cookie y el objeto ClaimsPrincipal,
y añadimos un claim por cada rol retornado por el método GetRolesForUser().
12) Añadir en PrivateController la acción ForAdmins, solo accesible a usuarios que pertenezcan al rol "admin".
13) Configurar política "FourCharacters" que se cumpla solo cuando el nombre de usuario autenticado tenga cuatro caracteres.
Crear acción FourCharacters en PrivateController y aplicarle dicha política.
14) Probar la aplicación y comprobar que funciona el acceso restringido a las acciones de Private,
utilizando usuarios administradores y no administradores, y con distinto número de caracteres en su nombre.

- 1.3 Filtros globales y AllowAnonymous
15) Añadir un filtro global de autorización para que todas las acciones de la aplicación sean accesibles solo por
usuarios autenticados.
NOTA: Hacer que las dos acciones Login() de AccountController estén disponibles para usuarios anónimos (sin autenticar)
para evitar que la aplicación entre en un bucle infinito de redirecciones.

- 2. Caché en cliente con [ResponseCache]
16) Introducir en la vista Index de Home el código que muestra la hora actual.
17) Ejecutar la aplicación y acceder a la raíz del sitio web. Comprobamos que el resultado es generado cada vez que se
realiza una petición a la acción, incluso cuando usamos los enlaces de la barra superior de navegación:
el resultado no está siendo cacheado.
18) Cachear el resultado de la acción Index() en cliente, con una validez de 10 segundos.
19) A partir de ese momento, al acceder a la página de inicio aparece la misma hora porque el navegador no ha realizado
la petición al servidor; en su lugar, vuelve a mostrar el contenido que tiene cacheado.
NOTA: Esto no será cierto si el usuario fuerza el refresco de la página en su navegador (con F5 por ejemplo), puesto que
ignorará su contenido en caché y realizará la petición al servidor.

- 3. Implementación de filtros personalizados
- 3.1 Añadir encabezados a la respuesta
20) Incluir en el proyecto un filtro que añada un encabezado extra a las peticiones que sean procesadas por acciones MVC.
21) Insertarlo en la colección de filtros globales para que tenga efecto en todos los controladores.
22) Ejecutar la aplicación y analizar los encabezados de respuesta mediante las herramientas de desarrolaldor del
navegador o con herramientas de trazado de peticiones como Fiddler (https://www.telerik.com/fiddler).

- 3.2 Filtros personalizados: caché en servidor simplificada
23) Añadir la clase SimpleCacheAttribute en Extensions/Filters.
24) Crear acción GetTime() en HomeController y aplicarle el filtro SimpleCache.
25) Ejecutar la aplicación y comprobar desde distintos navegadores que el resultado de la petición /home/gettime
está siendo cacheada en el servidor y siempre retorna la misma respuesta. Se puede introducir un breakpoint en
GetTime() para comprobar que solo se ejecuta la primera vez.
NOTA: La clave para la caché es la ruta de la petición. Podemos probar introduciendo alternativas como
/home/gettime/1 o /home/gettime/2 y veremos que se cachean de forma independiente.
26) Modificar el código del filtro para que el contenido solo sea cacheado durante 10 segundos. Si transcurrido ese
plazo entra una petición al mismo recurso, se debe asumir que el contenido de la caché no es válido, por lo que la
acción deberá ser ejecutada y su resultado cacheado de nuevo.
Aunque existen varias formas para conseguirlo, podemos hacer simplemente que en el diccionario de almacenamiento
se guarde tanto el contenido cacheado como la fecha/hora en la que se cacheó. Así, cuando vayamos a comprobar si
existe el elemento, podremoss también verificar si está caducado.
*/

using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//(5)
builder.Services.AddScoped<IAccountServices, AccountServices>();
//builder.Services.AddControllersWithViews(); //(15)

//(15)
builder.Services.AddControllersWithViews(config => 
{
    config.Filters.Add(new AuthorizeFilter());
    config.Filters.Add(new HandledByMvcAttribute()); //(20) Filtro creado en Extensions/Filters (21) Filtro añadido a la colección de filtros globales
});



//(1)
builder.Services.AddAuthentication()
    .AddCookie(opt =>
    {
        opt.AccessDeniedPath = "/account/login";
        opt.LoginPath = "/account/login";
    });

//(13)
builder.Services.AddAuthorization(options => 
{
    options.AddPolicy("FourCharacter",
        builder => 
        {
            builder.RequireAssertion(cond => cond.User.Identity?.IsAuthenticated == true && cond.User.Identity?.Name?.Length == 4);
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
