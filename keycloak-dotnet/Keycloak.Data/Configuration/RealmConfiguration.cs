using Keycloak.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keycloak.Data.Configuration;

public class RealmConfiguration : IEntityTypeConfiguration<Realm>
{
    public void Configure(EntityTypeBuilder<Realm> builder)
    {
        builder.ToTable("realm");

        builder.HasKey(r => r.Id)
            .HasName("constraint_4a");

        builder.Property(r => r.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(r => r.Name)
            .HasColumnName("name")
            .HasMaxLength(255);

        builder.HasIndex(r => r.Name)
            .IsUnique()
            .HasDatabaseName("uk_orvsdmla56612eaefiq6wl5oi");

        builder.Property(r => r.AccessCodeLifespan).HasColumnName("access_code_lifespan");
        builder.Property(r => r.UserActionLifespan).HasColumnName("user_action_lifespan");
        builder.Property(r => r.AccessTokenLifespan).HasColumnName("access_token_lifespan");
        builder.Property(r => r.AccountTheme).HasColumnName("account_theme").HasMaxLength(255);
        builder.Property(r => r.AdminTheme).HasColumnName("admin_theme").HasMaxLength(255);
        builder.Property(r => r.EmailTheme).HasColumnName("email_theme").HasMaxLength(255);
        builder.Property(r => r.Enabled).HasColumnName("enabled").HasDefaultValue(false);
        builder.Property(r => r.EventsEnabled).HasColumnName("events_enabled").HasDefaultValue(false);
        builder.Property(r => r.EventsExpiration).HasColumnName("events_expiration");
        builder.Property(r => r.LoginTheme).HasColumnName("login_theme").HasMaxLength(255);
        builder.Property(r => r.NotBefore).HasColumnName("not_before");
        builder.Property(r => r.PasswordPolicy).HasColumnName("password_policy").HasMaxLength(2550);
        builder.Property(r => r.RegistrationAllowed).HasColumnName("registration_allowed").HasDefaultValue(false);
        builder.Property(r => r.RememberMe).HasColumnName("remember_me").HasDefaultValue(false);
        builder.Property(r => r.ResetPasswordAllowed).HasColumnName("reset_password_allowed").HasDefaultValue(false);
        builder.Property(r => r.Social).HasColumnName("social").HasDefaultValue(false);
        builder.Property(r => r.SslRequired).HasColumnName("ssl_required").HasMaxLength(255);
        builder.Property(r => r.SsoIdleTimeout).HasColumnName("sso_idle_timeout");
        builder.Property(r => r.SsoMaxLifespan).HasColumnName("sso_max_lifespan");
        builder.Property(r => r.UpdateProfileOnSocLogin).HasColumnName("update_profile_on_soc_login").HasDefaultValue(false);
        builder.Property(r => r.VerifyEmail).HasColumnName("verify_email").HasDefaultValue(false);
        builder.Property(r => r.MasterAdminClient).HasColumnName("master_admin_client").HasMaxLength(36);
        builder.Property(r => r.LoginLifespan).HasColumnName("login_lifespan");
        builder.Property(r => r.InternationalizationEnabled).HasColumnName("internationalization_enabled").HasDefaultValue(false);
        builder.Property(r => r.DefaultLocale).HasColumnName("default_locale").HasMaxLength(255);
        builder.Property(r => r.RegEmailAsUsername).HasColumnName("reg_email_as_username").HasDefaultValue(false);
        builder.Property(r => r.AdminEventsEnabled).HasColumnName("admin_events_enabled").HasDefaultValue(false);
        builder.Property(r => r.AdminEventsDetailsEnabled).HasColumnName("admin_events_details_enabled").HasDefaultValue(false);
        builder.Property(r => r.EditUsernameAllowed).HasColumnName("edit_username_allowed").HasDefaultValue(false);
        builder.Property(r => r.OtpPolicyCounter).HasColumnName("otp_policy_counter").HasDefaultValue(0);
        builder.Property(r => r.OtpPolicyWindow).HasColumnName("otp_policy_window").HasDefaultValue(1);
        builder.Property(r => r.OtpPolicyPeriod).HasColumnName("otp_policy_period").HasDefaultValue(30);
        builder.Property(r => r.OtpPolicyDigits).HasColumnName("otp_policy_digits").HasDefaultValue(6);
        builder.Property(r => r.OtpPolicyAlg).HasColumnName("otp_policy_alg").HasMaxLength(36).HasDefaultValue("HmacSHA1");
        builder.Property(r => r.OtpPolicyType).HasColumnName("otp_policy_type").HasMaxLength(36).HasDefaultValue("totp");
        builder.Property(r => r.BrowserFlow).HasColumnName("browser_flow").HasMaxLength(36);
        builder.Property(r => r.RegistrationFlow).HasColumnName("registration_flow").HasMaxLength(36);
        builder.Property(r => r.DirectGrantFlow).HasColumnName("direct_grant_flow").HasMaxLength(36);
        builder.Property(r => r.ResetCredentialsFlow).HasColumnName("reset_credentials_flow").HasMaxLength(36);
        builder.Property(r => r.ClientAuthFlow).HasColumnName("client_auth_flow").HasMaxLength(36);
        builder.Property(r => r.OfflineSessionIdleTimeout).HasColumnName("offline_session_idle_timeout").HasDefaultValue(0);
        builder.Property(r => r.RevokeRefreshToken).HasColumnName("revoke_refresh_token").HasDefaultValue(false);
        builder.Property(r => r.AccessTokenLifeImplicit).HasColumnName("access_token_life_implicit").HasDefaultValue(0);
        builder.Property(r => r.LoginWithEmailAllowed).HasColumnName("login_with_email_allowed").HasDefaultValue(true);
        builder.Property(r => r.DuplicateEmailsAllowed).HasColumnName("duplicate_emails_allowed").HasDefaultValue(false);
        builder.Property(r => r.DockerAuthFlow).HasColumnName("docker_auth_flow").HasMaxLength(36);
        builder.Property(r => r.RefreshTokenMaxReuse).HasColumnName("refresh_token_max_reuse").HasDefaultValue(0);
        builder.Property(r => r.AllowUserManagedAccess).HasColumnName("allow_user_managed_access").HasDefaultValue(false);
        builder.Property(r => r.SsoMaxLifespanRememberMe).HasColumnName("sso_max_lifespan_remember_me").HasDefaultValue(0);
        builder.Property(r => r.SsoIdleTimeoutRememberMe).HasColumnName("sso_idle_timeout_remember_me").HasDefaultValue(0);
        builder.Property(r => r.DefaultRole).HasColumnName("default_role").HasMaxLength(255);

        // Navigation properties
        builder.HasMany(r => r.Clients)
            .WithOne(c => c.Realm)
            .HasForeignKey(c => c.RealmId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Users)
            .WithOne(u => u.Realm)
            .HasForeignKey(u => u.RealmId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.RealmAttributes)
            .WithOne(ra => ra.Realm)
            .HasForeignKey(ra => ra.RealmId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
