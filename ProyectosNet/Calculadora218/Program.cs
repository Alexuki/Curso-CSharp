var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICalculatorServices, CalculatorServices>();
builder.Services.AddTransient<ICalculationEngine, CalculationEngine>();


var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/error/show/{0}");
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error/show/500");
    app.UseCustomErrorPages();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCalculator("/calc");


app.Run();

/*
En appsettings se aplican los siguientes cambios para sistema de trazas:
/* {
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
} */

/*
Añadir nivel Trace para todos los proveedores
{
  "Logging": {
    "LogLevel": {
      "Default": "Trace"
    }
  },
  "AllowedHosts": "*"
}
*/

/*
Añadir nivel None para toda la aplicación y activarlas solo para el espacio de nombres raíz de la aplicación
{
  "Logging": {
    "LogLevel": {
      "Default": "None",
      "_2._18": "Debug",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
*/
