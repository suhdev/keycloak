using Keycloak.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Keycloak.Data;

/// <summary>
/// Entity Framework Core DbContext for Keycloak database.
/// </summary>
public class KeycloakDbContext : DbContext
{
    public KeycloakDbContext(DbContextOptions<KeycloakDbContext> options)
        : base(options)
    {
    }

    // Core entities
    public DbSet<Realm> Realms { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<KeycloakRole> Roles { get; set; }
    public DbSet<Credential> Credentials { get; set; }

    // Attribute entities
    public DbSet<RealmAttribute> RealmAttributes { get; set; }
    public DbSet<ClientAttribute> ClientAttributes { get; set; }
    public DbSet<UserAttribute> UserAttributes { get; set; }
    public DbSet<RoleAttribute> RoleAttributes { get; set; }

    // Relationship entities
    public DbSet<CompositeRole> CompositeRoles { get; set; }
    public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
    public DbSet<RedirectUri> RedirectUris { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the Configuration folder
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KeycloakDbContext).Assembly);
    }
}
