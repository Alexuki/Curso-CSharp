// 5.2 PRÁCTICA 1: Aplicaciones MVC y routing
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

// Configuración de rutas original
/* app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); */

// Modificación del formato de rutas que procesará nuestra aplicación
// Ahora no entiende direcciones como / o /home/about
// Ahora las direcciones de acceso a los controladores deben ser de laa forma /test o /test/home/index
app.MapControllerRoute(
    name: "default",
    pattern: "test/{controller=Home}/{action=Index}/{id?}");

app.Run();
