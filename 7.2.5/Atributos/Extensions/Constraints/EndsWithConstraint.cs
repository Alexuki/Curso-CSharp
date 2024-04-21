using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

public class EndsWithConstraint : IRouteConstraint
{
    private readonly string _substr;
    public EndsWithConstraint(string substr)
    {
        _substr = substr;
    }

    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        return (values[routeKey]?.ToString() ?? string.Empty)
            .EndsWith(_substr, StringComparison.OrdinalIgnoreCase);
    }
}