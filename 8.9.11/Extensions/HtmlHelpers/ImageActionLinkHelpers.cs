/* 
//Helper original (9.10: Helpers personalizados)

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
...

public static class ImageActionLinkHelpers
{
    public static IHtmlContent ImageActionLink(
            this IHtmlHelper helper, string action, string controller,
            object parameters, string imagePath, string altText)
    {
        var urlHelper = GetUrlHelper(helper);
        string imgPath = urlHelper.Content(imagePath);

        var image = new TagBuilder("img");
        image.MergeAttribute("src", imgPath);
        image.MergeAttribute("alt", altText);
        image.TagRenderMode = TagRenderMode.SelfClosing;

        var link = new TagBuilder("a");
        link.MergeAttribute("href", urlHelper.Action(action, controller, parameters));
        link.InnerHtml.AppendHtml(image);
        return link;
    }

    // Unas sobrecargas para los escenarios más simples:
    public static IHtmlContent ImageActionLink(this IHtmlHelper helper,
            string action, string imagePath, string altText)
    {
        return ImageActionLink(helper, action, null, null, imagePath, altText);
    }
    public static IHtmlContent ImageActionLink(this IHtmlHelper helper,
            string action, string controller, string imagePath, string altText)
    {
        return ImageActionLink(helper, action, controller, null, imagePath, altText);
    }

    // Funciones privadas:
    private static IUrlHelper GetUrlHelper(IHtmlHelper helper)
    {
        var urlHelperFactory = helper.ViewContext.HttpContext.RequestServices.GetService<IUrlHelperFactory>();
        var urlHelper = urlHelperFactory.GetUrlHelper(new ActionContext()
        {
            ActionDescriptor = helper.ViewContext.ActionDescriptor,
            RouteData = helper.ViewContext.RouteData,
            HttpContext = helper.ViewContext.HttpContext
        });
        return urlHelper;
    }
} 

//Llamada desde una vista:
//@Html.ImageActionLink(
//           action: "edit",
//           controller: "user",
//           parameters: null, //* route params
//           imagePath: "/images/banner1.svg",
//           altText: "Editar usuario")

*/


using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Lab03.Extensions.HtmlHelpers;

public static class ImageActionLinkHelpers
{
    public static IHtmlContent ImageActionLink(
        this IHtmlHelper helper, string action, string controller,
        object parameters, string imagePath, string hoverImage, string altText)
    {
        var urlHelper = GetUrlHelper(helper);
        string imgPath = urlHelper.Content(imagePath);

        var image = new TagBuilder("img");
        image.MergeAttribute("src", imgPath);
        image.MergeAttribute("data-img", imgPath);
        image.MergeAttribute("data-hover", hoverImage);
        image.MergeAttribute("alt", altText);
        image.TagRenderMode = TagRenderMode.SelfClosing;

        var link = new TagBuilder("a");
        link.MergeAttribute("href", urlHelper.Action(action, controller, parameters));
        link.InnerHtml.AppendHtml(image);
        return link;
    }

    // Unas sobrecargas para los escenarios más simples:    
    public static IHtmlContent ImageActionLink(this IHtmlHelper helper,
        string action, string imagePath, string hoverImage, string altText)
    {
        return ImageActionLink(helper, action, null, null, imagePath, hoverImage, altText);
    }
    public static IHtmlContent ImageActionLink(this IHtmlHelper helper,
        string action, string controller, string imagePath, string hoverImage, string altText)
    {
        return ImageActionLink(helper, action, controller, null, imagePath, hoverImage, altText);
    }

    // Funciones privadas
    private static IUrlHelper GetUrlHelper(IHtmlHelper helper)
    {
        var urlHelperFactory = helper.ViewContext.HttpContext.RequestServices.GetService<IUrlHelperFactory>();
        var urlHelper = urlHelperFactory.GetUrlHelper(new ActionContext()
        {
            ActionDescriptor = helper.ViewContext.ActionDescriptor,
            RouteData = helper.ViewContext.RouteData,
            HttpContext = helper.ViewContext.HttpContext
        });
        return urlHelper;
    }
}