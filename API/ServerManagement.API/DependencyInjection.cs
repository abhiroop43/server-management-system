using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.StaticAssets;

namespace ServerManagement.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMediatR(cfg =>
        {
            cfg.LicenseKey = configuration.GetSection("MEDIATR_LICENSE_KEY").Value;
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        services.AddOpenApi();
        services.AddCarter();
        services
            .AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("ServerManagement")!);
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapCarter();

        app.UseExceptionHandler(options => { });

        app.UseHealthChecks(
            "/health",
            new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
        );

        app.UseHttpsRedirection();
        return app;
    }
}
