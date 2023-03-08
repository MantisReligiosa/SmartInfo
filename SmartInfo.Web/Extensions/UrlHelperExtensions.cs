using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SmartInfo.Web.Common;

namespace SmartInfo.Web.Extensions;

public static class UrlHelperExtensions
{
    public static IHtmlContent ScriptVersioned(this IUrlHelper helper, string path,
        object htmlAttributes = null)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        path = VersionHelper.AddVersionToken(path);

        var tagBuilder = new TagBuilder("script");
        tagBuilder.MergeAttribute("src", helper.Content(path));
        tagBuilder.MergeAttribute("type", "text/javascript");

        if (htmlAttributes == null)
        {
            return tagBuilder;
        }

        var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
        tagBuilder.MergeAttributes(attributes);

        return tagBuilder;
    }
}