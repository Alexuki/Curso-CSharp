/*
9.7 PRÁCTICA 2: Formularios de edición de datos
1. Implementación de un formulario de datos
1) Crear nueva aplicación MVC.
2) Añadir clase view model UserViewModel con las siguientes propiedades y restricciones:
- Name, obligatorio, máximo 50 caracteres.
- Nickname, obligatorio, mínimo 3 y máximo 20 caracteres.
- Type, de tipo entero, que debe valer 1 (si el usuario es estándar), 2 (si es un usuario avanzado) o 3 (si es un administrador).
- EMail, obligatorio, que sea una dirección de correo válida de, como máximo, 100 caracteres.
- Password, obligatorio, con 20 caracteres como máximo y 5 como mínimo.
- RePassword, una repetición del password cuyo contenido debe coincidir con el de la propiedad Password.
- Birthdate, opcional, de tipo fecha.
- Enabled, booleano, que permitirá identificar cuándo un usuario está activo.
3) Crear UserController con acción "Edit" que retorne la vista por defecto.
4) Añadir la vista a la acción, tipándola a UserViewModel y crear formulario para introducir datos para objetos de este tipo.
Implementar todos los controles de edición y etiquetas de campos usando helpers tipados (LabelFor(), TextBoxFor(), etc.).
5) Para la propiedad Type, crear desplegable con las 3 clases de usuario permitidos por el sistema.
Los elementos del desplegable deberían obtenerse desde el Modelo, aunque, para simplificar, podemos usar la clase UserType con
un método GetUserTypes() que los devuelva.
Para enviar la lista a la vista para generar el desplegable, podemos usar el diccionario ViewData o añadir un campo más a UserViewModel.
6) Añadir al controlador la acción que recibirá los datos del formulario. El parámetro es de tipo UserViewModel y la lógica será:
- Si se detectan errores de validación, volver a mostrar el formulario indicando visualmente los errores.
- Si los datos son correctos, almacenar los datos en el Modelo, aunque, para simplificar, solo retornaremos Content("Ok") desde la acción.
7) Ejecutar la aplicación y comprobar que las validaciones funcionan tanto en cliente como en servidor.
NOTA: Para que las validaciones funcionen en cliente, debemos añadir a la vista, en su sección "Scripts", referencias hacia las
bibliotecas JavaScript que implementan esta funcionalidad. Antes de añadirlas, debemos comprobar que no se haya hecho previamente
desde el Layout, para evitar duplicidades.

2. Uso del atributo Remote
8) Añadir al Modelo la clase UserServices. Es un repositorio simple de usuarios en que incluimos un método para simular la comprobación
de existencia de un nickname en la base de datos.
9) Creamos la interfaz IUserServices y registramos su asociación con la clase en el sistema de inyección de dependencias.
10) Usar el atributo Remote sobre Nickname para comprobar en tiempo real la disponibilidad del nombre de usuario suministrado.
Crear también la acción que recibirá las solicitudes enviadas desde el cliente y que, a su vez, obtendrá los datos desde los
servicios del Modelo.
11) Ejecutar la aplicación y comprobar que funciona correctamente. Si intentamos registrar un usuario con nickname "john" o "peter"
debería avisar de que ya están ocupados.
12) Probar a desactivar JavaScript en el navegador (o comentar las líneas de la sección "Scripts" de la vista) para que no se realicen
validaciones en cliente, de forma que ya no se realiza la validación del nombre de usuario. Solucionarlo forzando que dicha comprobación
se realice desde el controlador, al recibir los datos.

3. Enlace con ActionLink()
13) Incluir un enlace en la barra de menús de la aplicación de forma que sea posible acceder directamente al formulario de edición,
usando el helper ActionLink().
14) Hacer que la acción Edit() de UserController esté accesible a través de la URL /profile.
15) Ejecutar la aplicación y comprobar cómo el enlace hacia el formulario de edición se ha modificado en función del contenido de la
configuración del sistema de routing y que todo sigue funcionando correctamente.
*/

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
