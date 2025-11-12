namespace Keycloak.Data.Entities;

public class UserAttribute
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Value { get; set; }
    public Guid UserId { get; set; }
    public long? LongValueHash { get; set; }
    public string? LongValueHashLowerCase { get; set; }
    public string? LongValue { get; set; }

    public UserEntity? User { get; set; }
}
