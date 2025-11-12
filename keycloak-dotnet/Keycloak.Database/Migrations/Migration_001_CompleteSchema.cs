using FluentMigrator;
using System.Reflection;

namespace Keycloak.Database.Migrations;

/// <summary>
/// Complete Keycloak database schema executed from embedded SQL script.
/// The database-schema.sql file has been modified to use UUID type for all ID columns
/// instead of VARCHAR(36), and foreign keys have been updated to match.
/// </summary>
[Migration(1, "Execute Complete Keycloak Schema from SQL")]
public class Migration_001_CompleteSchema : Migration
{
    public override void Up()
    {
        // Read the embedded SQL script
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Keycloak.Database.database-schema.sql";
        
        using (var stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                throw new Exception($"Could not find embedded resource: {resourceName}");
            }
            
            using (var reader = new StreamReader(stream))
            {
                var sql = reader.ReadToEnd();
                
                // Execute the SQL script
                // FluentMigrator will handle the execution
                Execute.Sql(sql);
            }
        }
    }

    public override void Down()
    {
        // Drop all tables in reverse order
        // This is a comprehensive list of all Keycloak tables
        var tables = new[]
        {
            "workflow_state", "web_origins", "user_session_note", "user_session",
            "user_role_mapping", "user_required_action", "user_group_membership",
            "user_federation_mapper_config", "user_federation_mapper", "user_federation_provider",
            "user_federation_config", "user_entity", "user_consent_client_scope", "user_consent",
            "user_attribute", "username_login_failure", "scope_policy", "scope_mapping",
            "role_attribute", "revoked_token", "resource_uris", "resource_server_scope",
            "resource_server_resource", "resource_server_policy", "resource_server_perm_ticket",
            "resource_server", "resource_attribute", "resource_policy", "resource_scope",
            "required_action_provider", "required_action_config", "redirect_uris",
            "realm_supported_locales", "realm_smtp_config", "realm_required_credential",
            "realm_localizations", "realm_events_listeners", "realm_enabled_event_types",
            "realm_default_groups", "realm_attribute", "realm", "protocol_mapper_config",
            "protocol_mapper", "policy_config", "offline_user_session", "offline_client_session",
            "migration_model", "keycloak_role", "keycloak_group", "identity_provider_mapper",
            "identity_provider_config", "identity_provider", "group_role_mapping", "group_attribute",
            "federated_user", "federated_identity", "fed_user_role_mapping", "fed_user_required_action",
            "fed_user_group_membership", "fed_user_credential", "fed_user_consent_cl_scope",
            "fed_user_consent", "fed_user_attribute", "event_entity", "databasechangeloglock",
            "databasechangelog", "default_client_scope", "credential", "composite_role",
            "component_config", "component", "client_user_session_note", "client_session_role",
            "client_session_prot_mapper", "client_session_note", "client_session_auth_status",
            "client_session", "client_scope_role_mapping", "client_scope_client", "client_scope_attributes",
            "client_scope", "client_node_registrations", "client_initial_access", "client_identity_prov_mapping",
            "client_default_roles", "client_attributes", "client_auth_flow_bindings", "client",
            "broker_link", "authentication_flow", "authentication_execution", "authenticator_config_entry",
            "authenticator_config", "authenticator", "associated_policy", "admin_event_entity"
        };

        foreach (var table in tables)
        {
            Execute.Sql($"DROP TABLE IF EXISTS {table} CASCADE");
        }
    }
}
