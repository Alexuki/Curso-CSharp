namespace Routing
{
    public class CustomErrorResult : IResult
    {
        private readonly string _text;

        public CustomErrorResult(string text)
        {
            _text = text;
        }

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 999;
            await httpContext.Response.WriteAsync("Error terrible: " + _text);
        }
    }

    public static class CustomResultsExtensions
    {
        public static IResult CustomError(this IResultExtensions results, string text)
        {
            return new CustomErrorResult(text);
        }
    }
}