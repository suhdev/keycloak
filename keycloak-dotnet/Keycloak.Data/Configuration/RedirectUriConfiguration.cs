using Keycloak.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keycloak.Data.Configuration;

public class RedirectUriConfiguration : IEntityTypeConfiguration<RedirectUri>
{
    public void Configure(EntityTypeBuilder<RedirectUri> builder)
    {
        builder.ToTable("redirect_uris");

        builder.HasKey(ru => new { ru.ClientId, ru.Value })
            .HasName("constraint_redirect_uris");

        builder.Property(ru => ru.ClientId).HasColumnName("client_id");
        builder.Property(ru => ru.Value).HasColumnName("value").HasMaxLength(255).IsRequired();
    }
}
