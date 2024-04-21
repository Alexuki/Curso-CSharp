/*
PRÁCTICA 3: Templated helpers y helpers personalizados

1. Templated helpers
1.1. Edición de fechas con un date picker
1) Partir del proyecto de la práctica 9.7 (PRÁCTICA 2: Formularios de datos)
2) En la vista de edición /Views/User/Edit.cshtml se había indicado explícitamente el editor utilizado
para la edición mediante diferentes helpers.
3) Sustituir el editor de la fecha de nacimeinto por Html.EditorFor para que el framework
decida el editor a utilizar en dicha propiedad.
4) Existen muchos plugins JavaScript para un selector de fechas. Uno sencillo que se integra
bien con Bootstrap puede descargarse en: https://github.com/uxsolutions/bootstrap-datepicker
o usar el gestor de bibliotecas de Visual Studio LibMan (https://learn.microsoft.com/es-es/aspnet/core/client-side/libman).
5) Añadir los scripts a la vista, en la sección "Scripts".
En primer lugar añadimos referencias hacia el script y hoja de estilos de este componente.
A continuación, usamos JQuery para, cuando haya terminado de cargar la página, activar el picker
en todos los elementos <input> con clase CSS "picker".
6) Crear plantilla de edición para el tipo DateTime? (debe ser anulable), asegurándonos de que el
<input> utilice la clase CSS "picker".
7) Añadir plantilla para editar la clase string de forma que:
  - Los campos obligatorios se destaquen visualmente (usando un asterisco o cambiando el color).
  - Las propiedades decoradas con [StringLength] deben incluir su tamaño máximo en el atributo
  maxLength de la etiqueta <input>.
8) Añadir una plantilla para emails (debe llamarse "EmailAddress.cshtml") que incluya en el placeholder
un texto reconocible como "address@server.com".
9) Cambiar la vista de edición para que los editores anteriores sean utilizados en los campos de texto
(sustituir los TextBoxFor por EditorFor).
10) Ejecutar la aplicación y comprobar que se están aplicando los cambios.


1.2 Editor de tipos de usuario
11) Crear el archivo /Views/Shared/EditorTemplates/UserTypeEditor.cshtml. Será un editor especializado
en mostrar un desplegable de tipos de usuario.
La vista está tipada a int. Es lógico, puesto que es el tipo de datos que vamos a editar.
La lista de usuarios se está toamndo desde el ViewBag.
12) Convertir esta vista parcial en el editor del campo Type de UserViewModel:
  - Indicar en la propiedad la plantilla de edición mediante [UIHint].
  - Eliminar la llamada a Html.DropDownListFor() para que utilice la plantilla indicada.
  - En el controlador, cargar la lista de usuarios en el ViewBag o ViewData para poder leerla desde la plantilla.


1.3 Editores de tipos complejos
13) Editar la vista Edit.cshtml y eliminar todo el código interior del formulario, dejando solo el botón de envío.
14) Introducir antes del botón el código para generar automáticamante un editor para la entidad UserViewModel que
recibe la vista desde el controlador.
15) Ejecutar la aplicación y comprobar que se genera un editor para cada propiedad y que para la fecha de nacimiento
y tipo de usuario se usan los que hemos definido. El resultado es el mismo salvo algunos aspectos de estilo que se
pueden solucionar mediante CSS.


2. Creación de helpers personalizados
2.1 Implementación de Html.ImageActionLink()
16) Añadir el helper Html.ImageActionLinkLink() desarrollado en un proyecto anterior.
17) Añadir a _ViewImports.cshtml las directivas necesarias para que este helper esté disponible en todas nuestras vistas.
18) Introducir en la home del sitio web un enlace al editor de ususarios con una imagen.
19) Ejecutar la aplicación y comprobar que funciona.


2.2 Ampliación del helper ImageActionLink()
Ampliar la funcionalidad de los métodos helpers, de forma que admitan un parámetro opciona,
llamado hoverImage, que permita suministrar una imagen aletrnativa para cuando el ratón se desplace sobre la superficie
del enlace.
Existen varias formas de lograrlo. Una sencilla es: en la etiqueta <img> que generamos desde el helper incluiremos dos
atributos adicionales, a los que llamaremos data-img y data-hover. Introduciremos en ellos, respectivamente, la URL de
la imagen inicial y la URL de la imagen cuando paseemos sobre su superficie.
NOTA: Los atributos HTML del tipo "data-" son permitidos en HTML5 para incluir información personalizada en las etiquetas.

20) Añadir al Layout, antes del cierre de la etiqueta <body> el código jQuery:
<script type="text/javascript">
    $(function() {
    $("img[data-hover]")
        .mouseenter(function () {
            $(this).attr("src", $(this).attr("data-hover"));
        })
        .mouseleave(function () {
            $(this).attr("src", $(this).attr("data-img"));
        });
    });
</script>
Lo que hará este script es detectar en todas las páginas que usen el Layout si hay alguna imagen con el atributo data-hover y,
en ese caso, les asignará el código de manejador de los eventos mouseenter y mouseleave para sustituir la imagen mostrada
(atributo src de la imagen) por la indicada en los atributos data-hover y data-img, respectivamente. Obviamente, este script
podría residir en un archivo externo y ser referenciado desde el Layout.
Para que este mecanismo funcione, el atributo data-hover sólo debe ser generado por el helper cuando le sea suministrado un
valor para la imagen alternativa. Si no se envía este valor, el atributo no debe aparecer en la etiqueta <img>.
21) Actualizar la llamada al helper en la Home para indicar la imagen alternativa.
22) Ejecutar la aplicación y coprobar que funciona correctamente.
*/

using Lab03.Models.Services;

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
