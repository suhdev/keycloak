namespace Keycloak.Data.Entities;

public class ClientAttribute
{
    public Guid ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Value { get; set; }

    public Client? Client { get; set; }
}
