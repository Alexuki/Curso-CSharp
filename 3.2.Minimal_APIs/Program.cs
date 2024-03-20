// 3.2 PRÁCTICA 1: Minimal APIs

// 1. Routing
/* var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICalculatorServices, CalculatorServices>();
builder.Services.AddTransient<ICalculationEngine, CalculationEngine>();

var app = builder.Build();

app.MapGet("/calculator/{operation}/{a}/{b}", CalculatorHandler.Calculate);

app.Run(); */




// 2. Mapeos más elegantes
/* var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICalculatorServices, CalculatorServices>();
builder.Services.AddTransient<ICalculationEngine, CalculationEngine>();

var app = builder.Build();

// Uso de extensor que mapea todo lo relativo a la calculadora
app.MapCalculator("/calculator/{operation}/{a}/{b}");

app.Run(); */



// 3. Aplicación de constraints
/* var builder = WebApplication.CreateBuilder(args);

// Registrar la constraint para poder utilizarla
builder.Services.AddRouting(opt =>
{
    opt.ConstraintMap.Add("valid-operation", typeof(ValidOperationConstraint));
});

builder.Services.AddTransient<ICalculatorServices, CalculatorServices>();
builder.Services.AddTransient<ICalculationEngine, CalculationEngine>();

var app = builder.Build();
// Aplicación de restricciones para evitar excepciones: a y b deben ser enteros y operation usará una restricción personalizada
app.MapCalculator("/calculator/{operation:valid-operation}/{a:int}/{b:int}");

app.Run(); */



// 4. Formularios
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(opt =>
{
    opt.ConstraintMap.Add("valid-operation", typeof(ValidOperationConstraint));
});
// registrar servicios para Antiforgery
builder.Services.AddAntiforgery();

builder.Services.AddTransient<ICalculatorServices, CalculatorServices>();
builder.Services.AddTransient<ICalculationEngine, CalculationEngine>();

var app = builder.Build();
// Añadir middleware de Antiforgery al pipeline
app.UseAntiforgery();

// Map calculator endpoints
app.MapCalculator("/calculator/{operation:valid-operation}/{a:int}/{b:int}");

// Map Friend creation endpoints
app.MapFriendsCreation("/friends");

app.Run();