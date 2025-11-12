namespace Keycloak.Data.Entities;

/// <summary>
/// Represents an OAuth/OIDC client in Keycloak.
/// </summary>
public class Client
{
    public Guid Id { get; set; }
    public bool Enabled { get; set; }
    public bool FullScopeAllowed { get; set; }
    public string? ClientId { get; set; }
    public int? NotBefore { get; set; }
    public bool PublicClient { get; set; }
    public string? Secret { get; set; }
    public string? BaseUrl { get; set; }
    public bool BearerOnly { get; set; }
    public string? ManagementUrl { get; set; }
    public bool SurrogateAuthRequired { get; set; }
    public Guid? RealmId { get; set; }
    public string? Protocol { get; set; }
    public int NodeReregTimeout { get; set; }
    public bool FrontchannelLogout { get; set; }
    public bool ConsentRequired { get; set; }
    public string? Name { get; set; }
    public bool ServiceAccountsEnabled { get; set; }
    public string? ClientAuthenticatorType { get; set; }
    public string? RootUrl { get; set; }
    public string? Description { get; set; }
    public string? RegistrationToken { get; set; }
    public bool StandardFlowEnabled { get; set; }
    public bool ImplicitFlowEnabled { get; set; }
    public bool DirectAccessGrantsEnabled { get; set; }
    public bool AlwaysDisplayInConsole { get; set; }

    // Navigation properties
    public Realm? Realm { get; set; }
    public ICollection<ClientAttribute> ClientAttributes { get; set; } = new List<ClientAttribute>();
    public ICollection<RedirectUri> RedirectUris { get; set; } = new List<RedirectUri>();
}
