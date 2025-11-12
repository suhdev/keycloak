using Keycloak.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keycloak.Data.Configuration;

public class CompositeRoleConfiguration : IEntityTypeConfiguration<CompositeRole>
{
    public void Configure(EntityTypeBuilder<CompositeRole> builder)
    {
        builder.ToTable("composite_role");

        builder.HasKey(cr => new { cr.Composite, cr.ChildRole })
            .HasName("constraint_composite_role");

        builder.Property(cr => cr.Composite).HasColumnName("composite");
        builder.Property(cr => cr.ChildRole).HasColumnName("child_role");
    }
}
