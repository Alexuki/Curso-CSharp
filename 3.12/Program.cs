//3.12 Health checks

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

/*
var builder = WebApplication.CreateBuilder(args);

// 1) Chequeo de espacio libre en discos y memoria consumida por la aplicación
// Requiere el paquete AspNetCore.HealthChecks.System
builder.Services.AddHealthChecks()
    .AddDiskStorageHealthCheck(opt =>
    {
        opt.AddDrive(driveName: "C:\\", minimumFreeMegabytes: 1000); // 1 GB
        opt.AddDrive(driveName: "D:\\", minimumFreeMegabytes: 20_000); // 20 GB
    }, name: "Disks")
    .AddPrivateMemoryHealthCheck(
        maximumMemoryBytes: 2_000_000_000, // 2 GB
        failureStatus: HealthStatus.Degraded,
        name: "Private memory"
    );

// 2) Chequeo de un SQL Server
// Requiere el paquete AspNetCore.HealthChecks.SqlServer
// NOTA: Añadir paquetes a csproj y realizar un dotnet restore en la raíz del proyecto
builder.Services.AddHealthChecks()
    .AddSqlServer(
        connectionString: "(connection string)",
        healthQuery: "SELECT 1;", // Verificar la disponibilidad y estado de la BD. Selecciona el valor 1 y lo devuelve como resultado
        name: "SQL Server",
        failureStatus: HealthStatus.Degraded
    );

// 3) Comprobar disponibilidad de APIs externas
// Añadir el paquete con dotnet add package AspNetCore.HealthChecks.Uris
builder.Services.AddHealthChecks()
    .AddUrlGroup(new Uri("http://httpbin.org/status/200"), name: "httpbin API");


// 4) Altenativa: Configurar los health checks anteriores de forma encadenada:
// builder.services
//     .AddHealthChecks()
//     .AddDiskStorageHealthCheck(...)
//     .AddSqlServer(...)
//     .AddRedis(...);

 */




// 5) Añadir interfaz de usuario para las comprobaciones de salud del sistema
// Añadir paquete AspNetCore.HealthChecks.UI
// Registrar los servicios correspondientes a la interfaz, utilizando el extensor AddHealthChecksUI()

// Requiere el NuGet adicional:
// AspNetCore.HealthChecks.UI.InMemory.Storage


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHealthChecksUI() // Registramos la UI
    .AddInMemoryStorage(); // Registramos el proveedor de almacenamiento de datos que usará la UI


builder.Services.AddHealthChecks()
    .AddDiskStorageHealthCheck(opt =>
    {
        opt.AddDrive(driveName: "C:\\", minimumFreeMegabytes: 1000); // 1 GB
        opt.AddDrive(driveName: "D:\\", minimumFreeMegabytes: 20_000); // 20 GB
    }, name: "Disks")
    .AddPrivateMemoryHealthCheck(
        maximumMemoryBytes: 2_000_000_000, // 2 GB
        failureStatus: HealthStatus.Degraded,
        name: "Private memory"
    )
    .AddSqlServer(
        connectionString: "(connection string)",
        healthQuery: "SELECT 1;", // Verificar la disponibilidad y estado de la BD. Selecciona el valor 1 y lo devuelve como resultado
        name: "SQL Server",
        failureStatus: HealthStatus.Degraded
    )
    .AddUrlGroup(new Uri("http://httpbin.org/status/200"), name: "httpbin API");


// 6) Configuración del pipeline:
// a) Modificar el registro del endpoint health check de ASP.NET Core para incluir datos adicionales en los resultados
// b) Añadir endpoint específico para procesar peticiones relativas a la UI
// Para esto, instalar el paquete AspNetCore.HealthChecks.UI.Client


var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions() //Configurar cómo se manejarán las comprobaciones de salud
{
    Predicate = _ => true, //Predicado que determina si se deben incluir o excluir ciertas comprobaciones de salud al informar sobre el estado de salud.
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse //Escritor de respuestas para las comprobaciones de salud
});

// La función lambda devuelve siempre true, lo que significa que todas las comprobaciones de salud se incluirán.
// UIResponseWriter.WriteHealthCheckUIResponse es un método proporcionado por AspNetCore.HealthChecks.UI que escribe la respuesta en un formato específico
// para ser utilizado con la interfaz de usuario de las comprobaciones de salud (/healthchecks-ui) proporcionada por AspNetCore.HealthChecks.UI.

app.MapHealthChecksUI();
// ... Otros endpoints

// c) Indicar a los componentes de UI desde dónde puede obtener los datos de salud del servicio,
// en el archivo de configuración appsettings.json
// Accedemos a la pantalla de estado desde localhost:xxxx/healthchecks-ui

app.Run();
