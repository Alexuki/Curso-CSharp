namespace Routing
{
    public class OnlyAlbertsConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpcontext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var name = values[routeKey]?.ToString();
            return name != null && name.StartsWith("albert",
            StringComparison.CurrentCultureIgnoreCase);
        }

    }
}