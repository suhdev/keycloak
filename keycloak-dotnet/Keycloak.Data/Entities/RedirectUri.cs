namespace Keycloak.Data.Entities;

public class RedirectUri
{
    public Guid ClientId { get; set; }
    public string Value { get; set; } = string.Empty;

    public Client? Client { get; set; }
}
