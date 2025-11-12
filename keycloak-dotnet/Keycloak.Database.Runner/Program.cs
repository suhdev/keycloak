using FluentMigrator.Runner;
using Keycloak.Database.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Keycloak.Database.Runner;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Keycloak Database Migration Runner");
        Console.WriteLine("===================================\n");

        // Get connection string from environment or use default
        var connectionString = Environment.GetEnvironmentVariable("KEYCLOAK_DB_CONNECTION") 
            ?? "Server=localhost;Port=5432;Database=keycloak;User Id=keycloak;Password=keycloak;";

        Console.WriteLine($"Connection: {MaskConnectionString(connectionString)}");
        Console.WriteLine();

        var serviceProvider = CreateServices(connectionString);

        using (var scope = serviceProvider.CreateScope())
        {
            try
            {
                // Parse command line arguments
                var command = args.Length > 0 ? args[0].ToLower() : "up";

                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                switch (command)
                {
                    case "up":
                        Console.WriteLine("Running migrations UP...");
                        runner.MigrateUp();
                        Console.WriteLine("✓ Migrations completed successfully!");
                        break;

                    case "down":
                        if (args.Length > 1 && long.TryParse(args[1], out var version))
                        {
                            Console.WriteLine($"Rolling back to version {version}...");
                            runner.MigrateDown(version);
                        }
                        else
                        {
                            Console.WriteLine("Rolling back last migration...");
                            runner.Rollback(1);
                        }
                        Console.WriteLine("✓ Rollback completed successfully!");
                        break;

                    case "list":
                        Console.WriteLine("Available migrations:");
                        // List migrations from the assembly
                        var migrationLoader = scope.ServiceProvider.GetRequiredService<IMigrationInformationLoader>();
                        var migrationInfos = migrationLoader.LoadMigrations();
                        
                        foreach (var migrationInfo in migrationInfos.OrderBy(m => m.Key))
                        {
                            var migrationVersion = migrationInfo.Key;
                            var description = migrationInfo.Value.Description ?? migrationInfo.Value.Migration.GetType().Name;
                            Console.WriteLine($"  Version {migrationVersion}: {description}");
                        }
                        
                        Console.WriteLine("\nNote: Use 'up' command to apply migrations, or check database VersionInfo table for applied migrations.");
                        break;

                    case "help":
                    default:
                        ShowHelp();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
                Console.WriteLine($"\nStack trace:\n{ex.StackTrace}");
                Environment.Exit(1);
            }
        }
    }

    private static IServiceProvider CreateServices(string connectionString)
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Migration_001_CompleteSchema).Assembly).For.Migrations())
            .AddLogging(lb => lb
                .AddConsole()
                .SetMinimumLevel(LogLevel.Information))
            .BuildServiceProvider(false);
    }

    private static string MaskConnectionString(string connectionString)
    {
        // Simple masking of password in connection string
        if (connectionString.Contains("Password=", StringComparison.OrdinalIgnoreCase))
        {
            var parts = connectionString.Split(';');
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Trim().StartsWith("Password=", StringComparison.OrdinalIgnoreCase))
                {
                    parts[i] = "Password=****";
                }
            }
            return string.Join(";", parts);
        }
        return connectionString;
    }

    private static void ShowHelp()
    {
        Console.WriteLine("Usage: Keycloak.Database.Runner [command] [options]");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("  up              Run all pending migrations (default)");
        Console.WriteLine("  down [version]  Rollback to specific version or rollback last migration");
        Console.WriteLine("  list            List all migrations and their status");
        Console.WriteLine("  help            Show this help message");
        Console.WriteLine();
        Console.WriteLine("Environment Variables:");
        Console.WriteLine("  KEYCLOAK_DB_CONNECTION  PostgreSQL connection string");
        Console.WriteLine("                          Default: Server=localhost;Port=5432;Database=keycloak;");
        Console.WriteLine("                                   User Id=keycloak;Password=keycloak;");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  # Run all migrations");
        Console.WriteLine("  dotnet run");
        Console.WriteLine();
        Console.WriteLine("  # List migration status");
        Console.WriteLine("  dotnet run list");
        Console.WriteLine();
        Console.WriteLine("  # Rollback last migration");
        Console.WriteLine("  dotnet run down");
        Console.WriteLine();
        Console.WriteLine("  # Rollback to version 1");
        Console.WriteLine("  dotnet run down 1");
    }
}
