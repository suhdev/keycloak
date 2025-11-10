using FluentMigrator;

namespace Keycloak.Database.Migrations;

/// <summary>
/// Updates for Keycloak version 26.5.0.
/// Combines Liquibase migrations: jpa-changelog-26.5.0.xml
/// </summary>
[Migration(3, "Version 26.5.0 Updates")]
public class Migration_003_Version_26_5_0 : Migration
{
    public override void Up()
    {
        // Create index on OFFLINE_CLIENT_SESSION for client queries
        // Note: PostgreSQL supports partial indexes with WHERE clause
        if (!Schema.Table("OFFLINE_CLIENT_SESSION").Index("IDX_OFFLINE_CSS_BY_CLIENT").Exists())
        {
            Create.Index("IDX_OFFLINE_CSS_BY_CLIENT")
                .OnTable("OFFLINE_CLIENT_SESSION")
                .OnColumn("CLIENT_ID").Ascending()
                .OnColumn("OFFLINE_FLAG").Ascending();
        }

        // Create index on OFFLINE_CLIENT_SESSION for client storage provider
        if (!Schema.Table("OFFLINE_CLIENT_SESSION").Index("IDX_OFFLINE_CSS_BY_CLIENT_STORAGE_PROVIDER").Exists())
        {
            Create.Index("IDX_OFFLINE_CSS_BY_CLIENT_STORAGE_PROVIDER")
                .OnTable("OFFLINE_CLIENT_SESSION")
                .OnColumn("CLIENT_STORAGE_PROVIDER").Ascending()
                .OnColumn("EXTERNAL_CLIENT_ID").Ascending()
                .OnColumn("OFFLINE_FLAG").Ascending();
        }

        // Allow NULL values for IDENTITY_PROVIDER columns
        Alter.Table("IDENTITY_PROVIDER")
            .AlterColumn("TRUST_EMAIL").AsBoolean().Nullable();

        Alter.Table("IDENTITY_PROVIDER")
            .AlterColumn("STORE_TOKEN").AsBoolean().Nullable();

        Alter.Table("IDENTITY_PROVIDER")
            .AlterColumn("ADD_TOKEN_ROLE").AsBoolean().Nullable();

        Alter.Table("IDENTITY_PROVIDER")
            .AlterColumn("AUTHENTICATE_BY_DEFAULT").AsBoolean().Nullable();

        Alter.Table("IDENTITY_PROVIDER")
            .AlterColumn("LINK_ONLY").AsBoolean().Nullable();

        // Remove WORKFLOW_PROVIDER_ID column from WORKFLOW_STATE if it exists
        if (Schema.Table("WORKFLOW_STATE").Column("WORKFLOW_PROVIDER_ID").Exists())
        {
            // Drop the existing index
            if (Schema.Table("WORKFLOW_STATE").Index("IDX_WORKFLOW_STATE_PROVIDER").Exists())
            {
                Delete.Index("IDX_WORKFLOW_STATE_PROVIDER").OnTable("WORKFLOW_STATE");
            }

            // Create new index on RESOURCE_ID before dropping the column
            Create.Index("IDX_WORKFLOW_STATE_PROVIDER")
                .OnTable("WORKFLOW_STATE")
                .OnColumn("RESOURCE_ID").Ascending();

            // Drop the column
            Delete.Column("WORKFLOW_PROVIDER_ID").FromTable("WORKFLOW_STATE");
        }
    }

    public override void Down()
    {
        // Note: Down migrations for schema changes that lose data (like dropping columns)
        // should be used with caution. This is a skeleton implementation.

        // Restore WORKFLOW_PROVIDER_ID column (data would be lost)
        if (!Schema.Table("WORKFLOW_STATE").Column("WORKFLOW_PROVIDER_ID").Exists())
        {
            Alter.Table("WORKFLOW_STATE")
                .AddColumn("WORKFLOW_PROVIDER_ID").AsString(255).Nullable();

            // Recreate original index
            Delete.Index("IDX_WORKFLOW_STATE_PROVIDER").OnTable("WORKFLOW_STATE");
            
            Create.Index("IDX_WORKFLOW_STATE_PROVIDER")
                .OnTable("WORKFLOW_STATE")
                .OnColumn("WORKFLOW_PROVIDER_ID").Ascending();
        }

        // Restore NOT NULL constraints on IDENTITY_PROVIDER
        // (This would fail if NULL values exist)
        Alter.Table("IDENTITY_PROVIDER")
            .AlterColumn("LINK_ONLY").AsBoolean().NotNullable().WithDefaultValue(false);

        Alter.Table("IDENTITY_PROVIDER")
            .AlterColumn("AUTHENTICATE_BY_DEFAULT").AsBoolean().NotNullable().WithDefaultValue(false);

        Alter.Table("IDENTITY_PROVIDER")
            .AlterColumn("ADD_TOKEN_ROLE").AsBoolean().NotNullable().WithDefaultValue(false);

        Alter.Table("IDENTITY_PROVIDER")
            .AlterColumn("STORE_TOKEN").AsBoolean().NotNullable().WithDefaultValue(false);

        Alter.Table("IDENTITY_PROVIDER")
            .AlterColumn("TRUST_EMAIL").AsBoolean().NotNullable().WithDefaultValue(false);

        // Drop indexes
        Delete.Index("IDX_OFFLINE_CSS_BY_CLIENT_STORAGE_PROVIDER").OnTable("OFFLINE_CLIENT_SESSION");
        Delete.Index("IDX_OFFLINE_CSS_BY_CLIENT").OnTable("OFFLINE_CLIENT_SESSION");
    }
}
