/*
9.6 DEMO: Negociación de contenidos.
Veremos la negociación de contenidos en APIs creadas con ASP.Net Core MVC.
1) Creamos una aplicación de prueba.
2) La ejecutamos y probamos a hacerle peticiones con Postman.
GET https://localhost:xxxx/api/blog/posts => Obtiene todos los posts y se ven en la pantalla de Body.
GET https://localhost:xxxx/api/blog/posts/999 => El id no existe. Devuelve un 404.
GET https://localhost:xxxx/api/blog/posts/2 => El id existe. Devuelve un 200 y el post seleccionado.

Los resultados se retornan en json porque es el formateador por defecto de MVC.
3) En el código de inicialización de la aplicación podemos cambiar la colección de formateadores
del framework. Se hace al registrar los controladores mediante una lambda en que se añaden los
formateadores. Añadimos el formateador de XML.
4) Si ahora lanzamos las peticiones anteriores poneinedo en el encabezado Accept: application/xml,
las peticiones Get nos devolverán la respuesta formateada en XML. Para ello, vamos a la pestaña "Headers",
marcamos "Accept" y escribimos application/xml en la celda contigua.
El header Accept nos permite alterar el formato del resultado que nos retorna el servidor, siempre y cuando
éste lo soporte.
5) Probamos a crear un Post:
POST https://localhost:xxxx/api/blog/posts
En la pestaña "Body" marcamos el tipo como "Text" en lugar de JSON que viene marcado. En este caso,
el payload va a ser un JSON sin especificar que es un JSON:
{
    "id": 999,
    "author": "Bruce Banner",
    "title": "Big green things",
    "body": "Post about green things"
}
Al lanzar la petición, nos va a dar como respuesta un Error 415 Unsupported Media Type porque no
hemos explicitado el formato en que estamos haciendo la petición. para ello, vamos a la pestaña "Headers",
marcamos "Content-Type" y le ponemos application/json, ue es el formato con que estamos enviando el Post.
Ahora, la respuesta será un 201 Created ya que el servidor fue capaz de entender el formato en que se
lo estamos enviando.
Además, si vamos a la pestaña "Headers" de la respuesta, tenemos uno "Location" que da la ruta que
deberíamos utilizar si queremos hacer un GET contra el Post que acabamos de crear en la propia
petición. En este caso, la ruta mostrada es: https://localhost:xxxx/api/blog/Posts/999
6) Volvemos a lanzar la petición para comprobar que si el objeto a crear ya existe, se genera un Conflicto.
Nos devuelve un 409 Conflict.
7) Volvemos a lanzar la petición con un campo en blanco. Por ejemplo, eliminamos la clave "author" con su valor.
En este caso, obtenemos un 400 Bad Request.
8) Podemos modificar el formato en que enviamos los datos explicitando la cabecera "Content-Type" en las peticiones.
Por ejemplo, podemos hacer un POST y marcamos en "Headers" "Content-Type", poniéndole como valor application/xml.
En el "Body" de la petición enviamos el objeto XML, y marcamos en tipo "XML":
<Post>
<Id>2938</Id>
<Title>Hello</Title>
<Author>John</Author>
<Body>...</Body>
</Post>
Obtendremos un 415 Unsupported Media Type porque al igual que en el caso de las respuestas, el servidor, para que
pueda entender y parsear el objeto que le llega, necesita el formatter específico.
9) Añadimos un formateador de entrada para XML.
10) Comprobamos que ahora la aplicación recibe peticiones XML correctamente. PAra el caso anterior,
devuelve una respuesta 201 Created y el Body de la respuesta muestra:
{
    "id": 2938,
    "author": "Hello",
    "title": "John",
    "body": "..."
}

*/

using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPostsRepository, PostsRepository>();
//builder.Services.AddControllers(); // No necesitamos nada relativo a vistas por eso no usamos AddControllersWithViews
//(3)
/* builder.Services.AddControllers(opt =>
{
    opt.InputFormatters.Add(new XmlSerializerInputFormatter(opt)); //(9) Necesita que se le pase el propio objeto de options al InputFormatter
    opt.OutputFormatters.Add(new XmlSerializerOutputFormatter()); //(3)
}); */

// Para añadir el Input y Output Formatter para XML se puede simplificar con una sola llamada:
builder.Services.AddControllers().AddXmlSerializerFormatters();

var app = builder.Build();

app.MapControllers(); // No nos interesa la convención por defecto de enrutados. las rutas las definiremos en los controladores y sus acciones

app.Run();


