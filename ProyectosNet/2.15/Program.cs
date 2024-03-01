






/*
15.1 DEMO: Utilización de User secrets
Vamos a guardar settings de aplicaciones como user secrets para que parámetros confidenciales no aparezcan en
el código de la aplicación.
Creamos un middleware que devuelve un ConnectionString que hay dentro de la configuración.
*/

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async ctx =>
{
    await ctx.Response.WriteAsync($"Connection string: {app.Configuration["ConnectionString"]}");
});

app.Run();

// 1) Al ejecutar así, devuelve vacío porque no hay un valor.
// 2) Añadimos la entrada ConnectionString al appSettings.json. Al ejecutar el programa, aparecerá este valor.
// 3) Si queremos dejar la cadena de conexión para futuras veces que nos haga falta para depurar, al hacer un
// commit estará disponible en el código fuente, suponiendo un problema de seguridad sobre todo si se sube a
// un repositorio público. Por eso usaremos user secrets.
// 4) Eliminamos la cadena de conexión anterior, vamos al proyecto y seleccionamos "Administrar secretos de usuario" en Visual Studio.
// Nos abre un json donde guardamos la clave ConnectionString junto con su valor.
// Al ejecutar la aplicación, coge el valor de este fichero que está fuera del código de la aplicación, en una carpeta local de nuestro equipo.
// De esta forma no hay riesgo de hacer un commit y push de los secretos.
// 5) El paso anterior hace que VS añada automáticamente en el fichero .csproj una entrada UserSecretsId con el Id para que
// cada vez que arranquemos el programa use los mismos user secrets.
// 6) Podemos encontrar el fichero de los user secrets yendo a %APPDATA$\Microsoft\UserSecrets y luego buscamos la carpeta con el id anterior.
// Por tanto, esto no expone los secretos al exterior, pero no impide que si alguien accede al equipo pueda verlos.
// 7) Por defecto, los user secrets solo se añen cuando estamos en Development. En Production no se añden por defecto.
// 8) También podemos gestionar los secretos usando la dotnetCLI. En la carpeta del proyecto usaremos: dotnet user-secrets
// Podemos setear una variable existente: dotnet user-secrets set ConnectionString "MyConnectionStringFromConsole"



