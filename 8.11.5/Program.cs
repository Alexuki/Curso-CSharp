/*
8.11.5 DEMO: tag helpers & cache tag helpers
1) Partimos de una aplicación MVC vacía con 2 controladores: ProductsController y CustomersController.
2) Crear el view model a utilizar.
3) Crear vista Views/Customers/Create.cshtml, pero de momento la dejamos vacía (solo con el título mostrado en un h1).
4) Vamos a la vista Home/Index.cshtml. Borramos el contenido, y en ella añadimos un helper tradicional para una ruta.
Los parámetros de la acción y los atributos del tag se pasan como objetos anónimos.
5) Probamos que el enlace funciona y, usando las herramientas del navegador, su contenido:
<a class="link" href="/products/Computers/18">Go to product 18</a>
La sintaxis que hemos utilizado para generar el enlace está alejada de la sintaxis HTML. Podemos hacerlo de una manera
más cercana al HTML tradicional usando tag helpers.
6) Introduciremos el enlace anterior usando la etiqueta <a> pero, en lugar de usar href, utilizaremos atributos especiales
que nos proporciona el tag helper. Los atributos de clases, estilos y demás los podemos añadir como en el HTML normal.
7) Comprobamos que se genera el enlace correctamente, y los inspeccionamos para ver que es igual que el creado anteriormente.
Es más legible para personas no acostumbradas a sintaxis Razor.
8) Uso de tag helpers para crear formularios. Vamos a la vista Create que teníamos vacía.
Hasta ahora, los formularios los creábamos con:
@using (Html.BeginForm())
{

}
En su lugar usaremos, mediante tag helpers, la etiqueta form habitual y los atributos especiales para indicar controlador,
acción, etc.
9) En el cuerpo del usuario usaremos HTML tradicional para crear la interfaz. Añadiremos clases BootStrap como "form-group"
y etiquetas <span> para mostrar los errores de validación.
10) En el navegador vamos a /customers/create y vemos que ha generado el formulario. El editor utilizado para generar en este
caso, depende del tipo de dato y de las anotaciones propias indicadas en el view model.
Inspeccionando el HTML generado, vemos que el type para el campo Name es "text", y para el campo Email es "email".
Se observa que incluye los atributos de validación, por lo que si se incluyese la página de scripts de validación en cliente,
funcionaría correctamente.
11) Para el control de tipo select, indicamos que obtenga los items de @Html.GetEnumSelectList(typeof(CustomerType)).
Si da error de que no reconoce el tipo de CustomerType, podemos añadir el nombre completo en @model (ejemplo: 
@model DEMO_Tag_helpers.Models.ViewModels.CreateCustomerViewModel) o bien, de forma más
elegante, añadir el using en _ViewImports.cshtml (ejemplo: @using DEMO_Tag_helpers.Models.ViewModels). Usando esta última opción,
ya no es necesario usar el nombre completo en la vista para que encuentre la clase, pudiendo poner @ model CreateCustomerViewModel.
12) Comprobamos que el formulario funciona correctamente y que también muestra las validaciones.

Los tag helpers vistos extienden de etiquetas HTML existentes pero hay otros que definen etiquetas nuevas.
13) En la vista de Index vamos a introducir la etiqueta environment con names "Development", de forma que el marcado que
incluyamos en su interior solo se añada cuando nos encontremos en dicho entorno. También aádimos para los otros entornos.
14) Ejecutamos la aplicación y se muestra el mensaje de Development. Si en las propiedades del proyecto cambiamos
la variable de entrono por "Production", mostrará el otro marcado.
15) Tag helper cache: permite cachear el contenido el tiempo que nos interese, de forma que no sea evaluado de nuevo
hasta que la caché expire. En el Index añadiremos un párrafo con la hora actual sin cachear y debajo de él la hora cacheada
durante 5 segundos. Al refrescar la página, vemos que el dato cacheado se mantiene durante el tiempo indicado.
16) Podemos usar el atributo expires-sliding para indicarle cuánto tiempo permance el contenido en la caché desde la última vez
que fue consultado. En este caso, si refrescamos la página de forma constante, el valor no cambia porque no ha transcurrido el
tiempo indicado (3 segundos en este ejemplo) desde la última vez que fue consultado. Si esperamos un rato mayor a 3 segundos,
sí se actualizará cuando refresquemos la página.
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
