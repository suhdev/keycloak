namespace Keycloak.Data.Entities;

/// <summary>
/// Represents a role in Keycloak.
/// </summary>
public class KeycloakRole
{
    public Guid Id { get; set; }
    public string? ClientRealmConstraint { get; set; }
    public bool ClientRole { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }
    public Guid? RealmId { get; set; }
    public Guid? ClientId { get; set; }
    public string? Realm { get; set; }

    // Navigation properties
    public ICollection<CompositeRole> CompositeRoles { get; set; } = new List<CompositeRole>();
    public ICollection<RoleAttribute> RoleAttributes { get; set; } = new List<RoleAttribute>();
}
