/*
8.5 DEMO: Los dos modos de Razor
Veremos la implementación de vistas usando la sintaxis Razor.
1) Partimos de una aplicación MVC vacía.
2) Crear controlador MathController con acción Multiplication que recibe un entero y devuelve una vista con la tabla de multiplicar de ese número.
Usaremos enrutado por atributos para marcar la ruta.
3) Creamos la vista en la ubicación por convención, dentro de Views/Math.
4) Cuando arrancamos una vista, por defecto estamos en modo marcado (el parser de Razor espera encontrar marcado html). Podemos comprobarlo al escribir la
apertura de etiqueta "<", que IntelliSense nos ofrece etiquetas para completar.
5) Como en esta vista usaremos un modelo recibido del controlador, lo especificamos en @model, indicando que es de tipo int.
6) Incluimos un encabezado h2 mostrando el valor del modelo recibido con @Model. Al pasar el ratón sobre esta referencia, nos indica que es de tipo int.
7) Razor continúa en modo marcado. Si queremos incluir código C# a ejecutar en el lado del servidor, lo introduciremos precedido de "@".
8) Crearemos un bloque de código para introducir el título de la página, el cual se insertaba en el Layout mediante el ViewData:
@{
    ViewData["Title"] = $"Multiplication table of {@Model}";
}
Se usa interpolación se strings para incluir el contenido del modelo.
9) Añadimos los resultados de la tabla de multiplicar usando una lista desordenada que incluye un bucle for.
Esto es código C# que se ejecuta en el servidor. Si dentro del bloque de código queremos  volver a escribir HTML, podemos mezclarlos porque el parser
detecta aperturas y cierres de etiquetas y es capaz de pasar a modo marcado cuando es necesario.
10) Vamos a implementar lógica de presentación para mostrar las filas pares e impares de forma diferente. Dentro de la partedel bucle for donde Razor
se encuentra en modo código, podremos asignar variables que luego podremos usar en el marcado.
11) Añadir algo más de lógica para que los resultados que sean múltiplos de 10 terminen con un determinado texto.
12) En el ejemplo anterior, se está añadiendo una etiqueta <span> cuando se envía al cliente la fila que es múltiplo de 10.
Puede que no nos interese enviar marcado extra al cliente. Para ello, podemos usar la etiqueta <text> contemplada por Razor para estos casos. Es una etiqueta
virtual que solo entiende el parser de Razor, y lo que hace es capturar todo lo que hay en su interior y lo envía al cliente eliminando la propia etiqueta.
Otra opción es usar "@:" que indica que todo lo que aparece a continuación en la misma línea, es contenido a enviar literalmente al cliente
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
