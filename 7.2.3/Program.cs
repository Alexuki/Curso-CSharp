/*
7.2.3 DEMO: Attribute routing y restricciones
Partimos de una aplicación Asp.NET Core MVC en blanco, solo con la clase Program para inicializar.

1) Añadir los maperos de los endpoints y del endpoint fallback.
2) Añadir en Controllers la clase CalculatorController.
3) Arrancamos la aplicación. Al usar la ruta por defecto, podemos acceder a la acción indicando toda la ruta:
controlador/acción y pasando los parámetros en la query string. Ejemplo: https:xxxx/calculator/sum?a=1&b=2
4) Si queremos acceder con una ruta más apropiada como /calculator/1/2, tendremos que mapear esta ruta con la acción Sum.
Puede hacerse por enrutado por convenciones o, más simple, utilizando el atributo Route.
5) Utilizamos una ruta personalizada mediante Route. Ahora, si intentamos acceder con la forma anterior, ya no funcionará.
Esto sucede porque aunque estamos usando la ruta por defecto, el Attribute Routing la está sobreescribiendo, haciéndola inaccesible
mediante rutas definidas por convención.
IMPORTANTE: Cuando se usa Attribute Routing, es necesario nombrar los parámetros de la ruta con los mismos nombres que los parámetros de la acción.
Además, tampoco va a funcionar si omitimos un parámetro de la ruta porque no encajará con la plantilla, pero esto podemos solucionarlo indicando que son opcionales.
En este caso, si se pone el parámetro b como opcional, si en la ruta no se pasa, tomará el valor 0 por defecto. Si se quiere usar otro valor por defecto,
lo pondremos en el parámetro de la acción o en la propia ruta.
6) Crear acción Product.
7) Como en las diferentes acciones se repite la parte de la ruta "calculadora", usaremos los mecanismos de ASP.Net Core para ascenderla y marcarla a nivel de controlador,
ya que normalmente sus rutas van a emprzar de esta forma.
7) Utilizar un token para que el nombre de la ruta esté sincronizado con el nombre del controlador, de forma que si cambiamos el nombre de la clase, se actualice el de la ruta.
8) Aplicar token para las acciones.
9) Al hacer estos cambios, ahora tenemos otra parte común que es [action], por lo que podemos ascenderla al Route del nivel de clase.
10) Observamos que los parámetros de las acciones son los mismos, con lo que también podemos ascenderlos al Route del nivel de clase.
11) Aplicar constricciones en línea en Attribute Routing.
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.MapFallback(
    ctx => ctx.Response.WriteAsync("No endpoint selected"));

app.Run();
