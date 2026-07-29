using System.Text;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace ServerManagement.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var assembly = typeof(Program).Assembly;
        services.AddMediatR(cfg =>
        {
            cfg.LicenseKey = configuration.GetSection("MEDIATR_LICENSE_KEY").Value;
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddOpenApi();
        services.AddCarter(configurator: c =>
        {
            c.WithDefaultValidatorLifetime(ServiceLifetime.Scoped);
        });
        services
            .AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("ServerManagement")!);

        services.AddAuthorization();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpContextAccessor();
        services.AddValidatorsFromAssembly(assembly);

        TypeAdapterConfig.GlobalSettings.Scan(assembly);
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapCarter();

        app.UseExceptionHandler(opts => { });

        app.UseHealthChecks(
            "/health",
            new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
        );

        app.UseHttpsRedirection();
        return app;
    }
}
