using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;
using Amazon.S3;
using FluentValidation;
using InspecaoVeicularPetroeng.API.Pipelines;
using InspecaoVeicularPetroeng.API.Services;
using InspecaoVeicularPetroeng.Mediator.Extensions;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.AspNetCore.Localization;
using Serilog;
using Serilog.Events;

namespace InspecaoVeicularPetroeng.API.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        var assembly = Assembly.Load("InspecaoVeicularPetroeng.API");
        services.AddSecurity();
        services.AddSwaggerConfig();
        services.AddDatabaseConfiguration(environment);
        services.AddControllers()
            .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddHttpContextAccessor();
        services.AddHttpClient();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddMediator([assembly]);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddValidatorsFromAssembly(assembly);
        services.AddSingleton<IAmazonS3>(_ =>
            new AmazonS3Client("admin",
                Environment.GetEnvironmentVariable("MINIO_PASSWORD"),
                new AmazonS3Config
                {
                    ServiceURL = Environment.GetEnvironmentVariable("MINIO_URL"),
                    ForcePathStyle = true
                }));

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .WriteTo.Async(a => a.Console())
            .WriteTo.Async(a => a.File(
                "logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                buffered: true
            ))
            .CreateLogger();

        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("Content-Disposition");
            });
        });

        return services;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        var defaultCulture = new CultureInfo("pt-BR");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = new List<CultureInfo> { defaultCulture },
            SupportedUICultures = new List<CultureInfo> { defaultCulture }
        };

        app.UseRequestLocalization(localizationOptions);
        app.UseSwaggerConfig();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        return app;
    }
}