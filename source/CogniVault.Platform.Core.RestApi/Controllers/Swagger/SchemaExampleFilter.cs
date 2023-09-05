using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CogniVault.Platform.Core.RestApi.Controllers.Swagger;

public class SchemaExampleFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo != null)
        {
            var schemaAttribute = context.MemberInfo.GetCustomAttributes<SchemaExampleAttribute>().FirstOrDefault();

            if (schemaAttribute != null) 
                schema.Example = new OpenApiString(schemaAttribute.ExampleText);
        }
    }
}
