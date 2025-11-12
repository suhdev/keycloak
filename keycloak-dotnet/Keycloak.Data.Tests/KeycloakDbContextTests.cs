using Keycloak.Data;
using Keycloak.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace Keycloak.Data.Tests;

/// <summary>
/// Integration tests for Keycloak DbContext using PostgreSQL Testcontainer.
/// </summary>
public class KeycloakDbContextTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres;
    private KeycloakDbContext? _dbContext;

    public KeycloakDbContextTests()
    {
        _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .WithDatabase("keycloak_test")
            .WithUsername("keycloak")
            .WithPassword("keycloak")
            .Build();
    }

    public async Task InitializeAsync()
    {
        // Start the PostgreSQL container
        await _postgres.StartAsync();

        // Create DbContext with connection to test container
        var optionsBuilder = new DbContextOptionsBuilder<KeycloakDbContext>();
        optionsBuilder.UseNpgsql(_postgres.GetConnectionString());

        _dbContext = new KeycloakDbContext(optionsBuilder.Options);

        // Ensure database is created
        await _dbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        if (_dbContext != null)
        {
            await _dbContext.DisposeAsync();
        }

        await _postgres.DisposeAsync();
    }

    [Fact]
    public async Task CanConnectToDatabase()
    {
        // Arrange & Act
        var canConnect = await _dbContext!.Database.CanConnectAsync();

        // Assert
        Assert.True(canConnect);
    }

    [Fact]
    public async Task CanCreateAndRetrieveRealm()
    {
        // Arrange
        var realm = new Realm
        {
            Id = Guid.NewGuid(),
            Name = "test-realm",
            Enabled = true,
            RegistrationAllowed = true
        };

        // Act
        _dbContext!.Realms.Add(realm);
        await _dbContext.SaveChangesAsync();

        // Assert
        var retrievedRealm = await _dbContext.Realms.FindAsync(realm.Id);
        Assert.NotNull(retrievedRealm);
        Assert.Equal("test-realm", retrievedRealm.Name);
        Assert.True(retrievedRealm.Enabled);
    }

    [Fact]
    public async Task CanCreateAndRetrieveClient()
    {
        // Arrange
        var realm = new Realm
        {
            Id = Guid.NewGuid(),
            Name = "client-test-realm",
            Enabled = true
        };

        var client = new Client
        {
            Id = Guid.NewGuid(),
            ClientId = "test-client",
            Name = "Test Client",
            Enabled = true,
            RealmId = realm.Id,
            PublicClient = false,
            StandardFlowEnabled = true
        };

        // Act
        _dbContext!.Realms.Add(realm);
        _dbContext.Clients.Add(client);
        await _dbContext.SaveChangesAsync();

        // Assert
        var retrievedClient = await _dbContext.Clients
            .Include(c => c.Realm)
            .FirstOrDefaultAsync(c => c.ClientId == "test-client");

        Assert.NotNull(retrievedClient);
        Assert.Equal("test-client", retrievedClient.ClientId);
        Assert.Equal("Test Client", retrievedClient.Name);
        Assert.NotNull(retrievedClient.Realm);
        Assert.Equal("client-test-realm", retrievedClient.Realm.Name);
    }

    [Fact]
    public async Task CanCreateAndRetrieveUser()
    {
        // Arrange
        var realm = new Realm
        {
            Id = Guid.NewGuid(),
            Name = "user-test-realm",
            Enabled = true
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Email = "testuser@example.com",
            EmailVerified = true,
            Enabled = true,
            RealmId = realm.Id,
            FirstName = "Test",
            LastName = "User"
        };

        // Act
        _dbContext!.Realms.Add(realm);
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        // Assert
        var retrievedUser = await _dbContext.Users
            .Include(u => u.Realm)
            .FirstOrDefaultAsync(u => u.Username == "testuser");

        Assert.NotNull(retrievedUser);
        Assert.Equal("testuser", retrievedUser.Username);
        Assert.Equal("testuser@example.com", retrievedUser.Email);
        Assert.Equal("Test", retrievedUser.FirstName);
        Assert.Equal("User", retrievedUser.LastName);
        Assert.NotNull(retrievedUser.Realm);
    }

    [Fact]
    public async Task CanCreateRealmWithAttributes()
    {
        // Arrange
        var realm = new Realm
        {
            Id = Guid.NewGuid(),
            Name = "attribute-test-realm",
            Enabled = true
        };

        var attribute1 = new RealmAttribute
        {
            Name = "theme",
            RealmId = realm.Id,
            Value = "custom-theme"
        };

        var attribute2 = new RealmAttribute
        {
            Name = "displayName",
            RealmId = realm.Id,
            Value = "My Custom Realm"
        };

        // Act
        _dbContext!.Realms.Add(realm);
        _dbContext.RealmAttributes.AddRange(attribute1, attribute2);
        await _dbContext.SaveChangesAsync();

        // Assert
        var retrievedRealm = await _dbContext.Realms
            .Include(r => r.RealmAttributes)
            .FirstOrDefaultAsync(r => r.Name == "attribute-test-realm");

        Assert.NotNull(retrievedRealm);
        Assert.Equal(2, retrievedRealm.RealmAttributes.Count);
        Assert.Contains(retrievedRealm.RealmAttributes, a => a.Name == "theme" && a.Value == "custom-theme");
        Assert.Contains(retrievedRealm.RealmAttributes, a => a.Name == "displayName" && a.Value == "My Custom Realm");
    }

    [Fact]
    public async Task CanCreateClientWithRedirectUris()
    {
        // Arrange
        var realm = new Realm
        {
            Id = Guid.NewGuid(),
            Name = "redirect-test-realm",
            Enabled = true
        };

        var client = new Client
        {
            Id = Guid.NewGuid(),
            ClientId = "redirect-test-client",
            Enabled = true,
            RealmId = realm.Id
        };

        var redirectUri1 = new RedirectUri
        {
            ClientId = client.Id,
            Value = "http://localhost:8080/*"
        };

        var redirectUri2 = new RedirectUri
        {
            ClientId = client.Id,
            Value = "https://example.com/callback"
        };

        // Act
        _dbContext!.Realms.Add(realm);
        _dbContext.Clients.Add(client);
        _dbContext.Set<RedirectUri>().AddRange(redirectUri1, redirectUri2);
        await _dbContext.SaveChangesAsync();

        // Assert
        var retrievedClient = await _dbContext.Clients
            .Include(c => c.RedirectUris)
            .FirstOrDefaultAsync(c => c.ClientId == "redirect-test-client");

        Assert.NotNull(retrievedClient);
        Assert.Equal(2, retrievedClient.RedirectUris.Count);
        Assert.Contains(retrievedClient.RedirectUris, ru => ru.Value == "http://localhost:8080/*");
        Assert.Contains(retrievedClient.RedirectUris, ru => ru.Value == "https://example.com/callback");
    }

    [Fact]
    public async Task RealmNameMustBeUnique()
    {
        // Arrange
        var realm1 = new Realm
        {
            Id = Guid.NewGuid(),
            Name = "duplicate-realm",
            Enabled = true
        };

        var realm2 = new Realm
        {
            Id = Guid.NewGuid(),
            Name = "duplicate-realm",  // Same name
            Enabled = true
        };

        _dbContext!.Realms.Add(realm1);
        await _dbContext.SaveChangesAsync();

        // Act & Assert
        _dbContext.Realms.Add(realm2);
        await Assert.ThrowsAsync<DbUpdateException>(async () =>
        {
            await _dbContext.SaveChangesAsync();
        });
    }
}
