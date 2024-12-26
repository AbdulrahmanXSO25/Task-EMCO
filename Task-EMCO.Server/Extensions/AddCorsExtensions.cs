namespace Task_EMCO.Server.Extensions;

public static class AddCorsExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("CorsConfig:AllowedOrigins").Get<string[]>();

        if (allowedOrigins == null || !allowedOrigins.Any())
        {
            throw new InvalidOperationException("Allowed origins is not ok in configuration -- Please check the appsettings.json file.");
        }

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins(allowedOrigins)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            });
        });

        return services;
    }
}