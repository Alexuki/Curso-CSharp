public class CalculatorMiddleware
{
    private readonly string _basePath;
    private readonly RequestDelegate _next;
    private readonly ICalculatorServices _calculatorServices;
    private readonly ILogger<CalculatorMiddleware> _logger;

    public CalculatorMiddleware(string basePath,
                                RequestDelegate next,
                                ICalculatorServices calculatorServices,
                                ILogger<CalculatorMiddleware> logger)
    {
        this._basePath = basePath;
        this._next = next;
        this._calculatorServices = calculatorServices;
        this._logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments(_basePath))
        {
            if (context.Request.Path.StartsWithSegments($"{_basePath}/results"))
            {
                // Envía al navegador el resultado del cálculo
                await SendCalculationResults(context);
            }
            else if (context.Request.Path.Value == _basePath)
            {
                // Envía al navegador la página de entrada de datos
                await SendCalculatorHomePage(context);
            }
            else
            {
                // Envía al navegador un error 404 (not found)
                context.Response.Clear();
                context.Response.StatusCode = 404;
            }
        }
        else await _next(context);
    }

    private async Task SendHtmlPage(HttpContext context, string title, string body)
    {
        var content = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8' />
                    <title>{title} - Calculator</title>
                    <link href='/styles/calculator.css' rel='stylesheet' />
                </head>
                <body>
                    <h1>
                        <img src='/images/calculator.png' />
                        Simple calculator
                    </h1>
                    {body}
                </body>
                </html>
            ";
        context.Response.Clear();
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync(content);
    }


    private async Task SendCalculatorHomePage(HttpContext context)
    {
        await SendHtmlPage(
            context,
            "Start",
            $@" <form method='post' action='{_basePath}/results'>
                <input type='number' name='a'>
                <select name='operation'>
                    <option value='+'>+</option>
                    <option value='-'>-</option>
                    <option value='*'>*</option>
                    <option value='/'>/</option>
                </select>
                <input type='number' name='b'>
                <input type='submit' value='Calculate'>
            </form>
        ");
    }

    private async Task SendCalculationResults(HttpContext context)
    {
        int a = int.Parse(context.Request.Form["a"]);
        int b = int.Parse(context.Request.Form["b"]);
        string operation = context.Request.Form["operation"];

        _logger.LogDebug($"Trying to calculate: {a}{operation}{b}");

        var result = _calculatorServices.Calculate(a, b, operation);
        await SendHtmlPage(
            context,
            "Results",
            $@"<h2>{a}{operation}{b}={result}</h2>
                <p><a href='{_basePath}'>Back</a></p>"
        );
    }







}