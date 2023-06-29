using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
public class TitleFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument doc, DocumentFilterContext context)
    {
        doc.Info.Title = "Azure IoT Demo";
    }
}