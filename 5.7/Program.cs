//5.7 DEMO: Añadiendo funcionalidades

/*
Añadir funcionalidades de un minimotor de blogs:

1. Show latest posts
=============================
    /blog

Endpoint que responderá a estas peticiones:
/blog        /index    
/{controller}/{action}/{id?}

Estamos usando la convención de rutas por defecto. Index será el action por defecto


2. Show posts by year and month
=============================
    /blog/archive/2013/2
    /blog/archive/2016/3

Patrón de ruta:
/blog/archive/{year}/{month}


3. Show latest posts using slug (post title)
=============================
    /blog/welcome-to-mvc
    /blog/hello-world

Patrón de ruta:
/blog/{slug}



Pasos:
1) Añadir el BlogController. Utilizará un servicio para obtener los posts.
2) Añadir en la carpeta de Models/Services la interfaz del servicio IBlogServices.
3) Crear clase Post en Models/Entities.
4) En Index devolver una vista de Index con los posts recuperados.
5) Añadir el servicio BlogServices con la lógica para recuperar los Posts.
6) Registrar en el inyector las instancias del servicio para que puedan resolverse.
7) Comprobar que funciona accediendo a la ruta /blog. Se produce una excepción porque no hemos creado la vista "Index" que le pedimos utilizar.
   En la excepción muestra las rutas en que ha realizado la búsqueda: /Views/Blog/Index.cshtml y /Views/Shared/Index.cshtml
8) Crear elemento RazorView Index.cshtml dentro de Views/Blog. En ella, para poder usar el modelo, utilizaremos la directiva @model junto con el
   dato que estamos recibiendo que, en este caso, es IEnumerable<Post>.
   Para poder usarla de esta forma, sin tener que añadir la ruta completa, todo el namespace, a Post, modificaremos el fichero _ViewImports.cshtml,
   añadiendo un using con el namespace de Entities.
9) Crear la vistaIndex.cshtml que realiza una iteración sobre los posts recibidos en el modelo y los lista. Podemos acceder a ellos mediante la propiedad Model
   que está disponible en todas las vistas, y que es del tipo indicado en la directiva @model. Además, debe coincidir con el tipo de datos que recibimos en el controlador.
10) Comprobar que ahora podemos acceder al listado en https://localhost:xxxx/Blog
11) Implementar funcionalidad 2. Hay varias formas de usar rutas personalizadas en nuestra aplicación, pero en este caso usaremos "enrutado por convención": mapear los
   endpoints con el patrón de ruta deseado, asegurando que las peticiones son procesadas por las acciones que nos interesan. Para ello, modificamos las líneas de
   app.MapControllerRoute.
12) Añadir la acción Archive en BlogController. Se debe añadir un  nuevo método al servicio para recuperar los posts por fecha, año y mes, parámetros que se reciben en la acción.
   Devolveremos los datos en una vista que crearemos posteriormente.
13) Crear el método del servicio GetPostsByDate(int year, int month).
14) Crear la vista que muestre los datos anteriores en Views/Blog/Archive.cshtml. Aprovecharemos para incluir lógica de representación, de forma que el color de fondo de los elementos
   pares e impares de la lista sea distinto. Es una forma de implementar lógica de representación en el servidor, por si lo necesitamos, en lugar de dar el estilo mediante CSS, que será
   lo que se utilice la mayoría de las veces.
15) Comprobamos que obtenemos los posts filtrados en https://localhost:xxxx/Blog/archive/2016/6
16) Implementar tercera funcionalidad, de mostrar posts a partir de su slug (título). Como el patrón de ruta no coincide con los usados previamente,
   tendremos que mapear nuestra ruta personalizada. En esta ocasión usaremos un mecanismo diferente del framework MVC: "Attribute Routing", en el que se aplica el atributo "Route"
   y se especifica el patrón de ruta para acceder a ese punto en concreto en el mismo punto donde se va aprocesar la petición, por lo que resulta más cómoda que definir la ruta durante
   la inicialización.
17) Añadimos la nueva acción en BlogController.
18) Añadimos el método en BlogServices.
19) Añadir la vista para esta acción.
20) Probamos a acceder a /Blog/welcome-to-mvc y a /blog/second-post

*/

using _5._7.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBlogServices, BlogServices>();

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


// Insertamos la ruta específica para la nueva acción
// Las partes fijas de la ruta son nuestro controlador y nuestra acción, y por parámetros recibiremos año y mes
// Como en el patrón de ruta no existen los parámetros "controller" y "action", le daremos los valores por defecto
// de ambos en "defaults". Así, MVC puede resolver las peticiones cuya ruta coincida con el patrón indicado, y
// serán procesadas por el controlador Blog y la acción Archive
app.MapControllerRoute(
    name: "archive", // <-- Nombre único de esta ruta
    pattern: "blog/archive/{year}/{month}",
    defaults: new {controller="Blog", action="Archive"}
);

// Esta es la ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
