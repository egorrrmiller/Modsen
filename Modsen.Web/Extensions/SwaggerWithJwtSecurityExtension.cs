using Microsoft.OpenApi.Models;

namespace Modsen.Web.Extensions;

public static class SwaggerWithJwtSecurityExtension
{
    public static IServiceCollection AddSwaggerWithJwtSecurity(this IServiceCollection collection)
    {
        const string description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"";

        return collection.AddSwaggerGen(swagger =>
        {
            swagger.AddSecurityDefinition("Bearer", new()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = description
            });

            swagger.AddSecurityRequirement(new()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}