using Keycloak.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keycloak.Data.Configuration;

public class ClientAttributeConfiguration : IEntityTypeConfiguration<ClientAttribute>
{
    public void Configure(EntityTypeBuilder<ClientAttribute> builder)
    {
        builder.ToTable("client_attributes");

        builder.HasKey(ca => new { ca.ClientId, ca.Name })
            .HasName("constraint_3c");

        builder.Property(ca => ca.ClientId).HasColumnName("client_id");
        builder.Property(ca => ca.Name).HasColumnName("name").HasMaxLength(255).IsRequired();
        builder.Property(ca => ca.Value).HasColumnName("value").HasMaxLength(4000);
    }
}
