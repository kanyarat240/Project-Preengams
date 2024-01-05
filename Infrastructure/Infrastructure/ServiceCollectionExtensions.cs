using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.Services;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserAccessor, CurrentUserAccessorService>();
        services.AddScoped<ILineAuthService, LineAuthService>();
        services.AddScoped<IJWTService, JWTService>();
        services.AddScoped<IHashingService, HashingService>();
        
        return services;
    }
}