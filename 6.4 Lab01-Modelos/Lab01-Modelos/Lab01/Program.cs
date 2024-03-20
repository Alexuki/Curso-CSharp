using Lab01.Models.Repositories;
using Lab01.Models.Repositories.Database;
using Lab01.Models.Repositories.InMemory;
using Lab01.Models.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBlogServices, BlogServices>();

// ** Descomenta la siguiente l�nea para usar el repositorio en memoria
//builder.Services.AddScoped<IPostsRepository, InMemoryPostsRepository>();

// ** Descomenta el siguiente bloque para usar el repositorio basado en EF con SQLite:
builder.Services.AddScoped<IPostsRepository, SqlitePostsRepository>();
builder.Services.AddDbContext<BlogDataContext>();
SqlitePostsRepository.InitializeDatabase();

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
    name: "archive",
    pattern: "blog/archive/{year}/{month}",
    defaults: new { controller = "Blog", action = "Archive" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
