using Scalar.AspNetCore;

namespace ContactsManager.Api.Startup;

public static class OpenApiConfig
{
    public static void AddOpenApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
    }
}
