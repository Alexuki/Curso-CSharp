/*
7.9.3 DEMO: Validaciones
1) Añadir la clase Friend en Models, con las propiedades Name, Age y Email.
2) Añadir FriendsController que se encarga de enviar la vista y recibir los datos del formulario. tendremos dos acciones Create (una GET y otra POST).
3) Añadir la vista.
4) Probamos el proyecto. Comprobamos que los datos llegan correctamente.
5) Incluir anotaciones a las clases de datos para añadir restricciones y reglas de validación que queremos que se apliquen a cada campo. En la clase Friend pondremos:
    - Propiedad Name obligatoria. Longitud entre 5 y 50 caracteres.
    - Age con rango válido 0-120.
    - Email obligatorio y uso de atributo que valida que es un email.
6) En la acción POST del controlador añadimos un if que realiza la comprobación de los datos introducidos.
7) Probamos la aplicación introduciendo diferentes datos para comprobar el funcionamiento de las validaciones.
8) Es más práctico saber en qué campos se producen los errores. A nivel de vista podremos saberlo gracias a la propiedad ViewContext.ModelState,
que contiene información sobre las validaciones y el estado de los campos. La utilizaremos en la vista así:
<label>Name:</label> @ViewContext.ModelState.IsValid
La propiedad IsValid nos dice si el modelo es válido.
Normalmente no accederemos directamente al ModelState sino que usaremos helpers que nos permitirán acceder a la información de forma
más sencilla. Por ejemplo, para mosrar errores en un campo, podemos usar Html.ValidationMessage indicando el nombre del campo para mostrar el error de validación.
9) Vamos al controlador y en la comprobación del if, en lugar de devolver un string "Not valid", retornaremos la misma vista que se retornaba en la primera petición.
10) Si introducimos datos incorrectos, nos cargará la vista del formulario en blanco, y nos mostrará el error de validación junto al label del formulario. Ejemplo:
"Name: The field Name must be a string with a minimum length of 5 ans a maximum length of 50."
Los campos aparecen en blanco porque en el html no le hemos indicado que los valores por defecto tengan que establecerse.
11) Vamos a corregir que los campos del formulario no aparezcan en blanco. En el Controlador, indicamos que cuando se produzcan errores de validación, que se devuelva la
vista pero pasándole el objeto friend que nos ha llegado. Como ahora la vista tiene el objeto friend, podemos añadir en ella el @model Friend y usarlo para poner los
valores por defecto. Como ahora la vista está esperando un objeto Friend, en la acción Create del GET también le pasaremos el objeto. Si no, la vista fallará al no recibir
el modelo.
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
