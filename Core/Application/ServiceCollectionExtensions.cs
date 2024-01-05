using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Application.Interfaces;
using Domain.DTOs;
using Domain.Configs;
using Microsoft.Extensions.Configuration;
using Application.Services;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<IUserAccountService, UserAccountService>();
        //services.AddScoped<IRunningNoService, RunningNoService>();
        services.AddScoped<IEmailService, SendEmail>();


        return services;

        //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
    }

    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LineConfig>(configuration.GetSection("Line"));
        services.Configure<JWTConfig>(configuration.GetSection("JWT"));
        
        return services;
    }
}
