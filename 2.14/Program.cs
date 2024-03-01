// DEMO: static files

//1. Tenemos un middleware que responde a todas las peticiones con "Hello world!"
// Añadimos la carpeta wwwroot con carpetas y ficheros y 2 ficheros estáticos home.html y second.html

/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello world!");
});

app.Run(); */




/* 
2. Al ejecutar la aplicación, devuelve "Hello world!" siempre, incluso si intentamos acceder a las carpetas:
https://localhost:xxxxx/css o ficheros /home.html porque no hay un middleware registrado en el pipe de peticiones que se encargue
del acceso a los ficheros.*/

// Añadimos el middleware StaticFiles para servir los ficheros estáticos, antes del middleware de Hello world
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello world!");
});

app.Run(); */




/* 
3. De momento no podemos navegar entre carpetas. El middleware anterior solo se encarga de servir ficheros.
Si intentamos ir a una carpeta, nos mostrará "Hello world!".
Para navegar entre directorios añadimos el middleware DirectoryBrowser. Es necesario añadirlo en el inyector de dependencias también.
*/
/* var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDirectoryBrowser();
var app = builder.Build();

// middlewares
app.UseStaticFiles();
app.UseDirectoryBrowser();

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello world!");
});

app.Run(); */




/*
4. Al trabajar con ficheros estáticos podemos definir ficheros por defecto para los directorios.
Usaremos el middleware UseDefaultFiles.
Es importante que esté por encima de UseStaticFiles porque va a configurar cosas que necesitará más adelante.
Al ejecutar este código, no va a servir un fichero por defecto porque registra como ficheros por defecto: default.html y index.html.
En nuestro caso no tenemos un fichero con ese nombre. Podemos cambiar el nombre de home.html a index.html para comprobar que funciona.
Al cargar la página principal localhost:xxxx, va a abrir index.html.
*/
/* var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDirectoryBrowser();
var app = builder.Build();

// middlewares
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseDirectoryBrowser();

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello world!");
});

app.Run(); */



/*
5. Si no nos interesa cambiar el nombre de los ficheros, podemos configurar otros ficheros por defecto pasándole unas options al middleware.
*/
/* var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDirectoryBrowser();
var app = builder.Build();

// middlewares
app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = new[] {"second.html"}
});
app.UseStaticFiles();
app.UseDirectoryBrowser();

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello world!");
});

app.Run(); */



/*
Habitualmente, al trabajar con ficheros estáticos, vamos a usar los 3 middlewares anteriores, por lo que podemos usar
el middleware UseFileServer que configura los 3.
Tiene sobrecargas para configurar los middlewares que contiene, pero lo más sencillo es pasarle un objeto de opciones.
En este ejemplo vamos a ponerle RequestPath, que es el path desde el que vamos a servir los ficheros, un prefijo que
tendremos que usar en nuestra ruta para servir ficheros estáticos. Es útil para distinguir qué es y qué no es de nuestra
aplicación.
También habilitamos la navegación por directorios y los ficheros por defecto.
Al ejecutar la aplicación, vemos "Hello world!" por el RequestPath. Ahora, para acceder a los ficheros y carpetas debemos poner:
https://localhost:xxxx/static
Esto debe tenerse en cuenta en el funcionamiento de las páginas. El enlace a la segunda página no va a funcionar porque le falta /static.
*/
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDirectoryBrowser();
var app = builder.Build();

// middlewares
app.UseFileServer(new FileServerOptions
{
    RequestPath = "/static",
    EnableDirectoryBrowsing = true,
    EnableDefaultFiles = true
});

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello world!");
});

app.Run();