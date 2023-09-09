using CogniVault.Platform.Core.RestApi.Constants;
using CogniVault.Platform.Core.RestApi.Controllers.Swagger;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CogniVault.Platform.Core.RestApi.Extensions;

public static class VersioningExtensions
{
    public static IServiceCollection AddAppApiVersioning(this IServiceCollection services,
        Action<ApiVersioningOptions> options = null)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddApiVersioning(options ?? (o =>
        {
            o.DefaultApiVersion = new ApiVersion(Swagger.ApiVersions.MajorVersion1, Swagger.ApiVersions.MinorVersion0);
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.ReportApiVersions = true;
        }));

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services,
        IWebHostEnvironment hostingEnvironment,
        IConfiguration configuration,
        IList<OpenApiInfo> openApiInfos)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (hostingEnvironment == null) throw new ArgumentNullException(nameof(hostingEnvironment));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        if (hostingEnvironment.IsDevelopmentOrStaging())
            services.AddSwaggerGen(c =>
            {
                foreach (var openApiInfo in openApiInfos)
                {
                    var name = $"{Swagger.Versions.VersionPrefix}{openApiInfo.Version}";
                    c.SwaggerDoc(name, openApiInfo);
                }

                c.CustomSchemaIds(x => x.FullName);

                // Filters
                c.OperationFilter<RemoveVersionFromParameterFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                c.OperationFilter<DefaultValuesFilter>();
                c.SchemaFilter<SchemaExampleFilter>();

                c.DescribeAllParametersInCamelCase();
                // c.IncludeXmlComments(XmlCommentsFilePath);
            });

        return services;
    }
}

