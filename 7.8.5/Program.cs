/*
7.8.5 DEMO: Binding avanzado

1) Crear clase Friend en Models.
2) Crear FriendsController con una acción Create que retorna una vista donde podemos introducir datos de nuevos amigos.
3) En Views/Friends crear la vista Create (objeto razor view). En ella ponemos el formulario.
4) Navegamos a /friends/create para comprobar que se muestra el formulario creado.
5) Crear método de acción en FriendsController para recibir los datos del formulario. Para que no exista ambigüedad en las rutas,
especificamos que esta acción se accede mediante el método POST. Pondremos que devuelva el contenido para verlo.
6) Probamos a introducir los datos en el formulario: Josh, 25, josh@josh.com. Al enviar los datos, vemos que la respuesta es la esperada.
7) Veremos el binding con propiedades de tipo complejo, añadiendo una propiedad de este tipo a la clase Friend, al formulario de la vista y a la
respuesta del controlador.
8) Comprobamos el funcionamiento del formulario actualizado, añadiendo Castellana y Madrid, y vemos que el binding es capaz de añadir la
propiedad compleja.
9) Vamos a comprobar qué sucede si el binder no puede detectar propiedades para poblar el objeto Friend que esperamos recibir en la acción.
Para ello, en la vista del formualrio eliminamos todas las propiedades, y ponemos un breakpoint en el método del controlador. Podemos comprobar
que al pulsar el botón de Submit, la acción llega al controlador, y que el objeto friend tiene todas sus propiedades NULL, lo que provocará una
excepción cuando intente acceder a cualquiera de ellas (friend.Name, friend.Age...). Se mostrará en el navegador, al tener activado el middleware
DeveloperExceptionPage, lo veremos como  una traza de dónde se ha producido la excepción.
10) Si en el código queremos detectar cuándo nos ocurre este problema, podemos exigir el bindeado del parámetro con el atributo [BindRequired] y
comprobar la propiedad ModelState.IsValid.
*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
