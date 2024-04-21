using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

public class StartsWithConstraint : IRouteConstraint
{
    private readonly string _substr;
    public StartsWithConstraint(string substr)
    {
        _substr = substr;
    }

    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        return (values[routeKey]?.ToString() ?? string.Empty)
            .StartsWith(_substr, StringComparison.OrdinalIgnoreCase);
    }
}