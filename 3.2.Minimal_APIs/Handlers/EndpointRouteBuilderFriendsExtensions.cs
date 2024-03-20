using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

public static class EndpointRouteBuilderFriendsExtensions
{
    public static void MapFriendsCreation(this IEndpointRouteBuilder endpoints, string routePattern)
    {
        endpoints.MapGet(routePattern, (HttpContext ctx, IAntiforgery antiforgery) =>
        {
            // Generar los tokens antiforgery
            var tokenSet = antiforgery.GetAndStoreTokens(ctx);
            // Generar el formulario (teniendo en cuenta la inserción del token como campo oculto)
            return Results.Content(GetFormHtml(tokenSet), "text/html");
        });
        endpoints.MapPost(routePattern, ([FromForm] Friend friend) => friend);
    }

    private static string GetFormHtml(AntiforgeryTokenSet tokens)
    {
        var html = $"""
                    <html>
                        <body>
                            <form action="/friends" method="POST">
                                <input type="hidden" name="{tokens.FormFieldName}" value="{tokens.RequestToken}" />
                                <input type="text" name="name" placeholder="Name" /><br>
                                <input type="text" name="age" placeholder="Age" /><br>
                                <input type="submit" />
                            </form>
                        </body>
                    </html>
                    """;
        return html;
    }
}
