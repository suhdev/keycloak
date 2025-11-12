using Keycloak.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keycloak.Data.Configuration;

public class UserRoleMappingConfiguration : IEntityTypeConfiguration<UserRoleMapping>
{
    public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
    {
        builder.ToTable("user_role_mapping");

        builder.HasKey(urm => new { urm.RoleId, urm.UserId })
            .HasName("constraint_6");

        builder.Property(urm => urm.RoleId).HasColumnName("role_id");
        builder.Property(urm => urm.UserId).HasColumnName("user_id");
    }
}
