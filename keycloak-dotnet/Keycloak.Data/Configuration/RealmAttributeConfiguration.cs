using Keycloak.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keycloak.Data.Configuration;

public class RealmAttributeConfiguration : IEntityTypeConfiguration<RealmAttribute>
{
    public void Configure(EntityTypeBuilder<RealmAttribute> builder)
    {
        builder.ToTable("realm_attribute");

        builder.HasKey(ra => new { ra.Name, ra.RealmId })
            .HasName("constraint_9");

        builder.Property(ra => ra.Name).HasColumnName("name").HasMaxLength(255).IsRequired();
        builder.Property(ra => ra.RealmId).HasColumnName("realm_id").IsRequired();
        builder.Property(ra => ra.Value).HasColumnName("value").HasColumnType("text");
    }
}
