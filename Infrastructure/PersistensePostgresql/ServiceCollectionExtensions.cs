using System;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PersistensePostgresql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistensePostgresql(this IServiceCollection services,IConfiguration configuration)
    {
        string con = configuration.GetConnectionString("PostgreSQLConnection");
        services.AddDbContext<ICleanDbContext, CleanDbContext>(options =>
        {
            options.UseNpgsql(con, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            options.EnableSensitiveDataLogging();
            //options.UseSnakeCaseNamingConvention();
        }, ServiceLifetime.Scoped);
        return services;
    }
}