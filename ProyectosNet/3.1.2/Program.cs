/*
DEMO: Routing: endpoints
Partimos de una aplicación vacía.
la base de enrutado de ASP.NET Core, el EndpointRouting, son básicamente 2 middlewares:
- EndpointRoutingMiddleware: Analiza el verbo HTTP y la ruta de la petición entrante para determinar qué endpoint debe gestionarla.
- EndpointMiddleware: Se encarga de ejecutar el endpoint seleccionado en el paso anterior.

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

*/





// 1) Si no tenemos ningún middleware, nuestra petición no será procesada.
// En este ejemplo devuelve un error 404 porque no hay ningún middleware en el pipeline capaz de procesar la petición
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(); */




// 2) Añadimos middlewares para procesar la petición:
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting(); // Añadir primer middleware

// Añadir segundo middleware. Se encarga de ejecutar el endpoint seleccionado por el primero.
// Se pasa también configuración de los endpoints.
// El propio sistema de routing es capaz de extraer los valores de la ruta. En este ejemplo, el parámetro name
// que va a recibir el endpoint y lo podemos devolver. Ejecutamos la aplicación y vamos a https://localhost:xxxx/hello/john
// para comprobarlo.
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/hello/{name}", (string name) => $"Hello {name}");
});

app.Run(); */




// 3) Podemos insertar otros middlewares.
// Todos los middlewares situados entre los dos anteriores van a poder obtenr información de la decisión tomada por el primero.
// Vamos a comprobarlo añadiendo unos metadatos.
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

// Añadimos middleware intermedio. Cuando el endpoint seleccionado es el del DisplayName indicado,
// reemplazará el valor "Jon" por el de "John".
// Probamos la aplicación con las direcciones:
// /hello/Luis -> devuelve "Hello Luis"
// /hello/John -> devuelve "Hello John"
// /hello/Jon -> devuelve "Hello John" => El middleware adicional ha extraído información del routing y ha actuado reemplazando "Jon" por "John"
app.Use(async (ctx, next) =>
{
    var selectedEndpoint = ctx.GetEndpoint();
    if (selectedEndpoint?.DisplayName == "Hello endpoint")
    {
        var name = ctx.GetRouteValue("name")?.ToString();
        if("Jon".Equals(name, StringComparison.CurrentCultureIgnoreCase))
        {
            ctx.Request.RouteValues["name"] = "John";
        }
    }
    await next(ctx); // Damos el contexto de ejecución hacia el siguiente endpoint
});



app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/hello/{name}", (string name) => $"Hello {name}")
    .WithDisplayName("Hello endpoint"); // Se le añade un nombre al endpoint. Podríamos añadir otros datos arbitrarios con otros métodos como .WithMetadata, .WithName, RequireHost...
});

app.Run(); */



// 4) Realmente vamos a definir los endpoints de una manera más sencilla porque parte del trabajo de configuración
// lo ha resuelto el framework.
// El modelo de minimal API ya incluye app.UseRouting y app.UseEndpoints, por lo que se pueden eliminar. El código es equivalente a:
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/hello/{name}", (string name) => $"Hello {name}")
        .WithDisplayName("Hello endpoint");

app.Run();