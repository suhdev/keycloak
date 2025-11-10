using FluentMigrator;

namespace Keycloak.Database.Migrations;

/// <summary>
/// Updates for Keycloak version 1.1.0.
/// Combines Liquibase migrations: jpa-changelog-1.1.0.Beta1.xml, jpa-changelog-1.1.0.Final.xml
/// </summary>
[Migration(2, "Version 1.1.0 Updates")]
public class Migration_002_Version_1_1_0 : Migration
{
    public override void Up()
    {
        // Clean up session tables (from Beta1)
        Delete.FromTable("CLIENT_SESSION_ROLE").AllRows();
        Delete.FromTable("CLIENT_SESSION").AllRows();
        Delete.FromTable("USER_SESSION").AllRows();

        // CLIENT_ATTRIBUTES
        Create.Table("CLIENT_ATTRIBUTES")
            .WithColumn("CLIENT_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(2048).Nullable()
            .WithColumn("NAME").AsString(255).NotNullable();

        Create.PrimaryKey("CONSTRAINT_3C")
            .OnTable("CLIENT_ATTRIBUTES")
            .Columns("CLIENT_ID", "NAME");

        Create.ForeignKey("FK3C47C64BEACCA966")
            .FromTable("CLIENT_ATTRIBUTES").ForeignColumn("CLIENT_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        // CLIENT_SESSION_NOTE
        Create.Table("CLIENT_SESSION_NOTE")
            .WithColumn("NAME").AsString(255).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable()
            .WithColumn("CLIENT_SESSION").AsString(36).NotNullable();

        Create.PrimaryKey("CONSTRAINT_5E")
            .OnTable("CLIENT_SESSION_NOTE")
            .Columns("CLIENT_SESSION", "NAME");

        Create.ForeignKey("FK5EDFB00FF51C2736")
            .FromTable("CLIENT_SESSION_NOTE").ForeignColumn("CLIENT_SESSION")
            .ToTable("CLIENT_SESSION").PrimaryColumn("ID");

        // APP_NODE_REGISTRATIONS
        Create.Table("APP_NODE_REGISTRATIONS")
            .WithColumn("APPLICATION_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsInt32().Nullable()
            .WithColumn("NAME").AsString(255).NotNullable();

        Create.PrimaryKey("CONSTRAINT_84")
            .OnTable("APP_NODE_REGISTRATIONS")
            .Columns("APPLICATION_ID", "NAME");

        Create.ForeignKey("FK8454723BA992F594")
            .FromTable("APP_NODE_REGISTRATIONS").ForeignColumn("APPLICATION_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        // Add columns to CLIENT_SESSION
        Alter.Table("CLIENT_SESSION")
            .AddColumn("AUTH_METHOD").AsString(255).Nullable()
            .AddColumn("REALM_ID").AsString(255).Nullable();

        // Add columns to CLIENT
        Alter.Table("CLIENT")
            .AddColumn("PROTOCOL").AsString(255).Nullable()
            .AddColumn("NODE_REREG_TIMEOUT").AsInt32().WithDefaultValue(0);

        // Add columns to REALM
        Alter.Table("REALM")
            .AddColumn("CERTIFICATE").AsString(2048).Nullable()
            .AddColumn("CODE_SECRET").AsString(255).Nullable();

        // Note: The original migration has a custom change class (AddRealmCodeSecret)
        // which would generate code secrets for existing realms. 
        // In a production migration, this would need to be implemented.
    }

    public override void Down()
    {
        // Remove foreign keys
        Delete.ForeignKey("FK8454723BA992F594").OnTable("APP_NODE_REGISTRATIONS");
        Delete.ForeignKey("FK5EDFB00FF51C2736").OnTable("CLIENT_SESSION_NOTE");
        Delete.ForeignKey("FK3C47C64BEACCA966").OnTable("CLIENT_ATTRIBUTES");

        // Drop tables
        Delete.Table("APP_NODE_REGISTRATIONS");
        Delete.Table("CLIENT_SESSION_NOTE");
        Delete.Table("CLIENT_ATTRIBUTES");

        // Remove columns from REALM
        Delete.Column("CODE_SECRET").FromTable("REALM");
        Delete.Column("CERTIFICATE").FromTable("REALM");

        // Remove columns from CLIENT
        Delete.Column("NODE_REREG_TIMEOUT").FromTable("CLIENT");
        Delete.Column("PROTOCOL").FromTable("CLIENT");

        // Remove columns from CLIENT_SESSION
        Delete.Column("REALM_ID").FromTable("CLIENT_SESSION");
        Delete.Column("AUTH_METHOD").FromTable("CLIENT_SESSION");
    }
}
