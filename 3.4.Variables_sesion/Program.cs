var builder = WebApplication.CreateBuilder(args);
// Añadir servicios de caché distribuido y de estado de sesión. Este último hace uso del anterior.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


var app = builder.Build();
// Añadir middleware de sesión. Genera la cookie que identifica al usuario y se utiliza en el diccionario de valores
// de variables de sesión de los diferentes usuarios
app.UseSession(new SessionOptions()
{
    Cookie =
    {
        Name = ".CookiePersonalizada",
        HttpOnly = true,
    },
    IdleTimeout = TimeSpan.FromSeconds(10)
});

app.MapGet("visits", (HttpContext ctx) =>
{
    var newCount = ctx.Session.GetInt32("count").GetValueOrDefault() + 1;
    ctx.Session.SetInt32("count", newCount);
    return $"Your visits: {newCount}";
});

// Elimina las variables de sesión del usuario
app.MapGet("reset", (HttpContext ctx) =>
{
    ctx.Session.Clear();
});



app.Run();
