/*
8.6 PRÁCTICA 1: View engines y codificación Razor

- Cambiar la convención de ubicación de las vistas en Razor
1) Crear aplicación MVC usando la plantilla que incluye algunas páginas (Home, Privacy...)
2) Modificar la configuración por defecto del motor de vistas Razor para hacer que los archivos se busquen en otras ubicaciones.
3) Modificar el cuerpo de delegado añadiendo las ubicaciones /Features/{1}/Views/{0}.cshtml y /Features/Shared/{0}.cshtml
4) Mover los controladores y vistas según la nueva convención. También es necesario cambiar la ubicación de las plantillas .cshtml de inicialización ViewStart y ViewImports
para que afecten a todas las vistas que hay por debajo en la estructura de carpetas.

- Codificación de vistas con Razor
5) Añadir controlador Math con acción Multiplication que muestra la tabla de multiplicar del número suministrado como parámetro.
6) Añadir en el sistema de routing que se llegue a la acción usando una URL como /math/multiplication/5 y sólo si el último segmento de la ruta es de tipo entero.
7) Implementar qu la acción retorn una vista tipada a la que se pasa el valor del parámetro number.
8) Crear la vista y, sin usar CSS, mostrar las filas de la tabla alternando los colores de fondo.
9) Modificar la página de inicio del sitio web para que aparezca una lista de enlaces hacia la tabla de multiplicar de los primeros 20 números naturales.

- Secciones
10) Abrir el Layout y sustituir footer por una instrucción que incluya el contenido de una sección "Footer" que, opcionalmente, implementarán las páginas de contenido que utilicen este Layout.
Si la plantilla no incluye etiquetas <footer>, introducir la sección antes del cierre de la etiqueta <body>.
11) Incluir en cada vista un contenido distinto para la sección "Footer".
12) Para vistas que no tienen definida la sección Footer como /home/about o /home/contact, generar un contenido por defecto desde el Layout.

- Vistas parciales
13) Existe una porción de código común en las vistas anteriores: &copy; @DateTime.Now.Year All rights reserved
que podríamos externalizar a una vista parcial para evitar código duplicado y facilitar el mantenimiento posterior.
14) Añadir al proyecto el archivo _Copyright.cshtml en la carpeta Shared porque es una vista compartida entre varios controladores, con el mensaje que se repite.
15) Añadir llamada @await Html.PartialAsync("_Copyright") en el lugar de las vistas donde queremos incluir el contenido de la vista parcial creado anteriormente.
*/

var builder = WebApplication.CreateBuilder(args);

//(2)
builder.Services.AddControllersWithViews()
    .AddRazorOptions(opt =>
    {
        
        opt.ViewLocationFormats.Clear();
        // Inicialmente contiene una colección con las rutas /Views/{1}/{0}.cshtml y /Views/Shared/{0}.cshtml
        // Los parámetros {1} y {0} serán sustituidos en tiempo de ejecución por el nombre del controlador actual y de la vista, respectivamente.
        // Configurar nuevas ubicaciones aquí (3)
        opt.ViewLocationFormats.Add("/Features/{1}/Views/{0}.cshtml");
        opt.ViewLocationFormats.Add("/Features/Shared/Views/{0}.cshtml");
    });
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
