namespace Keycloak.Data.Entities;

public class CompositeRole
{
    public Guid Composite { get; set; }
    public Guid ChildRole { get; set; }
}
