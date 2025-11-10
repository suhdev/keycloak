# Keycloak .NET Core Port

This is a .NET Core port of Keycloak, focusing initially on porting the database schema using FluentMigrator.

## Project Structure

- **Keycloak.Database**: Class library containing FluentMigrator database migrations  
- **Keycloak.Database.Runner**: Console application for running migrations  
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
   - Renames EVENT_ENTITY.TIME to EVENT_TIME
   - Based on: jpa-changelog-1.1.0.Beta1.xml, jpa-changelog-1.1.0.Final.xml

3. **Migration_003_Version_26_5_0.cs** - Version 26.5.0 updates (example of recent changes)
   - Adds indexes for offline client session queries
   - Makes IDENTITY_PROVIDER columns nullable
   - Removes WORKFLOW_PROVIDER_ID column
   - Based on: jpa-changelog-26.5.0.xml

4. **Migration_004_Version_1_2_0.cs** - Version 1.2.0 updates
   - Adds PROTOCOL_MAPPER, FEDERATED_IDENTITY, IDENTITY_PROVIDER tables
   - Adds identity provider configuration and client mappings
   - Fixes REALM_APPLICATION table column order (KEYCLOAK-1106)
   - Removes obsolete social and claims tables
   - Based on: jpa-changelog-1.2.0.Beta1.xml, jpa-changelog-1.2.0.CR1.xml, jpa-changelog-1.2.0.Final.xml

### Current Status

**Completed Migrations:** 4 of 76 Liquibase files (covering v1.0.0 - v1.2.0, plus v26.5.0 as example)

**Remaining Migrations (72 files):** Pattern established, ready for systematic porting
- **v1.3.0 - v1.9.2**: Authentication flows, user federation mappers (9 files)
- **v2.x - v4.x**: Authorization services, client scopes (19 files + authz files)
- **v5.x - v14.0**: Migration consolidation, offline sessions (13 files)
- **v15.0 - v26.4.0**: Modern features (WebAuthn, organizations) (31 files)

### Key Differences from Liquibase

- **Consolidated Migrations**: Multiple related Liquibase migrations are combined into single FluentMigrator migrations
- **Fluent API**: Uses C# fluent API instead of XML
- **PostgreSQL Focus**: Optimized for PostgreSQL (though FluentMigrator supports multiple databases)
- **Version Tracking**: FluentMigrator uses its own VersionInfo table for tracking applied migrations

### Omitted/Special Handling

Some aspects of the original Liquibase migrations need special handling:

- **Custom Change Classes**: Liquibase custom changes (e.g., `AddRealmCodeSecret`, `JpaUpdate1_2_0_Beta1`) need custom implementation
- **Database-Specific Migrations**: DB2, MSSQL-specific migrations may be omitted or adapted for PostgreSQL
- **Partial Indexes**: PostgreSQL partial index WHERE clauses may need raw SQL execution

## Requirements

- .NET 9.0 SDK
- PostgreSQL database
- FluentMigrator 7.1.0+

## Running Migrations

Use the provided console application:

```bash
# List all migrations
cd Keycloak.Database.Runner
dotnet run list

# Run all pending migrations
dotnet run up

# Rollback last migration
dotnet run down

# Rollback to specific version
dotnet run down 2

# Set connection string via environment variable
export KEYCLOAK_DB_CONNECTION="Server=localhost;Database=keycloak;User Id=keycloak;Password=****;"
dotnet run up
```

Or programmatically:

```csharp
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString("Server=localhost;Database=keycloak;...")
        .ScanIn(typeof(Migration_001_InitialSchema).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);

using (var scope = serviceProvider.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}
```

## Migration Development Guide

### Adding New Migrations

To add a new migration:

1. Create a new class inheriting from `Migration`
2. Add the `[Migration(version, "description")]` attribute
3. Implement `Up()` and `Down()` methods
4. Use FluentMigrator's fluent API for schema changes

Example:

```csharp
using FluentMigrator;

namespace Keycloak.Database.Migrations;

[Migration(5, "Version X.Y.Z - Feature description")]
public class Migration_005_Version_X_Y_Z : Migration
{
    public override void Up()
    {
        Create.Table("NEW_TABLE")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey()
            .WithColumn("NAME").AsString(255).NotNullable();
            
        Create.Index("IDX_NEW_TABLE_NAME")
            .OnTable("NEW_TABLE")
            .OnColumn("NAME");
    }

    public override void Down()
    {
        Delete.Table("NEW_TABLE");
    }
}
```

### Converting from Liquibase

Common Liquibase → FluentMigrator conversions:

| Liquibase XML | FluentMigrator C# |
|---------------|-------------------|
| `<createTable>` | `Create.Table()` |
| `<column>` | `.WithColumn()` |
| `<constraints nullable="false">` | `.NotNullable()` |
| `<addPrimaryKey>` | `Create.PrimaryKey()` or `.PrimaryKey()` |
| `<addForeignKeyConstraint>` | `Create.ForeignKey()` |
| `<createIndex>` | `Create.Index()` |
| `<addColumn>` | `Alter.Table().AddColumn()` |
| `<dropColumn>` | `Delete.Column().FromTable()` |
| `<renameColumn>` | `Rename.Column().OnTable().To()` |
| `<update>` | `Update.Table().Set().Where()` |

## Contributing

This is an ongoing port focusing on the database layer. Contributors can help by:

1. **Selecting an unmigrated Liquibase file** from the list above
2. **Creating a corresponding FluentMigrator migration** following existing patterns
3. **Testing** the migration builds and can be listed
4. **Submitting a PR** with the new migration

Each migration should:
- Use sequential version numbers (5, 6, 7, ...)
- Include descriptive comments referencing source Liquibase files
- Implement both Up() and Down() methods
- Follow the naming convention: `Migration_XXX_Version_Y_Y_Y.cs`
- Build without errors

### Migration Priority

Suggested order for porting remaining migrations:

1. **v1.3.0 - v1.9.2** (Authentication core) - Critical for auth flows
2. **v2.x** (Authorization setup) - Needed for authz services
3. **v3.x - v4.x** (Client scopes) - Core client functionality
4. **v8.x - v14.x** (Sessions, OAuth) - Session management improvements
5. **v15.x - v26.4.0** (Modern features) - Latest features

## Current Status

✅ Created .NET solution structure  
✅ Added Keycloak.Database project  
✅ Added FluentMigrator packages (7.1.0)  
✅ Created initial migration (v1.0.0)  
✅ Created incremental migrations (v1.1.0, v1.2.0)  
✅ Created sample recent migration (v26.5.0)  
✅ Created migration runner with CLI (up/down/list commands)  
✅ Added comprehensive documentation  
🔄 Porting remaining migrations (4 of 76 complete - 5%)

### Packages Used

- FluentMigrator 7.1.0
- FluentMigrator.Runner 7.1.0
- FluentMigrator.Runner.Postgres 7.1.0
- Npgsql 9.0.4
- Microsoft.Extensions.DependencyInjection 9.0.x
- Microsoft.Extensions.Logging.Console 9.0.x

## License

This port follows the same Apache 2.0 license as the original Keycloak project.
