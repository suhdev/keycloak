namespace Keycloak.Data.Entities;

/// <summary>
/// Represents a user in Keycloak.
/// </summary>
public class UserEntity
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? EmailConstraint { get; set; }
    public bool EmailVerified { get; set; }
    public bool Enabled { get; set; }
    public string? FederationLink { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Guid? RealmId { get; set; }
    public string? Username { get; set; }
    public long? CreatedTimestamp { get; set; }
    public string? ServiceAccountClientLink { get; set; }
    public int NotBefore { get; set; }

    // Navigation properties
    public Realm? Realm { get; set; }
    public ICollection<UserAttribute> UserAttributes { get; set; } = new List<UserAttribute>();
    public ICollection<Credential> Credentials { get; set; } = new List<Credential>();
    public ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();
}
