using Keycloak.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keycloak.Data.Configuration;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("user_entity");

        builder.HasKey(u => u.Id)
            .HasName("constraint_fb");

        builder.Property(u => u.Id).HasColumnName("id").IsRequired();
        builder.Property(u => u.Email).HasColumnName("email").HasMaxLength(255);
        builder.Property(u => u.EmailConstraint).HasColumnName("email_constraint").HasMaxLength(255);
        builder.Property(u => u.EmailVerified).HasColumnName("email_verified").HasDefaultValue(false);
        builder.Property(u => u.Enabled).HasColumnName("enabled").HasDefaultValue(false);
        builder.Property(u => u.FederationLink).HasColumnName("federation_link").HasMaxLength(255);
        builder.Property(u => u.FirstName).HasColumnName("first_name").HasMaxLength(255);
        builder.Property(u => u.LastName).HasColumnName("last_name").HasMaxLength(255);
        builder.Property(u => u.RealmId).HasColumnName("realm_id");
        builder.Property(u => u.Username).HasColumnName("username").HasMaxLength(255);
        builder.Property(u => u.CreatedTimestamp).HasColumnName("created_timestamp");
        builder.Property(u => u.ServiceAccountClientLink).HasColumnName("service_account_client_link").HasMaxLength(255);
        builder.Property(u => u.NotBefore).HasColumnName("not_before").HasDefaultValue(0);

        builder.HasIndex(u => new { u.RealmId, u.EmailConstraint })
            .IsUnique()
            .HasDatabaseName("uk_dykn684sl8up1crfei6eckhd7");

        builder.HasIndex(u => new { u.RealmId, u.Username })
            .IsUnique()
            .HasDatabaseName("uk_ru8tt6t700s9v50bu18ws5ha6");

        builder.HasIndex(u => new { u.RealmId, u.ServiceAccountClientLink })
            .HasDatabaseName("idx_user_service_account");

        builder.HasMany(u => u.UserAttributes)
            .WithOne(ua => ua.User)
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Credentials)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.UserRoleMappings)
            .WithOne(urm => urm.User)
            .HasForeignKey(urm => urm.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
