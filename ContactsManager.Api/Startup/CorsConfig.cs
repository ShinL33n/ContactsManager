namespace ContactsManager.Api.Startup;

public static class CorsConfig
{
    public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("BlazorClient", policy =>
                policy.WithOrigins(configuration["Cors:ClientOrigin"]!)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
            );
        });
    }
}
