namespace Keycloak.Data.Entities;

public class RoleAttribute
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Value { get; set; }
}
