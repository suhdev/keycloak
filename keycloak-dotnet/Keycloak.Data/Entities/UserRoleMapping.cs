namespace Keycloak.Data.Entities;

public class UserRoleMapping
{
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }

    public UserEntity? User { get; set; }
}
