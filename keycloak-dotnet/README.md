# Keycloak .NET Core Port

This is a .NET Core port of Keycloak, focusing initially on porting the database schema using FluentMigrator.

## Project Structure

- **Keycloak.Database**: Class library containing FluentMigrator database migrations
- **Migrations/**: Database migration files that port Liquibase migrations to FluentMigrator

## Database Migrations

The database migrations have been ported from the original Keycloak Liquibase migrations located in:
`model/jpa/src/main/resources/META-INF/jpa-changelog-*.xml`

### Migration Strategy

The 76 Liquibase migration files (containing over 7,000 lines) are being consolidated into logical FluentMigrator migrations:

1. **Migration_001_InitialSchema.cs** - Initial database schema (v1.0.0.Final)
   - Creates all core tables (CLIENT, REALM, USER_ENTITY, KEYCLOAK_ROLE, etc.)
   - Sets up primary keys, unique constraints, and foreign keys
   - Based on: jpa-changelog-1.0.0.Final.xml

2. **Migration_002_Version_1_1_0.cs** - Version 1.1.0 updates
   - Adds CLIENT_ATTRIBUTES, CLIENT_SESSION_NOTE, APP_NODE_REGISTRATIONS tables
   - Adds new columns to CLIENT, CLIENT_SESSION, and REALM tables
   - Based on: jpa-changelog-1.1.0.Beta1.xml, jpa-changelog-1.1.0.Final.xml

3. **Migration_003_Version_26_5_0.cs** - Version 26.5.0 updates (example of recent changes)
   - Adds indexes for offline client session queries
   - Makes IDENTITY_PROVIDER columns nullable
   - Removes WORKFLOW_PROVIDER_ID column
   - Based on: jpa-changelog-26.5.0.xml

### Key Differences from Liquibase

- **Consolidated Migrations**: Multiple related Liquibase migrations are combined into single FluentMigrator migrations
- **Fluent API**: Uses C# fluent API instead of XML
- **PostgreSQL Focus**: Optimized for PostgreSQL (though FluentMigrator supports multiple databases)
- **Version Tracking**: FluentMigrator uses its own VersionInfo table for tracking applied migrations

### Omitted Migrations

Some aspects of the original Liquibase migrations may need special handling:

- **Custom Change Classes**: Liquibase custom changes (e.g., `AddRealmCodeSecret`) need custom implementation
- **Database-Specific Migrations**: DB2, MSSQL-specific migrations may be omitted or adapted
- **Partial Indexes**: PostgreSQL partial index WHERE clauses may need raw SQL execution

## Requirements

- .NET 9.0 SDK
- PostgreSQL database
- FluentMigrator 7.1.0+

## Running Migrations

To run migrations, you'll need to create a migration runner. Example:

```csharp
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString("Server=localhost;Database=keycloak;User Id=keycloak;Password=password;")
        .ScanIn(typeof(Migration_001_InitialSchema).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);

using (var scope = serviceProvider.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}
```

## Current Status

✅ Created .NET solution structure
✅ Added Keycloak.Database project
✅ Added FluentMigrator packages
✅ Created initial migration (v1.0.0)
✅ Created sample incremental migrations (v1.1.0, v26.5.0)

### Next Steps

- [ ] Port remaining migrations (v1.2.0 through v26.4.0)
- [ ] Create migration runner console application
- [ ] Add integration tests for migrations
- [ ] Implement custom migration logic where needed
- [ ] Port authorization-specific migrations (authz-master.xml)
- [ ] Add support for migration verification

## Contributing

This is an initial port focusing on the database layer. The migration strategy prioritizes:

1. Accuracy: Maintaining schema compatibility with original Keycloak
2. Clarity: Using clear, maintainable C# code
3. Consolidation: Combining related migrations for easier management

## License

This port follows the same Apache 2.0 license as the original Keycloak project.
