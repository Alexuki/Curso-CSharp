/*
8.9.8.1 DEMO: Templated helpers
1) Partimos de la aplicación de la demo anterior 8.9.6.1 que tenía implementado un formulario para crear usuarios.
2) Contamos con un UsersControllers, un view model con las restricciones de los campos del formulario
y una vista en que se compone el formulario usando helpers como TextBoxFor para generar
el marcado HTML y le pasamos la propiedad como parámetro.
Otra forma de hacerlo es usando EditorFor y pasarle la propiedad como parámetro. MVC la analizará
y en función de su tipo y las anotaciones añadidas, determinará el control de edición a utilizar.
3) Usar EditorFor en el campo UserName. Razor buscará una plantilla específica para la propiedad.
Si no la encuentra, usará un editor por defecto (para los strings usará normalmente un cuadro de
texto, para los booleanos un checkbox...). En este caso, el formulario se va a ver igual.
4) Crear plantilla personalizada para el tipo string que se aplicará en el campo anterior
gracias al uso de EditorFor, en Views/Shared/EditorTemplates/String.cshtml.
En la vista le indicamos (en model) que va a recibir un string y lo que debe hacer es crear un TextBox.
5) Comprobamos que el campo Username ha cambiado de acuerdo a la plantilla personalizada.
6) Desde la plantilla genérica también podemos acceder a los metadatos de la propiedad en edición.
Por ejemplo, podemos comprobar si el campo es obligatorio para resaltarlo. Añadimos en la plantilla
que aplique un estilo para los campos obligatorios.
7) Cambiamos en la vista Create todos los campos TextFor por EditorFor. Los campos que cambian son
Username y FullName. Email no cambia porque además de ser un campo de tipo string, tiene a mayores
la anotación EmailAddress y el sistema intentará encontrar un template que siga EmailAddress.
8) Creamos un template para EmailAddress. Comprobamos que a partir de entonces la usa al renderizar la vista.
9) Los campos de password no cambian porque en la vista se está usando PasswordFor. Podemos cambiarlo
por EditorFor, pero entonces en el modelo tenemos que añadir una anotación DataType en dichas propiedades.
10) Creamos una plantilla para Password. En ella usamos Html.Password en lugar de Html.TextBox.
11) EditorFor intenta localizar una plantilla de edición apropiada para cada propiedad. Si no existe,
aporta una plantilla por defecto. Esto lo hace tanto para tipos simples (string, int...) como para tipos
complejos como el CreateUserViewModel. Cuando se pasa un tipo complejo a EditorFor, va a ir recorriendo sus
propiedades y llamando a EditorFor para cada una de ellas, facilitando la creación de interfaces de edición
de forma rápida y automática.
12) En la vista Create borramos todo el formulario, dejando solo el botón Create, y lo creamos a partir de
EditorForModel. Al volver a la página, veremos que no hay cambios porque EditorFor ha iterado sobre todas
las propiedades y ha mostrado lo mismo que habíamos hecho nosotros.
13) Además de plantillas de esición, tenemos de visualización. Vamos a UsersController y en lugar de 
retornar un Content("Ok") vamos a retornar una vista llamada "Details" a la que pasamos el view model.
14) Añadimos la vista en Views/Users. En ella usamos DisplayForModel que va a buscar una plantilla de
visualización para la clase indicada (CreateUserViewmodel en este caso). Si no la encuentra, como sucede
aquí, va a recorrer las propiedades y buscar una plantilla de visualización para cada una.
15) Introducimos unos datos en el formulario y pulsamos en Create. Se devuelve una vista por defecto.
Usando las herramientas de desarrollador, podemos ver que a cada propiedad le añade una clase css
para que podamos editarlas cómodamente. Ejemplo:
<div class="display-label">Username</div>
<div class="display-field">Josh</div>
16) Similar a lo que hicimos antes, podemos crear una vista en Shared/DisplayTemplates que se llame como
el modelo CreateUserViewModel.cshtml. Al enviar el formulario, nos aplicará el display template, usando
una tabla que hemos definido en él.
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
