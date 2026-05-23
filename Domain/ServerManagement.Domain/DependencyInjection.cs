using ServerManagement.Domain.Exceptions.Handler;

namespace ServerManagement.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        return services;
    }
}
