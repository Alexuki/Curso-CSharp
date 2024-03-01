// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

// Versión 1
/* var app = WebApplication.Create();
app.MapGet("/", () => "Hi there!");
app.Run(); */



// Versión 2
/* var builder = WebApplication.CreateBuilder();
// Configurar el builder aquí, ejemplo si fuéramos
// a utilizar funcionalidades de autenticación:
builder.Services.AddAuthentication();
var app = WebApplication.Create();
app.MapGet("/", () => "Hi there!");
app.Run(); */



// Versión 3
/* var builder = WebApplication.CreateBuilder();
// Configurar el builder aquí, ejemplo si fuéramos
// a utilizar funcionalidades de autenticación:
builder.Services.AddAuthentication();
var app = WebApplication.Create();
app.MapGet("/", () => app.Environment.EnvironmentName);
app.Run(); */



// Versión 4
var builder = WebApplication.CreateBuilder();
// Configurar el builder aquí, ejemplo si fuéramos
// a utilizar funcionalidades de autenticación:
builder.Services.AddAuthentication();
var app = WebApplication.Create();
app.MapGet("/", () => app.Environment.IsDevelopment() ? "Hello Developer!" : "Hi user!");
app.Run();
