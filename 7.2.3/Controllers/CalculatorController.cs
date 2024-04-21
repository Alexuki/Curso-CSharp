using Microsoft.AspNetCore.Mvc;

namespace _7._2._3.Controllers
{

    /* 
    [Route("calculadora")] // Nombre de ruta fijo
    [Route("[controller]")] // (*) Uso de token que aplica el nombre del controlador (ejemplo: cal). Ahora se accede con /calc/sum/1/2
    [Route("[controller]/[action]")] // Usando token para el controlador y la acción
    //public class CalculatorController : Controller
    public class CalController : Controller // (*) Cambio de nombre del controlador
    {
        //[Route("calculadora/sum/{a}/{b}")] // Ruta original
        //[Route("calculadora/sum/{a}/{b?}")] // Casos 1 y 2: Parámetro opcional
        //[Route("calculadora/sum/{a}/{b=10}")] // Caso 3: Parámetro opcional y con valor por defecto

        //public IActionResult Sum(int a, int b) // Caso 1: Sin valor por defecto
        //public IActionResult Sum(int a, int b = 10) // Caso 2: Con valor por defecto



        //[Route("calculadora/sum/{a}/{b?}")] // Sin usar Route a nivel de clase
        //[Route("sum/{a}/{b?}")] // Especificando Route a nivel de clase
        //[Route("[action]/{a}/{b?}")] // Usando token para las acciones
        [Route("{a}/{b?}")] // Especificando la acción en el Route a nivel de clase
        public IActionResult Sum(int a, int b) // Caso 3: Sin valor por defecto. Lo toma de la ruta
        {
            return Content($"{a}+{b}={a + b}");
        }


        //[Route("[action]/{a}/{b?}")]
        [Route("{a}/{b?}")]
        public IActionResult Product(int a, int b) // Caso 3: Sin valor por defecto. Lo toma de la ruta
        {
            return Content($"{a}*{b}={a * b}");
        }
    } */



    // Punto 10: Ascender los parámetros de las acciones al Route del nivel de clase y aplica inline constraints para los atributos

    [Route("[controller]/[action]/{a:int}/{b:int?}")]
    public class CalController : Controller
    {

        public IActionResult Sum(int a, int b)
        {
            return Content($"{a}+{b}={a + b}");
        }

        public IActionResult Product(int a, int b)
        {
            return Content($"{a}*{b}={a * b}");
        }
    }


}
