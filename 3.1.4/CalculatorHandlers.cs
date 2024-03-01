namespace Routing
{
    public class CalculatorHandlers
    {
        // No usamos async porque no queremos hacer await, pero sí vamos a hacer que sea un Task lo que devuelve la función.
        // Al ser un Task, va a esperar un Task.FromResult de la suma de a+b.
        // Si queremos devolver un valor, en el Task se debe indicar el tipo; si no, simplemente ejecuta un proceso asíncrono.
        //public static Task<int> Add(int a, int b) => Task.FromResult(a + b);

        // 2) Utilizar valores por defecto para parámetros opcionales por si no puede extraelos de la propia petición
        public static Task<int> Add(int a, int b = 0) => Task.FromResult(a + b);
    }
}