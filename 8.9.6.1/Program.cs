/*
8.9.6.1 DEMO: Validación en cliente
Veremos cómo funciona la validación en el lado de cliente de aplicaciones ASP.NET Core MVC.
1) Partimos de una aplicación vacía usando el template mvc.
2) Creamos un view model CreateUserViewModel.
3) Creamos UsersController con acción Create que retornará una vista con el formulario de edición del view model.
4) Creamos la vista. Añadimos el formulario con Html.BeginForm() y las propiedades.
En las propiedades añadimos una etiqueta, un mensaje de validación y un textBox por cada campo del view model.
5) En el controlador, en la acción Create añadimos el objeto CreateUserViewModel a la vista.
6) En el controlador, añadimos la acción que recibirá los datos. En ella se comprueba si el ModelState es válido.
Si no lo es, devuelve la vista con los datos del view model pasado.
Si es correcto, guarda los datos del usuario y devuelve un mensaje "Ok".
7) Probamos la aplicación en /users/create. Si lo enviamos vacío, pulsando el botón Create, nos llega el mensaje de error
y la hora se va actualizando. Si completamos los datos, nos devuelve "Ok"
8) Volvemos a la página del formulario /users/create e inspeccionamos el código fuente de la página generada.
Vemos que las reglas de validación están incluidas en el marcado HTML. Ejemplo:
<input data-val="true" data-val-length-max="29" ...>
Al marcar el view model con atributos de validación, hace que el marcado HTML los lleve.
Estos atributos pueden ser aprovechados por bibliotecas de JavaScript que van a ser capaces de interpretarlos 
y añadir la lógica de validación en el cliente para no tener que enviar los datos al servidor para comprobar que
están bien.
9) Vamos a añadir en la vista una sección llamada "scripts" fuera del formulario. En ella cargaremos la vista 
parcial que contiene los scripts de validación.
10) Volvemos a la pantalla del formulario y probamos a poner datos inválidos. Al pulsar en el botón "Create",
vemos que la hora no se actualiza porque la validación la está haciendo el propio navegador y no llega a enviar
los datos al servidor.
11) Ponemos datos válidos y comprobamos que funciona correctamente.
12) La validación en cliente mejora la experiencia de usuario porque le ofrece un feedback muy temprano
sobre los errores que está introduciendo en los formularios.
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
