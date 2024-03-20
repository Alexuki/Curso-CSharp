public static class EndpointRouteBuilderCalculatorExtensions
{
    public static IEndpointConventionBuilder MapCalculator(this IEndpointRouteBuilder endpoints, string routePattern)
    {
        return endpoints.MapGet(routePattern, CalculatorHandler.Calculate);
    }
}
