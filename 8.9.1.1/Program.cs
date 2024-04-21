/*
9.1.1 DEMO: Generación de enlaces
Veremos la forma correcta de crear enlaces en aplicaciones ASP.NET Core MVC
1) Vamos a la vista Index del Home para crear enlaces.
2) La forma mostrada funciona pero habrá problemas si en la configuración de la aplicación se cambia la convención de rutas
porque queremos las funcionalidades disponibles de acuerdo a un nuevo esquema. Por ejemplo, en MapsControllerRoute cambiamos el pattern.
Al intentar usar el enlace, nos dará un error Not Found (404).
3) Ahora, para acceder a la aplicación hay que poner localhost:xxxx/myapp y nos llevará a Home, pero el enlace no funcionará porque está
escrito en el código y no apunta a /myapp/home/privacy. No acepta los cambios.
4) Para solucionar esto y que los enlaces tengan en cuenta el sistema de rutas, usaremos el helper ActionLink.
5) El helper contiene muchas sobrecargas, admitiendo parámetros para configurar cómo se va a generar el enlace.
Añadimos una acción nueva Say en HomeController, con ruta /say/{message}/{to}. Comprobamos que funciona con localhost:xxxx/hello/john.
6)  En la vista Index creamos un enlace a la ruta anterior pasándole los parámetros.
7) Otras propiedades del helper permiten definir propiedades del tag <a>
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
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "myapp/{controller=Home}/{action=Index}/{id?}"); //(2)

app.Run();
