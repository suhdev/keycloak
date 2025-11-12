namespace Keycloak.Data.Entities;

/// <summary>
/// Represents a Keycloak realm (tenant).
/// </summary>
public class Realm
{
    public Guid Id { get; set; }
    public int? AccessCodeLifespan { get; set; }
    public int? UserActionLifespan { get; set; }
    public int? AccessTokenLifespan { get; set; }
    public string? AccountTheme { get; set; }
    public string? AdminTheme { get; set; }
    public string? EmailTheme { get; set; }
    public bool Enabled { get; set; }
    public bool EventsEnabled { get; set; }
    public long? EventsExpiration { get; set; }
    public string? LoginTheme { get; set; }
    public string? Name { get; set; }
    public int? NotBefore { get; set; }
    public string? PasswordPolicy { get; set; }
    public bool RegistrationAllowed { get; set; }
    public bool RememberMe { get; set; }
    public bool ResetPasswordAllowed { get; set; }
    public bool Social { get; set; }
    public string? SslRequired { get; set; }
    public int? SsoIdleTimeout { get; set; }
    public int? SsoMaxLifespan { get; set; }
    public bool UpdateProfileOnSocLogin { get; set; }
    public bool VerifyEmail { get; set; }
    public string? MasterAdminClient { get; set; }
    public int? LoginLifespan { get; set; }
    public bool InternationalizationEnabled { get; set; }
    public string? DefaultLocale { get; set; }
    public bool RegEmailAsUsername { get; set; }
    public bool AdminEventsEnabled { get; set; }
    public bool AdminEventsDetailsEnabled { get; set; }
    public bool EditUsernameAllowed { get; set; }
    public int OtpPolicyCounter { get; set; }
    public int OtpPolicyWindow { get; set; }
    public int OtpPolicyPeriod { get; set; }
    public int OtpPolicyDigits { get; set; }
    public string? OtpPolicyAlg { get; set; }
    public string? OtpPolicyType { get; set; }
    public string? BrowserFlow { get; set; }
    public string? RegistrationFlow { get; set; }
    public string? DirectGrantFlow { get; set; }
    public string? ResetCredentialsFlow { get; set; }
    public string? ClientAuthFlow { get; set; }
    public int OfflineSessionIdleTimeout { get; set; }
    public bool RevokeRefreshToken { get; set; }
    public int AccessTokenLifeImplicit { get; set; }
    public bool LoginWithEmailAllowed { get; set; }
    public bool DuplicateEmailsAllowed { get; set; }
    public string? DockerAuthFlow { get; set; }
    public int RefreshTokenMaxReuse { get; set; }
    public bool AllowUserManagedAccess { get; set; }
    public int SsoMaxLifespanRememberMe { get; set; }
    public int SsoIdleTimeoutRememberMe { get; set; }
    public string? DefaultRole { get; set; }

    // Navigation properties
    public ICollection<Client> Clients { get; set; } = new List<Client>();
    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    public ICollection<KeycloakRole> Roles { get; set; } = new List<KeycloakRole>();
    public ICollection<RealmAttribute> RealmAttributes { get; set; } = new List<RealmAttribute>();
}
