//4.5 DEMO: ^Puesta en marcha y ejemplos simples MVC

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Añadir en el inyector de dependencias las mínimas necesarias para utilizar MVC
builder.Services.AddControllersWithViews();

// Añadir MVC al pipeline usando los dos middlewares más habituales
// y mapear los controladores con la ruta por defecto de MVC
var app = builder.Build();

app.MapDefaultControllerRoute();
app.Run();

// Ruta por defecto: {controller}/{action}/{id?}
// La acción es dentro del controlador. El propio sistema de MVC es capaz de ejecutar
// la acción correcta al recibir una ruta de esta forma


// Vamos a hacer peticiones a /test/hello/john
// Llevado a nuestro modelo, esto es:
//                {controller}/{action}/{id?}
// Petición: GET: test        /hello   /john  



// Los controladores deben ser clases públicas
// Lo ponemos aquí pero debería ir en un fichero aparte
/* public class TestController : Controller
{
    // Llamar a esta acción con /test/hello/john
    
    // public IActionResult Hello(string id)
    // {
    //     var name = id ?? "name";
    //     return Content($"Hello, {name}!");
    // }

    // Los controladores soportan binding, al igual que en la implementación de endpoints directamente
    // Esto permite recuperar contenido que llega en las peticiones e introducirlo como parámetros en nuestras acciones
    // Pasar parámetro en la query string /test/hello/name?age=edad
    // Ejemplo: /test/hello/john?age=34
    public IActionResult Hello(string id, int age)
    {
        var name = id ?? "name";
        return Content($"Hello, {name}! You are {age} years old");
    }

} */

// Crear el controlador dentro de la carpeta Controllers en la raíz del proyecto