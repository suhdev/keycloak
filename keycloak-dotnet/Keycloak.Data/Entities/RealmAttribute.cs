namespace Keycloak.Data.Entities;

public class RealmAttribute
{
    public string Name { get; set; } = string.Empty;
    public Guid RealmId { get; set; }
    public string? Value { get; set; }

    public Realm? Realm { get; set; }
}
