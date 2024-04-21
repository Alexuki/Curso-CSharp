/*
9.9.1 DEMO: El filtro [APIController]
Veremos el uso del filtro en servicios ASP.NET Core MVC.
1) Partimos de una aplicación MVC vacía.
2) Añadimos un modelo Friend.
3) Añadimos el controlador FriendsController.
4) Ejecutamos la aplicación y comprobamos que funciona usando Fiddler. En la pestaña "Composer"
ponemos petición POST a la URL de nuestro servidor, añadiendo el endpoint:
POST https://localhost:xxxxx/friends/create

y en RequestBody le pasamos un JSON:
{
    "name": "Josh",
    "age": 26
}
En la parte superior, en las cabeceras donde aparece User-Agent: Fiddler, añadimos:
content-type: application/json
Pulsamos en "Execute" y comprobamos que funciona correctamente. Seleccionamos en el panel
de la izquierda la petición que se ha realizado, y en la pestaña "Inspectors", en la ventana inferior
de ResponseBody podemos consultar lo recibido en las diferentes pestañas:
"Headers" => 200 OK.
"JSON" => En el cuerpo de la petición nos devuleve el objeto creado.

5) Probamos a crear un objeto incorrecto, por ejemplo poniendo en "age" el valor -1. La respuesta es:
"Headers" => 400 Bad Request.

6) Marcamos el controlador con el atributo [ApiController] y volvemos a ejecutar la misma petición que
en el caso anterior. Genera esta respuesta:
"Headers" => 500 Internal Server Error.
Esto indica que no se ha configurado la ruta para la acción Create.
Vamos al controlador e indicamos dicha ruta.
7) Realizamos una petición a Create con un objeto válido. Se ejecuta correctamente porque ahora ya
cuplimos con el filtro [ApiController] que obliga a que todas als acciones del controlador sean
accesible mediante rutas definidas con atributos
8) Enviamos ahora un JSON no válido. Esto devuelve un error 400 Bad Request pero, además, podemos
ver que Content-Type: application/problem+json; chartset=utf-8. Así, si vamos a la pestaña "Raw" o "JSON"
para ver el cuerpo de la respuesta, veremos que ha añadido errores relativos al error.
Esto es posible porque los errores llegan serializados en un objeto ProblemDetailsStandard.
Esta respuesta no la estamos devolviendo nosotros con el return BadRequest() de la acción, sino que la
validación del objeto se realiza de forma automática antes de que nuestro código sea ejecutado. Esta es
otra de la convenciones derivadas de la aplicación de [ApiController].
9) Si eliminamos las líneas referidas al return BadRequest(), el error indicado será el mismo.
10) Otra de las convenciones de [ApiController] es que se aplicará automáticamente [FromBody] a los parámetros
complejos de las acciones. Así, poedmos eliminar el atributo [FromBody] en el método Create, y seguirá funcionando
correctamente.
11) [ApiController] también configura una serie de mapeos entre códigos de estado HTTP y errores del lado del cliente.
Vamos a añadir un fragmento de código en Create que devuelva un Conflicto si el nombre del objeto a crear es "john".
Probamos a hacer dicha petición usando ese nombre y nos da esta respuesta:
"Headers" => 409 Conflict. y la respuesta se ha convertido automáticamante en Content-Type: application/problem+json; chartset=utf-8.
Así, en el JSON nos está dando el objeto de ProblemDetail.
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
