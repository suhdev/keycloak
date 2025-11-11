# Keycloak .NET Core Port

This is a .NET Core port of Keycloak, focusing on porting the complete database schema using FluentMigrator.

## Project Structure

- **Keycloak.Database**: Class library containing FluentMigrator database migrations  
  - `database-schema.sql`: Complete Keycloak schema with UUID ID columns (embedded resource)
  - `Migrations/`: FluentMigrator migration that executes the SQL script
- **Keycloak.Database.Runner**: Console application for running migrations

## Database Migrations

The database migration uses the complete database schema file (`database-schema.sql`) as an embedded resource. The SQL file has been modified to use UUID type for all ID columns instead of VARCHAR(36).

### Migration Approach

1. **Embedded SQL Script**: The complete `database-schema.sql` is embedded in the Keycloak.Database assembly
2. **Single Migration**: `Migration_001_CompleteSchema.cs` reads and executes the embedded SQL script
3. **UUID Conversion**: All `id` and `*_id` columns have been converted from VARCHAR(36) to UUID type
4. **Foreign Key Compatibility**: All foreign key references have been updated to match the UUID type

### Key Features

- **UUID Type for IDs**: All `id` and `*_id` columns use PostgreSQL UUID type instead of VARCHAR(36)
- **Embedded Resource**: SQL schema is embedded in the assembly for easy deployment
- **Complete Schema**: Single migration creates the entire Keycloak database structure (87 tables)
- **PostgreSQL Optimized**: Uses PostgreSQL-specific types and features

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
        .ScanIn(typeof(Migration_001_CompleteSchema).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);

using (var scope = serviceProvider.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}
```

## Schema Details

### Tables Created (87 total)

The migration creates all Keycloak tables including:

**Core Tables:**
- `client` - OAuth/OIDC clients
- `realm` - Keycloak realms (tenants)
- `user_entity` - User accounts
- `keycloak_role` - Roles
- `credential` - User credentials

**Authentication & Authorization:**
- `authentication_flow`, `authentication_execution`, `authenticator`
- `resource_server`, `resource_server_resource`, `resource_server_policy`
- `identity_provider`, `federated_identity`

**Sessions & Events:**
- `user_session`, `client_session`, `offline_user_session`, `offline_client_session`
- `event_entity`, `admin_event_entity`

**Configuration:**
- `realm_attribute`, `client_attributes`, `user_attribute`
- `protocol_mapper`, `identity_provider_mapper`
- `authentication_config`, `identity_provider_config`

And 60+ more tables covering groups, roles, scopes, policies, and other Keycloak features.

### ID Column Conversions

All ID columns have been converted from VARCHAR(36) to UUID:

```sql
-- Before (original SQL)
id varchar(36) not null
realm_id varchar(36)

-- After (modified SQL)
id uuid not null
realm_id uuid
```

This applies to:
- All columns named `id`
- All columns ending with `_id` (e.g., `realm_id`, `client_id`, `user_id`)

Foreign key references have been automatically updated to match the UUID type.

### Migration Implementation

The migration reads the embedded SQL file and executes it:

```csharp
[Migration(1, "Execute Complete Keycloak Schema from SQL")]
public class Migration_001_CompleteSchema : Migration
{
    public override void Up()
    {
        // Read embedded SQL script
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Keycloak.Database.database-schema.sql";
        
        using (var stream = assembly.GetManifestResourceStream(resourceName))
        using (var reader = new StreamReader(stream))
        {
            var sql = reader.ReadToEnd();
            Execute.Sql(sql);
        }
    }
}
```

## Current Status

✅ Complete database schema embedded as resource  
✅ All 87 tables with UUID ID columns  
✅ ID columns converted from VARCHAR(36) to UUID  
✅ Foreign keys updated to match UUID type  
✅ SQL script embedded in assembly  
✅ Migration executes embedded script  
✅ CLI runner with up/down/list commands  
✅ Clean build with zero errors/warnings  

## Packages Used

- FluentMigrator 7.1.0
- FluentMigrator.Runner 7.1.0
- FluentMigrator.Runner.Postgres 7.1.0
- Npgsql 9.0.4
- Microsoft.Extensions.DependencyInjection 9.0.x
- Microsoft.Extensions.Logging.Console 9.0.x

## Schema Modification Process

The database schema was modified programmatically using a Python script that:

1. Reads the original `database-schema.sql` file
2. Identifies all `id` and `*_id` columns with VARCHAR(36) type
3. Converts them to UUID type
4. Preserves all other column attributes and constraints
5. Foreign key references are automatically compatible with the UUID type

This ensures:
- ✅ All ID columns use proper UUID type
- ✅ Foreign key relationships remain intact
- ✅ Performance benefits of native UUID type
- ✅ PostgreSQL best practices

## License

This port follows the same Apache 2.0 license as the original Keycloak project.
