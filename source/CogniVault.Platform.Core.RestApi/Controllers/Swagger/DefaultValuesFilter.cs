using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;


namespace CogniVault.Platform.Core.RestApi.Controllers.Swagger;


public class DefaultValuesFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescriptionModel = context.ApiDescription;

        // Check if the operation has parameters, if not, then return early
        if (operation.Parameters == null)
            return;

        // Add a default problem response if it doesn't exist
        if (!operation.Responses.ContainsKey("default"))
        {
            operation.Responses.Add("default", new OpenApiResponse
            {
                Description = "Problem response",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = context.SchemaGenerator.GenerateSchema(typeof(ProblemDetails), context.SchemaRepository)
                    }
                }
            });
        }

        // Adjust each parameter based on the apiDescriptionModel
        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescriptionModel.ParameterDescriptions.First(p => p.Name == parameter.Name);

            // Set parameter description
            parameter.Description ??= description.ModelMetadata?.Description;

            // Set default value
            if (parameter.Schema != null && description.DefaultValue != null)
            {
                parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
            }

            // Set required flag
            parameter.Required |= description.IsRequired;
        }
    }
}
