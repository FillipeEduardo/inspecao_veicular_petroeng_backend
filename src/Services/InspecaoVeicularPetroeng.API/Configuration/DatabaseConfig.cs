using InspecaoVeicularPetroeng.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace InspecaoVeicularPetroeng.API.Configuration;

public static class DatabaseConfig
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        var postgresConnectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Database = Environment.GetEnvironmentVariable("SQL_DATABASE"),
            Username = Environment.GetEnvironmentVariable("SQL_USER"),
            Password = Environment.GetEnvironmentVariable("SQL_PASSWORD"),
            Host = Environment.GetEnvironmentVariable("SQL_SERVER"),
            Port = 5432,
            Pooling = true,
            MinPoolSize = environment.IsDevelopment() ? 0 : 5,
            MaxPoolSize = environment.IsDevelopment() ? 10 : 200,
            ConnectionLifetime = environment.IsDevelopment() ? 30 : 300,
            CommandTimeout = environment.IsDevelopment() ? 30 : 60,
            Timeout = environment.IsDevelopment() ? 30 : 60,
            SslMode = SslMode.Prefer,
            MaxAutoPrepare = environment.IsDevelopment() ? 0 : 20,
            AutoPrepareMinUsages = 2,
            KeepAlive = 30,
            TcpKeepAliveTime = 30,
            TcpKeepAliveInterval = 2
        };

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseNpgsql(postgresConnectionStringBuilder.ConnectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure(
                        environment.IsDevelopment() ? 1 : 3,
                        TimeSpan.FromSeconds(30),
                        null);
                    npgsqlOptions.CommandTimeout(60);
                });
            opt.EnableSensitiveDataLogging(environment.IsDevelopment());
            opt.EnableDetailedErrors(environment.IsDevelopment());

            if (!environment.IsDevelopment())
                opt.EnableServiceProviderCaching();
        });
        return services;
    }
}