
using System.Text;
using System.Text.Json;
using Application;
using Application.Common.Extensions;
using AspNetCoreLineLogin;
using Domain.DTOs;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersistensePostgresql;
using Serilog;
using Serilog.Events;

try
{
    var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    enviroment = string.IsNullOrWhiteSpace(enviroment) ? "development" : enviroment;
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddJsonFile("appsettings.json").Build();
    builder.Configuration.AddJsonFile($"appsettings.{enviroment.ToLower()}.json", true, true).Build();

    builder.Services.Configure<AppSettingDTO>(builder.Configuration.GetSection(AppSettingDTO.AppSettings));
    builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(AppSettingDTO.EmailSettings));

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();


    Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(builder.Configuration)
          .CreateLogger();

    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .ReadFrom.Services(services);
        configuration.Enrich.FromLogContext();
    });

    Log.Information("Web Api started.");

    var AllowSpecificOrigins = "CleanOrigins";

    // Add services to the container.
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        //c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestSwagger", Version = "v1" });
        c.CustomSchemaIds(x => x.FullName);
    });
    builder.Services.AddInfrastructure();
    builder.Services.AddPersistensePostgresql(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddMemoryCache();
    builder.Services.AddConfig(builder.Configuration);
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: AllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins(builder.Configuration["AllowedHosts"]);
                              policy.AllowAnyHeader();
                          });
    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
            ValidAudience = builder.Configuration["Jwt:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{

    //}
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(AllowSpecificOrigins);

    app.UseSerilogRequestLogging();

    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
