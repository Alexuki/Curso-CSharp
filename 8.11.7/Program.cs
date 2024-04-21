/*
8.11.7 PRÁCTICA 4: Tag helpers
1. Formularios de edición con tag helpers
1) Partir de la práctica 7.2 (8.9.7) donde se implementó un formualrio de edición de datos usando helpers HTML.
Se reescribirá usando tag helpers.
2) Modificar la vista de edición de la entidad, utilizando tag helpers en todos los controles de edición.


2. Uso del tag helper "Cache"
3) Introducir en la página principal del sitio web el siguiente código,
que no tiene sentido en escenarios reales pero sirve para practicar el caching:
@{
    await Task.Delay(5000);
}
<div class="jumbotron">
    <h1>Welcome to MVC!</h1>
    <h2>@DateTime.Now.ToString("T")</h2>
</div>

4) Cuando se accede a la página, el renderizado de la página espera cinco segundos.
5) Usando el tag helper <cache> haremos que este bloque se ejecute como máximo cada
30 segundos, de forma que ahora se cargará más rápido al ejecutar la aplicación.
6) Reemplazar el tag helper por <distributed-cache>, asignándole un nombre para que funcione.
7) Añadir a la página otro bloque <distributed-cache> con el mismo valor en el atributo name
pero con un contenido distinto. Comprobamos que al usar el mismo nombre, el sistema no es capaz
de identificar los bloques correctamente y el resultado se mezcla.


3. Tag helpers personalizados
3.1. Creación del tag helper "LoremIpsum"
8) Dado que el framework MVC no aporta ninguna convención ni orientación sobre dónde ubicar
este tipo de componentes, creamos la clase LoremIpsumTagHelper en /Extensions/TagHelpers.
9) Implementamos el código del tag helper de forma que se pueda usar de esta forma en el
Layout del proyecto:
...
<div class="container">
    <main role="main" class="pb-3">
        <div class="alert alert-info" generate-words="30"></div>
        @RenderBody()
    </main>
</div>

El atributo generate-words indica el número de palabras aleatorias a incluir.

10) Añadir el tag helper en _ViewImports.cshtml para que esté visible en las vistas, y lo
añadimos al Layout como se indicó en el punto anterior.
Para añadir todos los tag helpers del proyectos se indica con: @addTagHelper *, Nombre_del_proyecto
11) Al ejecutar la aplicación debe aparecer en todas las páginas un encabezado con palabras
aleatorias.
12) Introducir la llamada al tag helper en un bloque cacheado, de forma que las palabras solo
se generen cada 10 segundos. Durante ese tiempo, todos los visitantes recibirán el mismo mensaje.


3.2. Creación del tag helper "ContentWatcher"
Crear nuevo tag helper que actuará como etiqueta independiente y como extensor de etiquetas
existentes, llamado "ContentWatcher", que se encargará de reemplazar las palabras que le
indiquemos por asteriscos. Ejemplo:

@* Código Razor original *@
<div>
    <content-watcher block-words="friend, meet, asap">
        Hello, my friend! I'd like to meet you asap!
    </content-watcher>
</div>
<p block-words="yellow, blue">
    I have a yellow car, but I'd like it to be blue.
</p>

<!-- Resultado enviado al cliente: -->
<div>
    Hello, my ******! I'd like to **** you ****!
</div>
<p>
    I have a ****** car, but I'd like it to be ****.
</p>

13) Crear la clase ContentWatcherTagHelper con una propiedad que recoja el valor del atributo
block-words, y decorar la clase con [HtmlTargetElement] indicando que dicho atributo es
obligatorio.
14) Sobreescribir el método ProcessAsync() de TagHelper e implementar en él la lógica de
sustitución que necesitamos para cumplir los requisitos. Ejemplo:

public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
{
    ...
    var childContent = await output.GetChildContentAsync();
    string content = childContent.GetContent();
    ...    // Realizar los reemplazos aquí
    output.Content.SetContent(content);
}

NOTA: Hemos tenido que implementar ProcessAsync() en lugar de Process() porque
necesitábamos acceder a los contenidos de la etiqueta mediante una llamada asíncrona a
GetChildContentAsync().

15) Introducir el siguiente código en una vista para comprobar que el tag helper funciona:
<p block-words="yellow, blue">
    I have a yellow car, but I'd like it to be blue.
</p>

16) Comprobar que también podemos utilizar el tag helper como etiqueta independiente:
<content-watcher block-words="friend, meet, asap">
    Hello, my friend! I'd like to meet you asap!
</content-watcher>

*/


using Lab04.Models.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
