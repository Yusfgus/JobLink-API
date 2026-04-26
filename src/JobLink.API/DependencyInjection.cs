using System.Text.Json.Serialization;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        // Add CORS services
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "http://localhost:5173") // The URL of your Angular app
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // Required if you use Cookies/Identity
                });
        });

        return services;
    }
}