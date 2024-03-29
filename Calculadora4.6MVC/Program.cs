var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ICalculatorServices, CalculatorServices>();
builder.Services.AddTransient<ICalculationEngine, CalculationEngine>();


var app = builder.Build();

app.MapDefaultControllerRoute();

app.UseStatusCodePagesWithReExecute("/error/show/{0}");
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error/show/500");
    //app.UseCustomErrorPages();
}

app.UseDefaultFiles();
app.UseStaticFiles();
//app.UseCalculator("/calc");


app.Run();


