/*
DEMO: Routing: Constraints
En la definición de la ruta podemos introducir constraints (requisitos).
Si la ruta no cumple los requisitos, muestra un error 404.
*/

// 1) En este ejemplo, solo acepta letras y debe tener un mínimo de 2 caracteres.
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/hello/{name:alpha:minlength(2)}", (string name) => $"Hello {name}!")
        .WithDisplayName("Hello endpoint");

app.Run(); */



// 2) Crear nuestras propias reglas de validación. Ejemplo: ruta solo accesible cuando el nombre comience por Alberts.
// Creamos un archivo con  una clase que llamamos, por ejemplo, OnlyAlbertsConstraint y le añadimos una regla de validación.
// Registramos la constraint en el inyector de dependencias. Para ello, añadimos el Routing y lo configuramos en un lambda.
// En options añadimos la constraint, dándole un nombre  eindicando su tipo. Al añadirla, podemos utilizarla inline como las anteriores.
// Ahora las rutas admitidas serán /hello/albert... Por ejemplo, son válidas: /hello/alberto, /hello/albert-einstein, etc.
using Routing;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(opt =>
{
        opt.ConstraintMap.Add("is-albert", typeof(OnlyAlbertsConstraint));
});

var app = builder.Build();

app.MapGet("/hello/{name:is-albert}", (string name) => $"Hello {name}!")
        .WithDisplayName("Hello endpoint");

app.Run();