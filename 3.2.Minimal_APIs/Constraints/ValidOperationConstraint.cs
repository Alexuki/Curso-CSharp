
public class ValidOperationConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        var operation = values[routeKey]?.ToString().ToLower();
        
        // Retornar true si la operación es válida
        // routeKey es la parte de la ruta que está siendo analizada. En el ejemplo en que se usa la restircción, es "operation"
        // values es un diccionario en que las claves son partes de la ruta, y se asocian con los valores pasados en ellas.
        if(operation == "div" && int.TryParse(values["b"]?.ToString(), out var b) && b == 0)
        {
            return false;
        }
            
        return operation == "add" || operation == "sub" || operation == "mul" || operation == "div";
    }
}