# Keycloak .NET Core Port

This is a .NET Core port of Keycloak, focusing on porting the complete database schema using FluentMigrator.

## Project Structure

- **Keycloak.Database**: Class library containing FluentMigrator database migrations  
- **Keycloak.Database.Runner**: Console application for running migrations  
- **Migrations/**: Database migration files based on database-schema.sql

## Database Migrations

The database migration is generated from the complete database schema file (`database-schema.sql`) which represents the current production Keycloak schema with all 87 tables.

### Migration Approach

Instead of incrementally porting 76 Liquibase XML files, this approach uses the final database schema as the source of truth:

1. **Single Comprehensive Migration** - `Migration_001_CompleteSchema.cs`
   - Creates all 87 Keycloak database tables
   - Includes indexes, constraints, and relationships
   - ID columns use UUID type (converted from VARCHAR(36))
   - Column names and constraint names match the source schema exactly

### Key Features

- **UUID Type for IDs**: All `id` and `*_id` columns use PostgreSQL UUID type instead of VARCHAR(36)
- **Constraint Name Matching**: All constraint names (primary keys, foreign keys, unique constraints) match the source schema
- **Complete Schema**: Single migration creates the entire Keycloak database structure
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

As requested, all ID columns have been converted from VARCHAR(36) to UUID:

```csharp
// Original SQL: id varchar(36) not null
// FluentMigrator: 
.WithColumn("id").AsGuid().NotNullable()

// Original SQL: realm_id varchar(36)
// FluentMigrator:
.WithColumn("realm_id").AsGuid().Nullable()
```

### Constraint Names

All constraint names match the source schema exactly:

```csharp
// Primary keys
.PrimaryKey("constraint_7")           // client
.PrimaryKey("constraint_4a")          // realm
.PrimaryKey("constraint_fb")          // user_entity

// Unique constraints
.UniqueConstraint("uk_b71cjlbenv945rb6gcon438at")  // client (realm_id, client_id)
.UniqueConstraint("uk_orvsdmla56612eaefiq6wl5oi")  // realm (name)
```

## Current Status

✅ Complete database schema ported from SQL to FluentMigrator  
✅ All 87 tables created with proper data types  
✅ ID columns converted to UUID type  
✅ Constraint names match source schema  
✅ Indexes created for performance  
✅ CLI runner with up/down/list commands  
✅ Clean build with zero errors/warnings  

## Packages Used

- FluentMigrator 7.1.0
- FluentMigrator.Runner 7.1.0
- FluentMigrator.Runner.Postgres 7.1.0
- Npgsql 9.0.4
- Microsoft.Extensions.DependencyInjection 9.0.x
- Microsoft.Extensions.Logging.Console 9.0.x

## Migration Generation

The migration was generated programmatically from `database-schema.sql` using a Python script that:

1. Parses PostgreSQL CREATE TABLE statements
2. Converts column types to FluentMigrator syntax
3. Identifies ID columns (ending in `_id` or named `id`) and converts VARCHAR(36) to UUID
4. Preserves all column attributes (NOT NULL, DEFAULT values)
5. Extracts and applies constraints and indexes
6. Maintains exact constraint naming

This ensures 100% fidelity to the source schema while adapting to .NET/FluentMigrator conventions.

## License

This port follows the same Apache 2.0 license as the original Keycloak project.
