namespace Keycloak.Data.Entities;

public class Credential
{
    public Guid Id { get; set; }
    public byte[]? Salt { get; set; }
    public string? Type { get; set; }
    public Guid? UserId { get; set; }
    public long? CreatedDate { get; set; }
    public string? UserLabel { get; set; }
    public string? SecretData { get; set; }
    public string? CredentialData { get; set; }
    public int Priority { get; set; }

    public UserEntity? User { get; set; }
}
