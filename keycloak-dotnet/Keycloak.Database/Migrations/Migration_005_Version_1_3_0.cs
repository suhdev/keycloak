using FluentMigrator;

namespace Keycloak.Database.Migrations;

/// <summary>
/// Updates for Keycloak version 1.3.0.
/// Combines Liquibase migrations: jpa-changelog-1.3.0.xml
/// Major changes: Authentication flows, user federation mappers, admin events
/// </summary>
[Migration(5, "Version 1.3.0 - Authentication flows and federation mappers")]
public class Migration_005_Version_1_3_0 : Migration
{
    public override void Up()
    {
        // Clean up session tables
        Delete.FromTable("CLIENT_SESSION_ROLE").AllRows();
        Delete.FromTable("CLIENT_SESSION_PROT_MAPPER").AllRows();
        Delete.FromTable("CLIENT_SESSION_NOTE").AllRows();
        Delete.FromTable("CLIENT_SESSION").AllRows();
        Delete.FromTable("USER_SESSION_NOTE").AllRows();
        Delete.FromTable("USER_SESSION").AllRows();

        // ADMIN_EVENT_ENTITY
        Create.Table("ADMIN_EVENT_ENTITY")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("PK_ADMIN_EVENT_ENTITY")
            .WithColumn("ADMIN_EVENT_TIME").AsInt64().Nullable()
            .WithColumn("REALM_ID").AsString(255).Nullable()
            .WithColumn("OPERATION_TYPE").AsString(255).Nullable()
            .WithColumn("AUTH_REALM_ID").AsString(255).Nullable()
            .WithColumn("AUTH_CLIENT_ID").AsString(255).Nullable()
            .WithColumn("AUTH_USER_ID").AsString(255).Nullable()
            .WithColumn("IP_ADDRESS").AsString(255).Nullable()
            .WithColumn("RESOURCE_PATH").AsString(2550).Nullable()
            .WithColumn("REPRESENTATION").AsCustom("TEXT").Nullable()
            .WithColumn("ERROR").AsString(255).Nullable();

        // AUTHENTICATOR
        Create.Table("AUTHENTICATOR")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_AUTH_PK")
            .WithColumn("ALIAS").AsString(255).Nullable()
            .WithColumn("REALM_ID").AsString(36).Nullable()
            .WithColumn("PROVIDER_ID").AsString(255).Nullable();

        Create.ForeignKey("FK_AUTH_REALM")
            .FromTable("AUTHENTICATOR").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        // AUTHENTICATION_FLOW
        Create.Table("AUTHENTICATION_FLOW")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_AUTH_FLOW_PK")
            .WithColumn("ALIAS").AsString(255).Nullable()
            .WithColumn("DESCRIPTION").AsString(255).Nullable()
            .WithColumn("REALM_ID").AsString(36).Nullable();

        Create.ForeignKey("FK_AUTH_FLOW_REALM")
            .FromTable("AUTHENTICATION_FLOW").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        // AUTHENTICATION_EXECUTION
        Create.Table("AUTHENTICATION_EXECUTION")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_AUTH_EXEC_PK")
            .WithColumn("ALIAS").AsString(255).Nullable()
            .WithColumn("AUTHENTICATOR").AsString(36).Nullable()
            .WithColumn("REALM_ID").AsString(36).Nullable()
            .WithColumn("FLOW_ID").AsString(36).Nullable()
            .WithColumn("REQUIREMENT").AsInt32().Nullable()
            .WithColumn("PRIORITY").AsInt32().Nullable()
            .WithColumn("USER_SETUP_ALLOWED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("AUTHENTICATOR_FLOW").AsBoolean().NotNullable().WithDefaultValue(false);

        Create.ForeignKey("FK_AUTH_EXEC_REALM")
            .FromTable("AUTHENTICATION_EXECUTION").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_AUTH_EXEC_FLOW")
            .FromTable("AUTHENTICATION_EXECUTION").ForeignColumn("FLOW_ID")
            .ToTable("AUTHENTICATION_FLOW").PrimaryColumn("ID");

        // AUTHENTICATOR_CONFIG
        Create.Table("AUTHENTICATOR_CONFIG")
            .WithColumn("AUTHENTICATOR_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsCustom("TEXT").Nullable()
            .WithColumn("NAME").AsString(255).NotNullable();

        Create.PrimaryKey("CONSTRAINT_AUTH_CFG_PK")
            .OnTable("AUTHENTICATOR_CONFIG")
            .Columns("AUTHENTICATOR_ID", "NAME");

        // USER_FEDERATION_MAPPER
        Create.Table("USER_FEDERATION_MAPPER")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_FEDMAPPERPM")
            .WithColumn("NAME").AsString(255).NotNullable()
            .WithColumn("FEDERATION_PROVIDER_ID").AsString(36).NotNullable()
            .WithColumn("FEDERATION_MAPPER_TYPE").AsString(255).NotNullable()
            .WithColumn("REALM_ID").AsString(36).NotNullable();

        Create.ForeignKey("FK_FEDMAPPERPM_REALM")
            .FromTable("USER_FEDERATION_MAPPER").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_FEDMAPPERPM_FEDPRV")
            .FromTable("USER_FEDERATION_MAPPER").ForeignColumn("FEDERATION_PROVIDER_ID")
            .ToTable("USER_FEDERATION_PROVIDER").PrimaryColumn("ID");

        // USER_FEDERATION_MAPPER_CONFIG
        Create.Table("USER_FEDERATION_MAPPER_CONFIG")
            .WithColumn("USER_FEDERATION_MAPPER_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable()
            .WithColumn("NAME").AsString(255).NotNullable();

        Create.PrimaryKey("CONSTRAINT_FEDMAPPER_CFG_PM")
            .OnTable("USER_FEDERATION_MAPPER_CONFIG")
            .Columns("USER_FEDERATION_MAPPER_ID", "NAME");

        Create.ForeignKey("FK_FEDMAPPER_CFG")
            .FromTable("USER_FEDERATION_MAPPER_CONFIG").ForeignColumn("USER_FEDERATION_MAPPER_ID")
            .ToTable("USER_FEDERATION_MAPPER").PrimaryColumn("ID");

        // Add columns to REALM
        Alter.Table("REALM")
            .AddColumn("ADMIN_EVENTS_ENABLED").AsBoolean().NotNullable().WithDefaultValue(false)
            .AddColumn("ADMIN_EVENTS_DETAILS_ENABLED").AsBoolean().NotNullable().WithDefaultValue(false)
            .AddColumn("EDIT_USERNAME_ALLOWED").AsBoolean().NotNullable().WithDefaultValue(false);

        // CLIENT_SESSION_AUTH_STATUS
        Create.Table("CLIENT_SESSION_AUTH_STATUS")
            .WithColumn("AUTHENTICATOR").AsString(36).NotNullable()
            .WithColumn("STATUS").AsInt32().Nullable()
            .WithColumn("CLIENT_SESSION").AsString(36).NotNullable();

        Create.PrimaryKey("CONSTRAINT_AUTH_STATUS_PK")
            .OnTable("CLIENT_SESSION_AUTH_STATUS")
            .Columns("CLIENT_SESSION", "AUTHENTICATOR");

        Create.ForeignKey("AUTH_STATUS_CONSTRAINT")
            .FromTable("CLIENT_SESSION_AUTH_STATUS").ForeignColumn("CLIENT_SESSION")
            .ToTable("CLIENT_SESSION").PrimaryColumn("ID");

        // Add column to CLIENT_SESSION
        Alter.Table("CLIENT_SESSION")
            .AddColumn("AUTH_USER_ID").AsString(36).Nullable();

        // Add columns to IDENTITY_PROVIDER
        Alter.Table("IDENTITY_PROVIDER")
            .AddColumn("TRUST_EMAIL").AsBoolean().NotNullable().WithDefaultValue(false)
            .AddColumn("UPDATE_PROFILE_FIRST_LGN_MD").AsString(255).NotNullable().WithDefaultValue("on");

        // Migrate UPDATE_PROFILE_FIRST_LOGIN to UPDATE_PROFILE_FIRST_LGN_MD
        Execute.Sql("UPDATE IDENTITY_PROVIDER SET UPDATE_PROFILE_FIRST_LGN_MD = 'off' WHERE UPDATE_PROFILE_FIRST_LOGIN = false");

        // Drop old column
        Delete.Column("UPDATE_PROFILE_FIRST_LOGIN").FromTable("IDENTITY_PROVIDER");

        // Update USER_REQUIRED_ACTION table
        Alter.Table("USER_REQUIRED_ACTION")
            .AddColumn("REQUIRED_ACTION").AsString(255).NotNullable().WithDefaultValue(" ");

        // Migrate ACTION enum to REQUIRED_ACTION string
        Execute.Sql("UPDATE USER_REQUIRED_ACTION SET REQUIRED_ACTION = 'VERIFY_EMAIL' WHERE ACTION = 0");
        Execute.Sql("UPDATE USER_REQUIRED_ACTION SET REQUIRED_ACTION = 'UPDATE_PROFILE' WHERE ACTION = 1");
        Execute.Sql("UPDATE USER_REQUIRED_ACTION SET REQUIRED_ACTION = 'CONFIGURE_TOTP' WHERE ACTION = 2");
        Execute.Sql("UPDATE USER_REQUIRED_ACTION SET REQUIRED_ACTION = 'UPDATE_PASSWORD' WHERE ACTION = 3");

        // Drop old primary key and column
        Delete.PrimaryKey("CONSTRAINT_2").FromTable("USER_REQUIRED_ACTION");
        Delete.Column("ACTION").FromTable("USER_REQUIRED_ACTION");

        // Add new primary key
        Create.PrimaryKey("CONSTRAINT_REQUIRED_ACTION")
            .OnTable("USER_REQUIRED_ACTION")
            .Columns("REQUIRED_ACTION", "USER_ID");

        // Drop old REALM column
        Delete.Column("PASSWORD_CRED_GRANT_ALLOWED").FromTable("REALM");

        // KEYCLOAK-1298: Fix constraint names to be uppercase
        // Fix PROTOCOL_MAPPER_CONFIG
        Delete.ForeignKey("FK_PMConfig").OnTable("PROTOCOL_MAPPER_CONFIG");
        Delete.PrimaryKey("CONSTRAINT_PMConfig").FromTable("PROTOCOL_MAPPER_CONFIG");

        Create.PrimaryKey("CONSTRAINT_PMCONFIG")
            .OnTable("PROTOCOL_MAPPER_CONFIG")
            .Columns("PROTOCOL_MAPPER_ID", "NAME");

        Create.ForeignKey("FK_PMCONFIG")
            .FromTable("PROTOCOL_MAPPER_CONFIG").ForeignColumn("PROTOCOL_MAPPER_ID")
            .ToTable("PROTOCOL_MAPPER").PrimaryColumn("ID");

        // Note: IDP_MAPPER tables referenced in XML but not yet created
        // These will be handled in a later migration
    }

    public override void Down()
    {
        // Restore PROTOCOL_MAPPER_CONFIG constraints
        Delete.ForeignKey("FK_PMCONFIG").OnTable("PROTOCOL_MAPPER_CONFIG");
        Delete.PrimaryKey("CONSTRAINT_PMCONFIG").FromTable("PROTOCOL_MAPPER_CONFIG");

        Create.PrimaryKey("CONSTRAINT_PMConfig")
            .OnTable("PROTOCOL_MAPPER_CONFIG")
            .Columns("PROTOCOL_MAPPER_ID", "NAME");

        Create.ForeignKey("FK_PMConfig")
            .FromTable("PROTOCOL_MAPPER_CONFIG").ForeignColumn("PROTOCOL_MAPPER_ID")
            .ToTable("PROTOCOL_MAPPER").PrimaryColumn("ID");

        // Restore REALM column
        Alter.Table("REALM")
            .AddColumn("PASSWORD_CRED_GRANT_ALLOWED").AsBoolean().NotNullable().WithDefaultValue(false);

        // Restore USER_REQUIRED_ACTION
        Delete.PrimaryKey("CONSTRAINT_REQUIRED_ACTION").FromTable("USER_REQUIRED_ACTION");
        Alter.Table("USER_REQUIRED_ACTION")
            .AddColumn("ACTION").AsInt32().NotNullable().WithDefaultValue(0);

        Create.PrimaryKey("CONSTRAINT_2")
            .OnTable("USER_REQUIRED_ACTION")
            .Columns("ACTION", "USER_ID");

        Delete.Column("REQUIRED_ACTION").FromTable("USER_REQUIRED_ACTION");

        // Restore IDENTITY_PROVIDER columns
        Alter.Table("IDENTITY_PROVIDER")
            .AddColumn("UPDATE_PROFILE_FIRST_LOGIN").AsBoolean().NotNullable().WithDefaultValue(false);

        Delete.Column("UPDATE_PROFILE_FIRST_LGN_MD").FromTable("IDENTITY_PROVIDER");
        Delete.Column("TRUST_EMAIL").FromTable("IDENTITY_PROVIDER");

        // Remove CLIENT_SESSION column
        Delete.Column("AUTH_USER_ID").FromTable("CLIENT_SESSION");

        // Remove CLIENT_SESSION_AUTH_STATUS
        Delete.ForeignKey("AUTH_STATUS_CONSTRAINT").OnTable("CLIENT_SESSION_AUTH_STATUS");
        Delete.Table("CLIENT_SESSION_AUTH_STATUS");

        // Remove REALM columns
        Delete.Column("EDIT_USERNAME_ALLOWED").FromTable("REALM");
        Delete.Column("ADMIN_EVENTS_DETAILS_ENABLED").FromTable("REALM");
        Delete.Column("ADMIN_EVENTS_ENABLED").FromTable("REALM");

        // Remove USER_FEDERATION_MAPPER tables
        Delete.ForeignKey("FK_FEDMAPPER_CFG").OnTable("USER_FEDERATION_MAPPER_CONFIG");
        Delete.Table("USER_FEDERATION_MAPPER_CONFIG");

        Delete.ForeignKey("FK_FEDMAPPERPM_FEDPRV").OnTable("USER_FEDERATION_MAPPER");
        Delete.ForeignKey("FK_FEDMAPPERPM_REALM").OnTable("USER_FEDERATION_MAPPER");
        Delete.Table("USER_FEDERATION_MAPPER");

        // Remove AUTHENTICATOR_CONFIG
        Delete.Table("AUTHENTICATOR_CONFIG");

        // Remove AUTHENTICATION_EXECUTION
        Delete.ForeignKey("FK_AUTH_EXEC_FLOW").OnTable("AUTHENTICATION_EXECUTION");
        Delete.ForeignKey("FK_AUTH_EXEC_REALM").OnTable("AUTHENTICATION_EXECUTION");
        Delete.Table("AUTHENTICATION_EXECUTION");

        // Remove AUTHENTICATION_FLOW
        Delete.ForeignKey("FK_AUTH_FLOW_REALM").OnTable("AUTHENTICATION_FLOW");
        Delete.Table("AUTHENTICATION_FLOW");

        // Remove AUTHENTICATOR
        Delete.ForeignKey("FK_AUTH_REALM").OnTable("AUTHENTICATOR");
        Delete.Table("AUTHENTICATOR");

        // Remove ADMIN_EVENT_ENTITY
        Delete.Table("ADMIN_EVENT_ENTITY");
    }
}
