using Microsoft.AspNetCore.Mvc.Filters;

//(20)
public class HandledByMvcAttribute : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add("X-Handled-By", "ASP.NET Core MVC");
        base.OnResultExecuting(context);
    }
}