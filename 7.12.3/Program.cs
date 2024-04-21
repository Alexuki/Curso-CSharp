/*
7.12.3 DEMO: ActionResults
Se creará un IActionResult personalizado para generación de archivos PDF bajo demanda.
Mediante un formulario se solicitará al usuario el nombre de archivo y el texto que incluirá, y con ello se generará el PDF.
1) En Models crear la clase PdfFile con propiedades obligatorias.
2) Crear PdfController con una acción GET para solicitar el formulario, y una acción POST para generar el PDF.
3) Añadir Vista. NOTAS:
    - Recibe como modelo un objeto de la clase PdfFile.
    - Url.Action(): Si no se indica nada, por defecto llama a la misma URL desde la que se está llamando.
4) Comprobamos que funciona en /pdf/download
5) A continuación, haremos que cuando se envíe un formulario, se genere un PDF sobre la marcha y se envíe al usuario como un archivo descargable.
Para ello, crearemos nuestro propio IActionResult.
6) En el controlador, en la acción Post retornaremos un PdfFileResult al que pasamos el título y texto del fichero.
7) No existe una convención para la ubicación de las clases que implementen IActionResult. En nuestro caso, las pondremos en una carpeta Extensions/IActionResults.
Creamos la clase PdfFileResult:
    - En el contructor recibirá los parámetros que le vamos a pasar (en nuestro caso, nombre de fichero y texto) y los mantendremos dentro de la clase.
    - Implementar los métodos de la interfaz. En ExecuteResultAsync tenemos que implementar una Task con la lógica que genera el PDF.
8) Para crear el PDF usaremos un componente iTextSharp que es de pago pero del que existe un port no oficial para Net Core gratuito.
Se distribuye mediante un paquete NuGet. Para ello, vamos a la solución y usamos la opción del menú contextual Manage nuGet Packages. Buscamos ItextSharp.lglv2.core
y lo instalamos.

NOTA para instalar desde comandos o usando referencias de paquete:
    -.NET CLI: dotnet add package iTextSharp.LGPLv2.Core --version 3.4.18
    -Package Manager (Visual Studio): NuGet\Install-Package iTextSharp.LGPLv2.Core -Version 3.4.18
    -PackageReference: <PackageReference Include="iTextSharp.LGPLv2.Core" Version="3.4.18" />

9) Creamos el código para generar el PDF en ExecuteResultAsync. Usaremos un stream de tipo MemoryStream que pasaremos a nuestro método GeneratePdf junto con el texto.
El método GeneratePdf lo implementaremos a continuación.
El PDF generado se devuelve en la petición:
    - Indicar Content-Type "application/pdf" para que el navegador sepa cómo gestionarlo.
    - Añadir en Headers "content-disposition" con el valor "attachment; filename={_fileName}" para que el fichero se envíe como un recurso descargable que, por defecto,
    irá a la carpeta Downloads del usuario con el nombre por defecto que le hayamos indicado.
    - Finalizamos escribiendo el stream en la respuesta. Usamos await, por lo que al método debemos añadirle el modificador async.

10) Añadimos el método para generar PDF. Recibe un stream y un string con el contenido.
Utilizaremos lo que nos ofrece la librería iTextSharp para crear el documento.
11) Probamos a acceder a /pdf/download. Pulsamos el botón y vemos que se descarga el pdf con los datos introducidos.


7.12.4 PRÁCTICA 4: IActionResults personalizados
Añadir funcionalidades para generar un archivo Excel
1) Añadir en el proyecto referencia al paquete NuGet closedXML.
2) Añadir nuevo IActionresult ExcelResult. Genera el archivo .XLS recorriendo la colección de objetos que recibimos como parámetro añadiendo una celda con el valor de
cada una de sus propiedades. Para generar los encabezados, usaremos los nombres de las propiedades del primer objeto de la colección.
3) Crear ExcelController que utiliza ExcelResult para generar el archivo Excel de un conjunto de datos que podrían haber sido obtenidos desde una BBDD o similar.
4) Ejecutar la aplicación y comprobar que al acceder a la acción Generate() se crea el archivo correctamente.
*/

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
