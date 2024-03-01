// 1.4.1 DEMO: Routing: Binding de parámetros

// 1) Handler que manjea ints
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// localhost:xxxx/add/1/2 => Devuelve 3
app.MapGet("/add/{a}/{b}", (int a, int b) => a+b);

app.Run(); */


// 2) Handler que maneja strings. El bind de parámetros se encarga de hacer el casting al tipo indicado
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// localhost:xxxx/add/1/2 => Devuelve 12
app.MapGet("/add/{a}/{b}", (string a, string b) => a+b);

app.Run(); */


// 3) Vamos a añadir un archivo de clase para implementar los handlers en lugar
// de hacerlo en una lambda en la propia definición del endpoint. Es preferible que el procesado
// de las peticiones se haga en un fichero aparte del de la inicialización de la aplicación para
// que el código esté más limpio y sea más mantenible.
/* using Routing;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// El framework va a intentar siempre convertir los parámetros pasados por la ruta en los tipos indicados.
// Si no puede, da un error BadRequest. Por ejemplo: https://localhost:xxxx/add/1/hello
// Este error también sucederá aunque los parámetros sean opcionales. Por ejemplo, si ponemos:
// "/add/{a}/{b?}" y enviamos https://localhost:xxxx/add/1, dará error BadRequest porque no puede obtener un valor para el parámetro b
// Para este caso, es necesario definir el parámetro como opcional también en el handler
app.MapGet("/add/{a}/{b}", CalculatorHandlers.Add);

app.Run(); */


// 4) Endpoint con parámetros opcionales
/* using Routing;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/add/{a}/{b?}", CalculatorHandlers.Add);

app.Run(); */



// 5) Obtención de valores desde otros orígenes diferentes a la ruta.
// Al eliminar los parámetros de la ruta, el sistema de binding no sabrá de dónde obtener los valores para los parámetros a y b
// Sin embargo, sí lo consigue si se explicitan dichos valores mediante una query string porque es el comportamiento por defecto
// buscar los valores de los parámetros en el query string. Esto funciona para datos simples.
/* using Routing;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// localhost:xxxx/add/1/2 => Devuelve un error 404
// localhost:xxxx/add?a=1&b=2 => Devuelve 3
app.MapGet("/add", CalculatorHandlers.Add);

app.Run(); */



// 6) Obtener un valor desde el encabezado
/* using Microsoft.AspNetCore.Mvc;
using Routing;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// localhost:xxxx/add/1/2 => Devuelve un error 404
app.MapGet("/add", CalculatorHandlers.Add);

// Extracción de datos del header de la request cuando se hace una petición a root
app.MapGet("/", ([FromHeader(Name = "user-agent")] string userAgent,
                 [FromHeader(Name = "accept-language")] string lang)
                    => $"Lang: {lang}, user agent: {userAgent}"
);

app.Run(); */




// 7) Uso de tipos complejos como requisito del parámetro.
// Añadimos una clase, por ejemplo Friend, con unas propiedades.
// Añadimos un MapPost al endpoint /friends que recibe un objeto Friend y nos devuelve un string con la información recibida.
// Arrancamos la aplicación y le enviamos una petición POST usando Postman, con el objeto que espera.
// En Postman escribimos la dirección del endpoint, en "Body" escogemos "raw" y le pasamos el objeto JSON.
// El JSON será:
/* {
    "name": "John",
    "age": 34
} */
// En "Headers" le indicamos que es un JSON añadiendo la key "Content-Type" y en valor "application/json".
// Seleccionamos el verbo "POST" y pulsamos en Send.
// Si no funciona puede ser necesario deshabilitar la validación SSL porque el que está sirviendo es autofirmado.
// El resultado aparecerá en la pantalla de respuesta, en el apartado "Body".
// No ha sido necesario utilizar el atributo [FromBody] porque por defecto un parámetro de tipo complejo,
// se asume que viene en el cuerpo de la petición. Solo se permite codificar en JSON un objeto. El atributo anterior lo 
// usaremos para forzar que cualquier parámetro, aunque sea simple, lo tome del body.
/* using Microsoft.AspNetCore.Mvc;
using Routing;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/add", CalculatorHandlers.Add);

app.MapPost("/friends", (Friend friend) => $"Recibido Friend con nombre: {friend.Name}");

app.MapGet("/", ([FromHeader(Name = "user-agent")] string userAgent,
                 [FromHeader(Name = "accept-language")] string lang)
                    => $"Lang: {lang}, user agent: {userAgent}"
);

app.Run(); */



// 8) Obtener parámetros que vengan directamente desde la inyección de dependencias
// Creamos un servicio en una nueva clase CalculatorService con la interfaz y la implementación.
// Registramos el servicio en el inyector de dependencias. Lo ponemos como Singleton por ejemplo, aunque en este ejemplo da igual.
// Recibiremos el servicio en un endpoint. En el lambda indicamos los parámetros y el servicio.
// En la llamada, el servicio se va a inyectar correctamente como un parámetro más de nuestro handler.
/*
using Microsoft.AspNetCore.Mvc;
using Routing;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ICalculatorServices, CalculatorServices>();

var app = builder.Build();

app.MapGet("/add", CalculatorHandlers.Add);

app.MapPost("/friends", (Friend friend) => $"Recibido Friend con nombre: {friend.Name}");

app.MapGet("/multiply/{a}/{b}", (int a, int b, ICalculatorServices calculator) => calculator.Multiply(a, b));

app.MapGet("/", ([FromHeader(Name = "user-agent")] string userAgent,
                 [FromHeader(Name = "accept-language")] string lang)
                    => $"Lang: {lang}, user agent: {userAgent}"
);

app.Run(); */


// 9) NOTA SOBRE LO QUE PUEDE DEVOLVER UN HANDLER
// Cuando retorna un string, el texto se devuelve tal cual con un Content-Type de "text-plain"


/////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////


// 1.7 DEMO: Routing: Retorno de handlers
// El handler puede devolver objetos de tipo IResult
// 1) Hacer que el handler devuelva un error 400 BadRequest cuando el divisor sea 0
/*
using Microsoft.AspNetCore.Mvc;
using Routing;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ICalculatorServices, CalculatorServices>();

var app = builder.Build();

app.MapGet("/add", CalculatorHandlers.Add);

app.MapPost("/friends", (Friend friend) => $"Recibido Friend con nombre: {friend.Name}");

app.MapGet("/multiply/{a}/{b}", (int a, int b, ICalculatorServices calculator) => calculator.Multiply(a, b));

// localhost:xxxx/divide?a=1 => Error 400 BadRequest porque falta un parámetro
// localhost:xxxx/divide?a=1&b=1 => Estado 200 Devuelve 1
// localhost:xxxx/divide?a=1&b=0 => Error 400 BadRequest
app.MapGet("/divide", (int a, int b) =>
{
   if (b == 0)
   {
      return Results.BadRequest();
   }
   return Results.Ok(a/b);
});

app.MapGet("/", ([FromHeader(Name = "user-agent")] string userAgent,
                 [FromHeader(Name = "accept-language")] string lang)
                    => $"Lang: {lang}, user agent: {userAgent}"
);

app.Run(); */




// 2) Cuando el framework ejecuta un handler para procesar la petición, si el resultado retornado es un
// objeto IResult, se ejecutará el método ExecuteAsync definido por la interfaz para que se genere la respuesta
// del IResult. Este método, al que se envía el contexto HTTP, se utilizado para modificar el StatusCode,
// añadir encabezados o componer el código de respuesta.
// Podemos definir nuestros resultados personalizados si lo necesitamos. Para ello creamos una clase,
// por ejemplo CustomErrorResult y la usamos en la devolución del caso anterior para un divisor 0.
// Esto permitió modificar el comportamiento del objeto IResult que devuelve el handler.
/* using Microsoft.AspNetCore.Mvc;
using Routing;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ICalculatorServices, CalculatorServices>();

var app = builder.Build();

app.MapGet("/add", CalculatorHandlers.Add);

app.MapPost("/friends", (Friend friend) => $"Recibido Friend con nombre: {friend.Name}");

app.MapGet("/multiply/{a}/{b}", (int a, int b, ICalculatorServices calculator) => calculator.Multiply(a, b));

// localhost:xxxx/divide?a=1&b=0 => Error 999 y devuelve el texto "Error terrible: Divisor incorrecto"
app.MapGet("/divide", (int a, int b) =>
{
   if (b == 0)
   {
      return Results.BadRequest();
   }
   return Results.Ok(a/b);
});

app.MapGet("/", ([FromHeader(Name = "user-agent")] string userAgent,
                 [FromHeader(Name = "accept-language")] string lang)
                    => $"Lang: {lang}, user agent: {userAgent}"
);

app.Run(); */



// 3) Devolver en el handler cualquier objeto que no sea un string o una implementación de IResult.
// En este caso, se serializará el resultado y se devolverá al cliente con el tipo "application/json"
using Microsoft.AspNetCore.Mvc;
using Routing;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ICalculatorServices, CalculatorServices>();

var app = builder.Build();

app.MapGet("/add", CalculatorHandlers.Add);

app.MapPost("/friends", (Friend friend) => $"Recibido Friend con nombre: {friend.Name}");

// Ejemplo de devolución de objeto Friend. Se serializa y se envía al cliente como un JSON.
// localhost:xxxx/friend => Estado 200, content-type: application/json. En pantalla se muestra el objeto JSON
app.MapGet("/friend", () => new Friend { Name = "John", Age = 34 });

app.MapGet("/multiply/{a}/{b}", (int a, int b, ICalculatorServices calculator) => calculator.Multiply(a, b));


app.MapGet("/divide", (int a, int b) =>
{
   if (b == 0)
   {
      return Results.BadRequest();
   }
   return Results.Ok(a/b);
});

app.MapGet("/", ([FromHeader(Name = "user-agent")] string userAgent,
                 [FromHeader(Name = "accept-language")] string lang)
                    => $"Lang: {lang}, user agent: {userAgent}"
);

app.Run();