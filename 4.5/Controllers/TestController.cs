using Microsoft.AspNetCore.Mvc;
public class TestController : Controller
{
    // {controller}/{action=Index}/{id?}
    public IActionResult Hello(string id, int age)
    {
        var name = id ?? "name";
        return Content($"Hello, {name}! You are {age} years old");
    }
    
    // Si no se indica acción, usará eu valor por defecto que es Index
    // Ejemplo: /test
    public IActionResult Index() => Content("Hello from Index");
    
    // El controlador también tiene un valor por defecto que es Home
    // Si vamos a https://localhost:xxxx, nos dará un error 404 porque el controlador Home no existe
    // Vamos a implementar dicho controlador
    
}