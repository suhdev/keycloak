using Keycloak.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keycloak.Data.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("client");

        builder.HasKey(c => c.Id)
            .HasName("constraint_7");

        builder.Property(c => c.Id).HasColumnName("id").IsRequired();
        builder.Property(c => c.Enabled).HasColumnName("enabled").HasDefaultValue(false);
        builder.Property(c => c.FullScopeAllowed).HasColumnName("full_scope_allowed").HasDefaultValue(false);
        builder.Property(c => c.ClientId).HasColumnName("client_id").HasMaxLength(255);
        builder.Property(c => c.NotBefore).HasColumnName("not_before");
        builder.Property(c => c.PublicClient).HasColumnName("public_client").HasDefaultValue(false);
        builder.Property(c => c.Secret).HasColumnName("secret").HasMaxLength(255);
        builder.Property(c => c.BaseUrl).HasColumnName("base_url").HasMaxLength(255);
        builder.Property(c => c.BearerOnly).HasColumnName("bearer_only").HasDefaultValue(false);
        builder.Property(c => c.ManagementUrl).HasColumnName("management_url").HasMaxLength(255);
        builder.Property(c => c.SurrogateAuthRequired).HasColumnName("surrogate_auth_required").HasDefaultValue(false);
        builder.Property(c => c.RealmId).HasColumnName("realm_id");
        builder.Property(c => c.Protocol).HasColumnName("protocol").HasMaxLength(255);
        builder.Property(c => c.NodeReregTimeout).HasColumnName("node_rereg_timeout").HasDefaultValue(0);
        builder.Property(c => c.FrontchannelLogout).HasColumnName("frontchannel_logout").HasDefaultValue(false);
        builder.Property(c => c.ConsentRequired).HasColumnName("consent_required").HasDefaultValue(false);
        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(255);
        builder.Property(c => c.ServiceAccountsEnabled).HasColumnName("service_accounts_enabled").HasDefaultValue(false);
        builder.Property(c => c.ClientAuthenticatorType).HasColumnName("client_authenticator_type").HasMaxLength(255);
        builder.Property(c => c.RootUrl).HasColumnName("root_url").HasMaxLength(255);
        builder.Property(c => c.Description).HasColumnName("description").HasMaxLength(255);
        builder.Property(c => c.RegistrationToken).HasColumnName("registration_token").HasMaxLength(255);
        builder.Property(c => c.StandardFlowEnabled).HasColumnName("standard_flow_enabled").HasDefaultValue(true);
        builder.Property(c => c.ImplicitFlowEnabled).HasColumnName("implicit_flow_enabled").HasDefaultValue(false);
        builder.Property(c => c.DirectAccessGrantsEnabled).HasColumnName("direct_access_grants_enabled").HasDefaultValue(false);
        builder.Property(c => c.AlwaysDisplayInConsole).HasColumnName("always_display_in_console").HasDefaultValue(false);

        builder.HasIndex(c => new { c.RealmId, c.ClientId })
            .IsUnique()
            .HasDatabaseName("uk_b71cjlbenv945rb6gcon438at");

        builder.HasIndex(c => c.ClientId)
            .HasDatabaseName("idx_client_id");

        builder.HasMany(c => c.ClientAttributes)
            .WithOne(ca => ca.Client)
            .HasForeignKey(ca => ca.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.RedirectUris)
            .WithOne(ru => ru.Client)
            .HasForeignKey(ru => ru.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
