using Microsoft.AspNetCore.Mvc;

// 1) Nombre del Controlador por convención
/* public class ControladorDePruebaController : Controller
{

} */

// 2) Uso de atributo [controller]
/*
Al no heredar de Controller, no disponemos de los métodos para el retorno sencillo de resultados como Content y View.
Tendremos que crear manualmente el ContentResult a devolver
*/
/* [Controller]
public class ControladorDePrueba
{
    [Route("/test/{name}")]
    public IActionResult Test(string name)
    {
        return new ContentResult() { Content = $"Hello {name}!" };
    }

} */




// 3) Nomenclatura por convenciones
public class ControladorDePruebaController
{
    /* [Route("/test/{name}")]
    public IActionResult Test(string name)
    {
        return new ContentResult() { Content = $"Hello {name}!" };
    } */

    [ActionName("prueba")] // 10) Eliminamos el enrutado y cambiamos el nombre de la acción.
    [HttpPost] // 11) Indicar el verbo que ejecuta la acción
    public IActionResult Test(string name)
    {
        return new ContentResult() { Content = $"Hello {name}!" };
    }

    [NonAction]
    public int Compute()
    {
        return 42;
    }

}

