using Microsoft.Extensions.FileProviders;

namespace SmartInfo.Web.Pipelines;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSmartInfoStaticFiles(this IApplicationBuilder app, string contentRootPath)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(contentRootPath, "vue-app/dist")),
            RequestPath = "/dist"
        });
        
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(contentRootPath, "Content")),
            RequestPath = "/content"
        });

        return app;
    }
}