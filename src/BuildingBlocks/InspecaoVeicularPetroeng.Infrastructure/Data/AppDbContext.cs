using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(ToSnakeCase(entity.GetTableName()));

            foreach (var property in entity.GetProperties()) property.SetColumnName(ToSnakeCase(property.Name));

            foreach (var key in entity.GetKeys()) key.SetName(ToSnakeCase(key.GetName()));

            foreach (var index in entity.GetIndexes()) index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()));
        }

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("InspecaoVeicularPetroeng.Infrastructure"));
    }

    private static string? ToSnakeCase(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var result = string.Empty;
        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsUpper(c) && i > 0) result += "_";
            result += char.ToLower(c);
        }

        return result;
    }
}