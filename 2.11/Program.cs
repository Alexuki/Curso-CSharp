//2.11 PRÁCTICA 1: inicialización, middlewares e inyección de dependencias

// Parte 1: Múltiples entornos

// 1.1: Mostrar entonro actual
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) => 
{
    var msg = $"Current environment: {app.Environment.EnvironmentName}";
    await context.Response.WriteAsync(msg);
});

app.Run(); */



// 1.2: Añadir middlewares al pipeline en función del entorno actual
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.Run(async (context) => 
    {
        await context.Response.WriteAsync("Development environment");
    });
}
else
{
    app.Run(async (context) => 
    {
        await context.Response.WriteAsync("No development environment");
    });
}

app.Run(); */



// Parte 2: Middlewares

// 2.1: Añadir middlewares
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (ctx, next) => 
{
    if (ctx.Request.Path == "/hello-world")
    {
        // Procesa la petición y no permite la ejecución de middlewares posteriores
        await ctx.Response.WriteAsync("Hello world!");
    }
    else
    {
        // Pasa el control al siguiente middleware
        await next(ctx);
    }
    
});

// Request Info middleware
app.Run(async ctx => 
{
    await ctx.Response.WriteAsync($"Path requested: {ctx.Request.Path}");
});

app.Run(); */


// 2.2: Insertar un middleware para que sea el primero en el pipeline de ASP.NET Core:
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (ctx, next) => 
{
    if (ctx.Request.Path.ToString().StartsWith("/hello"))
    {
        // Procesa la petición y no permite la ejecución de otros middlewares
        await ctx.Response.WriteAsync("Hello, user!");
    }
    else if (ctx.Request.Path == "/hello-world")
    {
        // Procesa la petición y no permite la ejecución de middlewares posteriores
        await ctx.Response.WriteAsync("Hello world!");
    }
    else
    {
        // Pasa el control al siguiente middleware
        await next(ctx);
    }
    
});

// Request Info middleware
app.Run(async ctx => 
{
    await ctx.Response.WriteAsync($"Path requested: {ctx.Request.Path}");
});

app.Run(); */

// 2.3: Modificar orden de los middlewares del pipeline para que las peticiones de hello-world sean procesadas de forma correcta:
/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (ctx, next) => 
{
    if (ctx.Request.Path == "/hello-world")
    {
        // Procesa la petición y no permite la ejecución de middlewares posteriores
        await ctx.Response.WriteAsync("Hello world!");
    }
    else if (ctx.Request.Path.ToString().StartsWith("/hello"))
    {
        // Procesa la petición y no permite la ejecución de otros middlewares
        await ctx.Response.WriteAsync("Hello, user!");
    }
    else
    {
        // Pasa el control al siguiente middleware
        await next(ctx);
    }
    
});

// Request Info middleware
app.Run(async ctx => 
{
    await ctx.Response.WriteAsync($"Path requested: {ctx.Request.Path}");
});

app.Run(); */


// Parte 3: Inyección de dependencias

//3.1 Crear servicio Cslculadora y obtenerlo
/* var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IAdder, BasicCalculator>();

var app = builder.Build();

app.Run(async (context) =>
{
    if(context.Request.Path == "/add")
    {
        int a = 0, b = 0;
        int.TryParse(context.Request.Query["a"], out a);
        int.TryParse(context.Request.Query["b"], out b);

        var adder = context.RequestServices.GetService<IAdder>();
        await context.Response.WriteAsync(adder.Add(a, b));
    }
    else
    {
         await context.Response.WriteAsync("Try again!");
    }
});

app.Run();


public interface IAdder
{
    string Add(int a, int b);
}

public class BasicCalculator: IAdder
{
    public string Add(int a , int b) => $"{a} + {b} = {a+b}";
} */



// 3.2 Crear componente para formatear expresiones, dejando que la calculadora solo se ocupe de los cálculos
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IAdder, BasicCalculator>();
builder.Services.AddTransient<IOperationFormatter, OperationFormatter>();

var app = builder.Build();

app.Run(async (context) =>
{
    if(context.Request.Path == "/add")
    {
        int a = 0, b = 0;
        int.TryParse(context.Request.Query["a"], out a);
        int.TryParse(context.Request.Query["b"], out b);

        var adder = context.RequestServices.GetService<IAdder>();
        await context.Response.WriteAsync(adder.Add(a, b));
    }
    else
    {
         await context.Response.WriteAsync("Try again!");
    }
});

app.Run();


public interface IAdder
{
    string Add(int a, int b);
}

public class BasicCalculator: IAdder
{
    private readonly IOperationFormatter _formatter;

    public BasicCalculator(IOperationFormatter formatter)
    {
        _formatter = formatter;
    }
    public string Add(int a , int b) => _formatter.Format(a, "+", b, a+b);
}

public interface IOperationFormatter
{
    string Format(int a, string operation, int b, int result);
}

public class OperationFormatter : IOperationFormatter
{
    public string Format(int a, string operation, int b, int result)
    {
        return $"{a}{operation}{b}={result}";
    }
}


