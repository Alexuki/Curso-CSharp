/*
7.6 DEMO: Controladores y acciones

1) Crear ControladorDePruebaController. Estamos usando la convención de nombre + sufijo Controller + heredar de Controller para que
la aplicación reconozca la clase como controladora.
2) Usar atributo Controller para conseguir lo anterior.
3) Usar atributo Route para modificar la ruta de acceso a la acción Test. Comprobamos que funciona con /test/john.
El Controlador creado de esta forma se llama también Controlador POCO porque usa una clase plana de CLR que no hereda de las clases proporcionadas por MVC.
4) Volvemos autilizar la nomenclatura por convenciones para no tener que utilizar el atributo Controller. Aún sin heredar de la clase Controller, la ruta es accesible.
5) Si en nuestro proyecto hay una clase cuyo nombre acabe en Controller, va a poder ser accedida desde el exterior, lo que puede ser peligroso. Ejemplo:
Creamos la clase VideoController en la carpeta Models. Tendrá un método Shutdown para apagar el equipo. Si en el navegador ponemos /video/shutdown, vemos que aparece "true",
que hemos puesto que devolviese en el método aparte del código de apagar el equipo.
En el método se devuelve un booleano. Esto es posible porque una acción puede devolver cualquier tipo de objeto aparte de la interfaz IActionResult.
6) Marcamos la clase con el atributo NonController para que el framework no la considere un controlador aunque siga su convención de nombres.
Así ya no es accesible desde el exterior, retornando un error 404, indicando que no encontró la acción.
7) El caso anterior también puede aplicarse a métodos dentro de un controlador, que no queremos que sean considerados acciones accesibles desde el exterior. Ejemplo:
Añadimos un método Compute que devuelve un número en nuestro controlador y probamos a acceder con /controladordeprueba/compute. Aquí no accedemos con /test porque
no hemos marcado el routing. 
8) Añadimos el decorador NonAction para evitar que el método pueda ser accedido desde el exterior. Le indicamos que no es una acción y no va a estar disponible.
9) Los atributos anteriores sacaron del enrutado una clase y un método. Retornan error 404 al llamarlos.
10) Asignar a la acción un nombre diferente al método que la implementa mediante el atributo ActionName. En el ejemplo,
poniendo de nombre "prueba", hace que la acción no responda ante controladordeprueba/test sino ante controladordeprueba/prueba.
Probamos también a pasarle un nombre en la query string: https://localhost:xxxx/controladordeprueba/prueba?name=josh
11) Uso de atributos para indicar el verbo de la petición. De esta forma, la acción solo se ejecuta cuando venga con dicho verbo (POST, GET...).
Si en el caso anterior añadimos HttpPost, la acción deja de estar dispnible porque el navegador está usando GET por defecto salvo que usemos un formulario.
En este caso, nos devuelve un error 405, de no poder conectar.
12) Para comprobar que una acción responde correctamente a las peticiones POST, podemos usar Fiddler, herramienta de depuración web que actúa como proxy local
que captura todas las peticiones que se realizan desde nuestra máquina. También permite componer peticiones HTTP en la pestaña Composer, indicando la URL y el verbo,
y podemos ver la respuesta a dicha petición.
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
