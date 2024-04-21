/*
5.8.1 DEMO: Binding simple.
Veremos el funcionamoento de Model Binding en MVC que es el mecanismo encargado de obtener, transformar y suministrar los valores de los parámetros
de nuestros métodos de acción.
1) Creamos un controlador BindController.
2) Crear método Test dentro del controlador, que recibe varios parámetros de diferente tipo.
3) Comprobamos que funciona en el navegador usando la dirección https://localhost:xxxx/bind/test?i=1&b=true&s=Hello&d=12.12&array=1&array=2
Observamos que los parámetros se han obtenido y transformado automáticamente en el tipo de dato esperado en la acción. Esto es posible porque
el binding usa unos componentes llamados proveedores de valores (value providers) que obtienen la información desde la información presente en el
contexto de la petición, sea desde:
  - campos de formulario
  - parámetros de ruta
  - parámetros de query string
Los value providers son consultados de forma secuencial para cada parámetro de nuestra acción, terminando cuando encuentre un value provider capaz de
proporcionar un valor para el parámetro que se le está solicitando. Por ejemplo, para el parámetro i, primero busca si se está enviando en un formulario con
el nombre de campo i, luego busca en los parámetros de ruta, y finalmente lo obtiene de la query string.
4) Añadir una ruta a Test de esta forma: /bind/test/{i}
5) Al volver al navegador probamos con https://localhost:xxxx/bind/test/18?i=1&b=true&s=hello&d=12.12&array=1&array=2
Ahora estamos mandando el parámetro i por duplicado, pero se queda con el suministrado por la ruta (parámetro de ruta)

*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
