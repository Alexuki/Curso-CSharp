public static class CalculatorExtensions
{
    public static IApplicationBuilder UseCalculator(this IApplicationBuilder app, string path)
    {
        return app.UseMiddleware<CalculatorMiddleware>(path);
    }
}