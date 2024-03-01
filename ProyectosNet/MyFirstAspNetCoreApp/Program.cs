//Archivo original creado con dotnet new web
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run(); */


//3.3 Ejercicio recomendado

var builder = WebApplication.CreateBuilder(args); // Se crea un builder con el patrón HostBuilder ofrecido por .Net
var app = builder.Build(); // Generar aplicación

// En la aplicación se mapea un endpoint al root de la web que nos responda "Hello World!"
app.MapGet("/", (HttpContext context) =>
{
    var name = (string)context.Request.Query["name"] ?? "Unknown user";
    return $"Hello, {name}!!!";
});

app.MapGet("/time", () =>
{
    var currentTime = DateTime.Now.ToString("HH:mm:ss");
    return $"Current time is {currentTime}";
});

app.MapGet("/sum", (HttpContext context) =>
{
    if (!int.TryParse(context.Request.Query["a"], out int a) || !int.TryParse(context.Request.Query["b"], out int b))
    {
        return "Invalid input. Please provide integer values for 'a' and 'b'.";
    }
    
    var sum = a + b;
    return $"{a}+{b}={sum}";
});

app.MapGet("/environment", () => app.Environment.EnvironmentName);



app.Run(); // Se encarga de mantener la aplicación viva para que el servidor responda peticiones mientras queramos.

//3.4 DEMO: Creación de un proyecto básico
/*
Si se crea la aplicación con Visual Studio, tendremos
[DIR] Connected Services: Conexiones con servicios externos (Azure, otros servicios web...) que se hayan creado desde el entorno. Solo aparece en VS pero no en VS Code porque es parte de VS.
[DIR] Dependencies: Dependencias del framework; analizadores estáticos del código y dependencias al framework (AspNetCore.App y NETCore.App)
[DIR] Properties: En VS contiene launchSettings.json con las configuraciones de cómo arranca la aplicación: con ISS, http o https
[FILE] appsettings.json y appsettings.Development.json: Se configuran las variables que necesitemos dependientes del entorno. En el primero van las generales y en el segundo van las de Development.
[FILE] Program.cs

Al crear el proyecto usando CLI, existen varias plantillas que vienen con .NET Core instaladas
*/


//4.1 DEMO: Hot Reload
/*
Característica que permite recompilar en caliente.
En VS se activa en Ver -> Salida y seleccionando en la ventana de salida Mostrar salida de Recarga activa.
Se configura en el botón de Recarga activa si queremos hacerla manualmente (pulsando el botón) o bien cuando se guarde el archivo.
Al modificar el archivo, cuando se recarga el navegador se ven los cambios.

En VS Code usaremos una terminal embebida dentro de VS Code.
Para tener Hot Reload, en lugar de dotnet run, usamos dotnet watch.
La terminal muestra que cuando se hacen cambios al fichero y se guardan, ya se aplican. Es necesario recargar la página.

Hot Reload solo permite ciertos cambios. Por ejemplo, no se puede cambiar una firma de método, una interface, clase abstracta, etc.
Para estos casos es necsario recompilar. En nuestro ejemplo, si modificamos el método abstracto que devuelve
"Hello World!" y lo cambiamos por que devuelva un entero: () => 1, al guardar nos dirá que necesita recompilar.
Si decimos que sí, parará la aplicación, recompilará y la volverá a arrancar.

*/