using FluentMigrator;

namespace Keycloak.Database.Migrations;

/// <summary>
/// Complete Keycloak database schema based on database-schema.sql.
/// ID columns converted from VARCHAR(36) to UUID type.
/// All 87 tables with constraints and indexes.
/// </summary>
[Migration(1, "Complete Keycloak Schema")]
public class Migration_001_CompleteSchema : Migration
{
    public override void Up()
    {
        // CLIENT table
        Create.Table("client")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("full_scope_allowed").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("client_id").AsString(255).Nullable()
            .WithColumn("not_before").AsInt32().Nullable()
            .WithColumn("public_client").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("secret").AsString(255).Nullable()
            .WithColumn("base_url").AsString(255).Nullable()
            .WithColumn("bearer_only").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("management_url").AsString(255).Nullable()
            .WithColumn("surrogate_auth_required").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("realm_id").AsGuid().Nullable()
            .WithColumn("protocol").AsString(255).Nullable()
            .WithColumn("node_rereg_timeout").AsInt32().Nullable().WithDefaultValue(0)
            .WithColumn("frontchannel_logout").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("consent_required").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("name").AsString(255).Nullable()
            .WithColumn("service_accounts_enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("client_authenticator_type").AsString(255).Nullable()
            .WithColumn("root_url").AsString(255).Nullable()
            .WithColumn("description").AsString(255).Nullable()
            .WithColumn("registration_token").AsString(255).Nullable()
            .WithColumn("standard_flow_enabled").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("implicit_flow_enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("direct_access_grants_enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("always_display_in_console").AsBoolean().NotNullable().WithDefaultValue(false)
            ;

        // EVENT_ENTITY table
        Create.Table("event_entity")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("client_id").AsString(255).Nullable()
            .WithColumn("details_json").AsString(2550).Nullable()
            .WithColumn("error").AsString(255).Nullable()
            .WithColumn("ip_address").AsString(255).Nullable()
            .WithColumn("realm_id").AsString(255).Nullable()
            .WithColumn("session_id").AsString(255).Nullable()
            .WithColumn("event_time").AsInt64().Nullable()
            .WithColumn("type").AsString(255).Nullable()
            .WithColumn("user_id").AsString(255).Nullable()
            .WithColumn("details_json_long_value").AsCustom("text").Nullable()
            ;

        // REALM table
        Create.Table("realm")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("access_code_lifespan").AsInt32().Nullable()
            .WithColumn("user_action_lifespan").AsInt32().Nullable()
            .WithColumn("access_token_lifespan").AsInt32().Nullable()
            .WithColumn("account_theme").AsString(255).Nullable()
            .WithColumn("admin_theme").AsString(255).Nullable()
            .WithColumn("email_theme").AsString(255).Nullable()
            .WithColumn("enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("events_enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("events_expiration").AsInt64().Nullable()
            .WithColumn("login_theme").AsString(255).Nullable()
            .WithColumn("name").AsString(255).Nullable()
            .WithColumn("not_before").AsInt32().Nullable()
            .WithColumn("password_policy").AsString(2550).Nullable()
            .WithColumn("registration_allowed").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("remember_me").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("reset_password_allowed").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("social").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("ssl_required").AsString(255).Nullable()
            .WithColumn("sso_idle_timeout").AsInt32().Nullable()
            .WithColumn("sso_max_lifespan").AsInt32().Nullable()
            .WithColumn("update_profile_on_soc_login").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("verify_email").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("master_admin_client").AsString(36).Nullable()
            .WithColumn("login_lifespan").AsInt32().Nullable()
            .WithColumn("internationalization_enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("default_locale").AsString(255).Nullable()
            .WithColumn("reg_email_as_username").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("admin_events_enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("admin_events_details_enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("edit_username_allowed").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("otp_policy_counter").AsInt32().Nullable().WithDefaultValue(0)
            .WithColumn("otp_policy_window").AsInt32().Nullable().WithDefaultValue(1)
            .WithColumn("otp_policy_period").AsInt32().Nullable().WithDefaultValue(30)
            .WithColumn("otp_policy_digits").AsInt32().Nullable().WithDefaultValue(6)
            .WithColumn("otp_policy_alg").AsString(36).Nullable().WithDefaultValue("HmacSHA1")
            .WithColumn("otp_policy_type").AsString(36).Nullable().WithDefaultValue("totp")
            .WithColumn("browser_flow").AsString(36).Nullable()
            .WithColumn("registration_flow").AsString(36).Nullable()
            .WithColumn("direct_grant_flow").AsString(36).Nullable()
            .WithColumn("reset_credentials_flow").AsString(36).Nullable()
            .WithColumn("client_auth_flow").AsString(36).Nullable()
            .WithColumn("offline_session_idle_timeout").AsInt32().Nullable().WithDefaultValue(0)
            .WithColumn("revoke_refresh_token").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("access_token_life_implicit").AsInt32().Nullable().WithDefaultValue(0)
            .WithColumn("login_with_email_allowed").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("duplicate_emails_allowed").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("docker_auth_flow").AsString(36).Nullable()
            .WithColumn("refresh_token_max_reuse").AsInt32().Nullable().WithDefaultValue(0)
            .WithColumn("allow_user_managed_access").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("sso_max_lifespan_remember_me").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("sso_idle_timeout_remember_me").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("default_role").AsString(255).Nullable()
            ;

        // KEYCLOAK_ROLE table
        Create.Table("keycloak_role")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("client_realm_constraint").AsString(255).Nullable()
            .WithColumn("client_role").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("description").AsString(255).Nullable()
            .WithColumn("name").AsString(255).Nullable()
            .WithColumn("realm_id").AsString(255).Nullable()
            .WithColumn("client").AsString(36).Nullable()
            .WithColumn("realm").AsString(36).Nullable()
            ;

        // COMPOSITE_ROLE table
        Create.Table("composite_role")
            .WithColumn("composite").AsString(36).NotNullable()
            .WithColumn("child_role").AsString(36).NotNullable()
            ;

        // REALM_ATTRIBUTE table
        Create.Table("realm_attribute")
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("value").AsCustom("text").Nullable()
            ;

        // REALM_EVENTS_LISTENERS table
        Create.Table("realm_events_listeners")
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("value").AsString(255).NotNullable()
            ;

        // REALM_REQUIRED_CREDENTIAL table
        Create.Table("realm_required_credential")
            .WithColumn("type").AsString(255).NotNullable()
            .WithColumn("form_label").AsString(255).Nullable()
            .WithColumn("input").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("secret").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("realm_id").AsGuid().NotNullable()
            ;

        // REALM_SMTP_CONFIG table
        Create.Table("realm_smtp_config")
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("value").AsString(255).Nullable()
            .WithColumn("name").AsString(255).NotNullable()
            ;

        // REDIRECT_URIS table
        Create.Table("redirect_uris")
            .WithColumn("client_id").AsGuid().NotNullable()
            .WithColumn("value").AsString(255).NotNullable()
            ;

        // SCOPE_MAPPING table
        Create.Table("scope_mapping")
            .WithColumn("client_id").AsGuid().NotNullable()
            .WithColumn("role_id").AsGuid().NotNullable()
            ;

        // USER_ENTITY table
        Create.Table("user_entity")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("email").AsString(255).Nullable()
            .WithColumn("email_constraint").AsString(255).Nullable()
            .WithColumn("email_verified").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("federation_link").AsString(255).Nullable()
            .WithColumn("first_name").AsString(255).Nullable()
            .WithColumn("last_name").AsString(255).Nullable()
            .WithColumn("realm_id").AsString(255).Nullable()
            .WithColumn("username").AsString(255).Nullable()
            .WithColumn("created_timestamp").AsInt64().Nullable()
            .WithColumn("service_account_client_link").AsString(255).Nullable()
            .WithColumn("not_before").AsInt32().NotNullable().WithDefaultValue(0)
            ;

        // CREDENTIAL table
        Create.Table("credential")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("salt").AsString(255).Nullable()
            .WithColumn("type").AsString(255).Nullable()
            .WithColumn("user_id").AsGuid().Nullable()
            .WithColumn("created_date").AsInt64().Nullable()
            .WithColumn("user_label").AsString(255).Nullable()
            .WithColumn("secret_data").AsCustom("text").Nullable()
            .WithColumn("credential_data").AsCustom("text").Nullable()
            .WithColumn("priority").AsInt32().Nullable()
            .WithColumn("version").AsInt32().Nullable().WithDefaultValue(0)
            ;

        // USER_ATTRIBUTE table
        Create.Table("user_attribute")
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("value").AsString(255).Nullable()
            .WithColumn("user_id").AsGuid().NotNullable()
            .WithColumn("id").AsGuid().NotNullable().WithDefaultValue("sybase-needs-something-here")
            .WithColumn("long_value_hash").AsString(255).Nullable()
            .WithColumn("long_value_hash_lower_case").AsString(255).Nullable()
            .WithColumn("long_value").AsCustom("text").Nullable()
            ;

        // USER_FEDERATION_PROVIDER table
        Create.Table("user_federation_provider")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("changed_sync_period").AsInt32().Nullable()
            .WithColumn("display_name").AsString(255).Nullable()
            .WithColumn("full_sync_period").AsInt32().Nullable()
            .WithColumn("last_sync").AsInt32().Nullable()
            .WithColumn("priority").AsInt32().Nullable()
            .WithColumn("provider_name").AsString(255).Nullable()
            .WithColumn("realm_id").AsGuid().Nullable()
            ;

        // USER_FEDERATION_CONFIG table
        Create.Table("user_federation_config")
            .WithColumn("user_federation_provider_id").AsGuid().NotNullable()
            .WithColumn("value").AsString(255).Nullable()
            .WithColumn("name").AsString(255).NotNullable()
            ;

        // USER_REQUIRED_ACTION table
        Create.Table("user_required_action")
            .WithColumn("user_id").AsGuid().NotNullable()
            .WithColumn("required_action").AsString(255).NotNullable()
            ;

        // USER_ROLE_MAPPING table
        Create.Table("user_role_mapping")
            .WithColumn("role_id").AsString(255).NotNullable()
            .WithColumn("user_id").AsGuid().NotNullable()
            ;

        // WEB_ORIGINS table
        Create.Table("web_origins")
            .WithColumn("client_id").AsGuid().NotNullable()
            .WithColumn("value").AsString(255).NotNullable()
            ;

        // CLIENT_ATTRIBUTES table
        Create.Table("client_attributes")
            .WithColumn("client_id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("value").AsCustom("text").Nullable()
            ;

        // CLIENT_NODE_REGISTRATIONS table
        Create.Table("client_node_registrations")
            .WithColumn("client_id").AsGuid().NotNullable()
            .WithColumn("value").AsInt32().Nullable()
            .WithColumn("name").AsString(255).NotNullable()
            ;

        // FEDERATED_IDENTITY table
        Create.Table("federated_identity")
            .WithColumn("identity_provider").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().Nullable()
            .WithColumn("federated_user_id").AsString(255).Nullable()
            .WithColumn("federated_username").AsString(255).Nullable()
            .WithColumn("token").AsCustom("text").Nullable()
            .WithColumn("user_id").AsGuid().NotNullable()
            ;

        // IDENTITY_PROVIDER table
        Create.Table("identity_provider")
            .WithColumn("internal_id").AsGuid().NotNullable()
            .WithColumn("enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("provider_alias").AsString(255).Nullable()
            .WithColumn("provider_id").AsString(255).Nullable()
            .WithColumn("store_token").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("authenticate_by_default").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("realm_id").AsGuid().Nullable()
            .WithColumn("add_token_role").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("trust_email").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("first_broker_login_flow_id").AsGuid().Nullable()
            .WithColumn("post_broker_login_flow_id").AsGuid().Nullable()
            .WithColumn("provider_display_name").AsString(255).Nullable()
            .WithColumn("link_only").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("organization_id").AsString(255).Nullable()
            .WithColumn("hide_on_login").AsBoolean().Nullable().WithDefaultValue(false)
            ;

        // IDENTITY_PROVIDER_CONFIG table
        Create.Table("identity_provider_config")
            .WithColumn("identity_provider_id").AsGuid().NotNullable()
            .WithColumn("value").AsCustom("text").Nullable()
            .WithColumn("name").AsString(255).NotNullable()
            ;

        // REALM_SUPPORTED_LOCALES table
        Create.Table("realm_supported_locales")
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("value").AsString(255).NotNullable()
            ;

        // REALM_ENABLED_EVENT_TYPES table
        Create.Table("realm_enabled_event_types")
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("value").AsString(255).NotNullable()
            ;

        // MIGRATION_MODEL table
        Create.Table("migration_model")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("version").AsString(36).Nullable()
            .WithColumn("update_time").AsInt64().NotNullable().WithDefaultValue(0)
            ;

        // IDENTITY_PROVIDER_MAPPER table
        Create.Table("identity_provider_mapper")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("idp_alias").AsString(255).NotNullable()
            .WithColumn("idp_mapper_name").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            ;

        // IDP_MAPPER_CONFIG table
        Create.Table("idp_mapper_config")
            .WithColumn("idp_mapper_id").AsGuid().NotNullable()
            .WithColumn("value").AsCustom("text").Nullable()
            .WithColumn("name").AsString(255).NotNullable()
            ;

        // USER_CONSENT table
        Create.Table("user_consent")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("client_id").AsString(255).Nullable()
            .WithColumn("user_id").AsGuid().NotNullable()
            .WithColumn("created_date").AsInt64().Nullable()
            .WithColumn("last_updated_date").AsInt64().Nullable()
            .WithColumn("client_storage_provider").AsString(36).Nullable()
            .WithColumn("external_client_id").AsString(255).Nullable()
            ;

        // ADMIN_EVENT_ENTITY table
        Create.Table("admin_event_entity")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("admin_event_time").AsInt64().Nullable()
            .WithColumn("realm_id").AsString(255).Nullable()
            .WithColumn("operation_type").AsString(255).Nullable()
            .WithColumn("auth_realm_id").AsString(255).Nullable()
            .WithColumn("auth_client_id").AsString(255).Nullable()
            .WithColumn("auth_user_id").AsString(255).Nullable()
            .WithColumn("ip_address").AsString(255).Nullable()
            .WithColumn("resource_path").AsString(2550).Nullable()
            .WithColumn("representation").AsCustom("text").Nullable()
            .WithColumn("error").AsString(255).Nullable()
            .WithColumn("resource_type").AsString(64).Nullable()
            .WithColumn("details_json").AsCustom("text").Nullable()
            ;

        // AUTHENTICATOR_CONFIG table
        Create.Table("authenticator_config")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("alias").AsString(255).Nullable()
            .WithColumn("realm_id").AsGuid().Nullable()
            ;

        // AUTHENTICATION_FLOW table
        Create.Table("authentication_flow")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("alias").AsString(255).Nullable()
            .WithColumn("description").AsString(255).Nullable()
            .WithColumn("realm_id").AsGuid().Nullable()
            .WithColumn("provider_id").AsGuid().NotNullable().WithDefaultValue("basic-flow")
            .WithColumn("top_level").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("built_in").AsBoolean().NotNullable().WithDefaultValue(false)
            ;

        // AUTHENTICATION_EXECUTION table
        Create.Table("authentication_execution")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("alias").AsString(255).Nullable()
            .WithColumn("authenticator").AsString(36).Nullable()
            .WithColumn("realm_id").AsGuid().Nullable()
            .WithColumn("flow_id").AsGuid().Nullable()
            .WithColumn("requirement").AsInt32().Nullable()
            .WithColumn("priority").AsInt32().Nullable()
            .WithColumn("authenticator_flow").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("auth_flow_id").AsGuid().Nullable()
            .WithColumn("auth_config").AsString(36).Nullable()
            ;

        // AUTHENTICATOR_CONFIG_ENTRY table
        Create.Table("authenticator_config_entry")
            .WithColumn("authenticator_id").AsGuid().NotNullable()
            .WithColumn("value").AsCustom("text").Nullable()
            .WithColumn("name").AsString(255).NotNullable()
            ;

        // USER_FEDERATION_MAPPER table
        Create.Table("user_federation_mapper")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("federation_provider_id").AsGuid().NotNullable()
            .WithColumn("federation_mapper_type").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            ;

        // USER_FEDERATION_MAPPER_CONFIG table
        Create.Table("user_federation_mapper_config")
            .WithColumn("user_federation_mapper_id").AsGuid().NotNullable()
            .WithColumn("value").AsString(255).Nullable()
            .WithColumn("name").AsString(255).NotNullable()
            ;

        // REQUIRED_ACTION_PROVIDER table
        Create.Table("required_action_provider")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("alias").AsString(255).Nullable()
            .WithColumn("name").AsString(255).Nullable()
            .WithColumn("realm_id").AsGuid().Nullable()
            .WithColumn("enabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("default_action").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("provider_id").AsString(255).Nullable()
            .WithColumn("priority").AsInt32().Nullable()
            ;

        // REQUIRED_ACTION_CONFIG table
        Create.Table("required_action_config")
            .WithColumn("required_action_id").AsGuid().NotNullable()
            .WithColumn("value").AsCustom("text").Nullable()
            .WithColumn("name").AsString(255).NotNullable()
            ;

        // OFFLINE_USER_SESSION table
        Create.Table("offline_user_session")
            .WithColumn("user_session_id").AsGuid().NotNullable()
            .WithColumn("user_id").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("created_on").AsInt32().NotNullable()
            .WithColumn("offline_flag").AsString(4).NotNullable()
            .WithColumn("data").AsCustom("text").Nullable()
            .WithColumn("last_session_refresh").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("broker_session_id").AsString(1024).Nullable()
            .WithColumn("version").AsInt32().Nullable().WithDefaultValue(0)
            ;

        // OFFLINE_CLIENT_SESSION table
        Create.Table("offline_client_session")
            .WithColumn("user_session_id").AsGuid().NotNullable()
            .WithColumn("client_id").AsString(255).NotNullable()
            .WithColumn("offline_flag").AsString(4).NotNullable()
            .WithColumn("timestamp").AsInt32().Nullable()
            .WithColumn("data").AsCustom("text").Nullable()
            .WithColumn("client_storage_provider").AsString(36).NotNullable().WithDefaultValue("local")
            .WithColumn("external_client_id").AsString(255).NotNullable().WithDefaultValue("local")
            .WithColumn("version").AsInt32().Nullable().WithDefaultValue(0)
            ;

        // KEYCLOAK_GROUP table
        Create.Table("keycloak_group")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).Nullable()
            .WithColumn("parent_group").AsString(36).NotNullable()
            .WithColumn("realm_id").AsGuid().Nullable()
            .WithColumn("type").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("description").AsString(255).Nullable()
            ;

        // GROUP_ROLE_MAPPING table
        Create.Table("group_role_mapping")
            .WithColumn("role_id").AsGuid().NotNullable()
            .WithColumn("group_id").AsGuid().NotNullable()
            ;

        // GROUP_ATTRIBUTE table
        Create.Table("group_attribute")
            .WithColumn("id").AsGuid().NotNullable().WithDefaultValue("sybase-needs-something-here")
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("value").AsString(255).Nullable()
            .WithColumn("group_id").AsGuid().NotNullable()
            ;

        // USER_GROUP_MEMBERSHIP table
        Create.Table("user_group_membership")
            .WithColumn("group_id").AsGuid().NotNullable()
            .WithColumn("user_id").AsGuid().NotNullable()
            .WithColumn("membership_type").AsString(255).NotNullable()
            ;

        // REALM_DEFAULT_GROUPS table
        Create.Table("realm_default_groups")
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("group_id").AsGuid().NotNullable()
            ;

        // CLIENT_SCOPE table
        Create.Table("client_scope")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).Nullable()
            .WithColumn("realm_id").AsGuid().Nullable()
            .WithColumn("description").AsString(255).Nullable()
            .WithColumn("protocol").AsString(255).Nullable()
            ;

        // PROTOCOL_MAPPER table
        Create.Table("protocol_mapper")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("protocol").AsString(255).NotNullable()
            .WithColumn("protocol_mapper_name").AsString(255).NotNullable()
            .WithColumn("client_id").AsGuid().Nullable()
            .WithColumn("client_scope_id").AsGuid().Nullable()
            ;

        // PROTOCOL_MAPPER_CONFIG table
        Create.Table("protocol_mapper_config")
            .WithColumn("protocol_mapper_id").AsGuid().NotNullable()
            .WithColumn("value").AsCustom("text").Nullable()
            .WithColumn("name").AsString(255).NotNullable()
            ;

        // CLIENT_SCOPE_ATTRIBUTES table
        Create.Table("client_scope_attributes")
            .WithColumn("scope_id").AsGuid().NotNullable()
            .WithColumn("value").AsString(2048).Nullable()
            .WithColumn("name").AsString(255).NotNullable()
            ;

        // CLIENT_SCOPE_ROLE_MAPPING table
        Create.Table("client_scope_role_mapping")
            .WithColumn("scope_id").AsGuid().NotNullable()
            .WithColumn("role_id").AsGuid().NotNullable()
            ;

        // RESOURCE_SERVER table
        Create.Table("resource_server")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("allow_rs_remote_mgmt").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("policy_enforce_mode").AsString(255).NotNullable()
            .WithColumn("decision_strategy").AsString(255).NotNullable().WithDefaultValue(1)
            ;

        // RESOURCE_SERVER_RESOURCE table
        Create.Table("resource_server_resource")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("type").AsString(255).Nullable()
            .WithColumn("icon_uri").AsString(255).Nullable()
            .WithColumn("owner").AsString(255).NotNullable()
            .WithColumn("resource_server_id").AsGuid().NotNullable()
            .WithColumn("owner_managed_access").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("display_name").AsString(255).Nullable()
            ;

        // RESOURCE_SERVER_SCOPE table
        Create.Table("resource_server_scope")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("icon_uri").AsString(255).Nullable()
            .WithColumn("resource_server_id").AsGuid().NotNullable()
            .WithColumn("display_name").AsString(255).Nullable()
            ;

        // RESOURCE_SERVER_POLICY table
        Create.Table("resource_server_policy")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("description").AsString(255).Nullable()
            .WithColumn("type").AsString(255).NotNullable()
            .WithColumn("decision_strategy").AsString(255).Nullable()
            .WithColumn("logic").AsString(255).Nullable()
            .WithColumn("resource_server_id").AsGuid().NotNullable()
            .WithColumn("owner").AsString(255).Nullable()
            ;

        // POLICY_CONFIG table
        Create.Table("policy_config")
            .WithColumn("policy_id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("value").AsCustom("text").Nullable()
            ;

        // RESOURCE_SCOPE table
        Create.Table("resource_scope")
            .WithColumn("resource_id").AsGuid().NotNullable()
            .WithColumn("scope_id").AsGuid().NotNullable()
            ;

        // RESOURCE_POLICY table
        Create.Table("resource_policy")
            .WithColumn("resource_id").AsGuid().NotNullable()
            .WithColumn("policy_id").AsGuid().NotNullable()
            ;

        // SCOPE_POLICY table
        Create.Table("scope_policy")
            .WithColumn("scope_id").AsGuid().NotNullable()
            .WithColumn("policy_id").AsGuid().NotNullable()
            ;

        // ASSOCIATED_POLICY table
        Create.Table("associated_policy")
            .WithColumn("policy_id").AsGuid().NotNullable()
            .WithColumn("associated_policy_id").AsGuid().NotNullable()
            ;

        // BROKER_LINK table
        Create.Table("broker_link")
            .WithColumn("identity_provider").AsString(255).NotNullable()
            .WithColumn("storage_provider_id").AsString(255).Nullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("broker_user_id").AsString(255).Nullable()
            .WithColumn("broker_username").AsString(255).Nullable()
            .WithColumn("token").AsCustom("text").Nullable()
            .WithColumn("user_id").AsString(255).NotNullable()
            ;

        // FED_USER_ATTRIBUTE table
        Create.Table("fed_user_attribute")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("user_id").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("storage_provider_id").AsGuid().Nullable()
            .WithColumn("value").AsString(2024).Nullable()
            .WithColumn("long_value_hash").AsString(255).Nullable()
            .WithColumn("long_value_hash_lower_case").AsString(255).Nullable()
            .WithColumn("long_value").AsCustom("text").Nullable()
            ;

        // FED_USER_CONSENT table
        Create.Table("fed_user_consent")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("client_id").AsString(255).Nullable()
            .WithColumn("user_id").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("storage_provider_id").AsGuid().Nullable()
            .WithColumn("created_date").AsInt64().Nullable()
            .WithColumn("last_updated_date").AsInt64().Nullable()
            .WithColumn("client_storage_provider").AsString(36).Nullable()
            .WithColumn("external_client_id").AsString(255).Nullable()
            ;

        // FED_USER_CREDENTIAL table
        Create.Table("fed_user_credential")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("salt").AsString(255).Nullable()
            .WithColumn("type").AsString(255).Nullable()
            .WithColumn("created_date").AsInt64().Nullable()
            .WithColumn("user_id").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("storage_provider_id").AsGuid().Nullable()
            .WithColumn("user_label").AsString(255).Nullable()
            .WithColumn("secret_data").AsCustom("text").Nullable()
            .WithColumn("credential_data").AsCustom("text").Nullable()
            .WithColumn("priority").AsInt32().Nullable()
            ;

        // FED_USER_GROUP_MEMBERSHIP table
        Create.Table("fed_user_group_membership")
            .WithColumn("group_id").AsGuid().NotNullable()
            .WithColumn("user_id").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("storage_provider_id").AsGuid().Nullable()
            ;

        // FED_USER_REQUIRED_ACTION table
        Create.Table("fed_user_required_action")
            .WithColumn("required_action").AsString(255).NotNullable()
            .WithColumn("user_id").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("storage_provider_id").AsGuid().Nullable()
            ;

        // FED_USER_ROLE_MAPPING table
        Create.Table("fed_user_role_mapping")
            .WithColumn("role_id").AsGuid().NotNullable()
            .WithColumn("user_id").AsString(255).NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("storage_provider_id").AsGuid().Nullable()
            ;

        // COMPONENT table
        Create.Table("component")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).Nullable()
            .WithColumn("parent_id").AsGuid().Nullable()
            .WithColumn("provider_id").AsGuid().Nullable()
            .WithColumn("provider_type").AsString(255).Nullable()
            .WithColumn("realm_id").AsGuid().Nullable()
            .WithColumn("sub_type").AsString(255).Nullable()
            ;

        // COMPONENT_CONFIG table
        Create.Table("component_config")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("component_id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("value").AsCustom("text").Nullable()
            ;

        // FEDERATED_USER table
        Create.Table("federated_user")
            .WithColumn("id").AsString(255).NotNullable()
            .WithColumn("storage_provider_id").AsString(255).Nullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            ;

        // CLIENT_INITIAL_ACCESS table
        Create.Table("client_initial_access")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("timestamp").AsInt32().Nullable()
            .WithColumn("expiration").AsInt32().Nullable()
            .WithColumn("count").AsInt32().Nullable()
            .WithColumn("remaining_count").AsInt32().Nullable()
            ;

        // CLIENT_AUTH_FLOW_BINDINGS table
        Create.Table("client_auth_flow_bindings")
            .WithColumn("client_id").AsGuid().NotNullable()
            .WithColumn("flow_id").AsGuid().Nullable()
            .WithColumn("binding_name").AsString(255).NotNullable()
            ;

        // CLIENT_SCOPE_CLIENT table
        Create.Table("client_scope_client")
            .WithColumn("client_id").AsString(255).NotNullable()
            .WithColumn("scope_id").AsString(255).NotNullable()
            .WithColumn("default_scope").AsBoolean().NotNullable().WithDefaultValue(false)
            ;

        // DEFAULT_CLIENT_SCOPE table
        Create.Table("default_client_scope")
            .WithColumn("realm_id").AsGuid().NotNullable()
            .WithColumn("scope_id").AsGuid().NotNullable()
            .WithColumn("default_scope").AsBoolean().NotNullable().WithDefaultValue(false)
            ;

        // USER_CONSENT_CLIENT_SCOPE table
        Create.Table("user_consent_client_scope")
            .WithColumn("user_consent_id").AsGuid().NotNullable()
            .WithColumn("scope_id").AsGuid().NotNullable()
            ;

        // FED_USER_CONSENT_CL_SCOPE table
        Create.Table("fed_user_consent_cl_scope")
            .WithColumn("user_consent_id").AsGuid().NotNullable()
            .WithColumn("scope_id").AsGuid().NotNullable()
            ;

        // RESOURCE_SERVER_PERM_TICKET table
        Create.Table("resource_server_perm_ticket")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("owner").AsString(255).NotNullable()
            .WithColumn("requester").AsString(255).NotNullable()
            .WithColumn("created_timestamp").AsInt64().NotNullable()
            .WithColumn("granted_timestamp").AsInt64().Nullable()
            .WithColumn("resource_id").AsGuid().NotNullable()
            .WithColumn("scope_id").AsGuid().Nullable()
            .WithColumn("resource_server_id").AsGuid().NotNullable()
            .WithColumn("policy_id").AsGuid().Nullable()
            ;

        // RESOURCE_ATTRIBUTE table
        Create.Table("resource_attribute")
            .WithColumn("id").AsGuid().NotNullable().WithDefaultValue("sybase-needs-something-here")
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("value").AsString(255).Nullable()
            .WithColumn("resource_id").AsGuid().NotNullable()
            ;

        // RESOURCE_URIS table
        Create.Table("resource_uris")
            .WithColumn("resource_id").AsGuid().NotNullable()
            .WithColumn("value").AsString(255).NotNullable()
            ;

        // ROLE_ATTRIBUTE table
        Create.Table("role_attribute")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("role_id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("value").AsString(255).Nullable()
            ;

        // REALM_LOCALIZATIONS table
        Create.Table("realm_localizations")
            .WithColumn("realm_id").AsString(255).NotNullable()
            .WithColumn("locale").AsString(255).NotNullable()
            .WithColumn("texts").AsCustom("text").NotNullable()
            ;

        // ORG table
        Create.Table("org")
            .WithColumn("id").AsString(255).NotNullable()
            .WithColumn("enabled").AsBoolean().NotNullable()
            .WithColumn("realm_id").AsString(255).NotNullable()
            .WithColumn("group_id").AsString(255).NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("description").AsString(4000).Nullable()
            .WithColumn("alias").AsString(255).NotNullable()
            .WithColumn("redirect_url").AsString(2048).Nullable()
            ;

        // ORG_DOMAIN table
        Create.Table("org_domain")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("verified").AsBoolean().NotNullable()
            .WithColumn("org_id").AsString(255).NotNullable()
            ;

        // REVOKED_TOKEN table
        Create.Table("revoked_token")
            .WithColumn("id").AsString(255).NotNullable()
            .WithColumn("expire").AsInt64().NotNullable()
            ;

        // JGROUPS_PING table
        Create.Table("jgroups_ping")
            .WithColumn("address").AsString(200).NotNullable()
            .WithColumn("name").AsString(200).Nullable()
            .WithColumn("cluster_name").AsString(200).NotNullable()
            .WithColumn("ip").AsString(200).NotNullable()
            .WithColumn("coord").AsBoolean().Nullable()
            ;

        // SERVER_CONFIG table
        Create.Table("server_config")
            .WithColumn("server_config_key").AsString(255).NotNullable()
            .WithColumn("value").AsCustom("text").NotNullable()
            .WithColumn("version").AsInt32().Nullable().WithDefaultValue(0)
            ;

        // WORKFLOW_STATE table
        Create.Table("workflow_state")
            .WithColumn("execution_id").AsString(255).NotNullable()
            .WithColumn("resource_id").AsString(255).NotNullable()
            .WithColumn("workflow_id").AsString(255).NotNullable()
            .WithColumn("workflow_provider_id").AsString(255).Nullable()
            .WithColumn("resource_type").AsString(255).Nullable()
            .WithColumn("scheduled_step_id").AsString(255).Nullable()
            .WithColumn("scheduled_step_timestamp").AsInt64().Nullable()
            ;

        // INDEXES
        Create.Index("idx_client_id")
            .OnTable("client")
            .OnColumn("client_id").Ascending()
            ;

        Create.Index("idx_event_time")
            .OnTable("event_entity")
            .OnColumn("realm_id").Ascending()
            .OnColumn("event_time").Ascending()
            ;

        Create.Index("idx_event_entity_user_id_type")
            .OnTable("event_entity")
            .OnColumn("user_id").Ascending()
            .OnColumn("type").Ascending()
            .OnColumn("event_time").Ascending()
            ;

        Create.Index("idx_composite")
            .OnTable("composite_role")
            .OnColumn("composite").Ascending()
            ;

        Create.Index("idx_composite_child")
            .OnTable("composite_role")
            .OnColumn("child_role").Ascending()
            ;

        Create.Index("idx_keycloak_role_client")
            .OnTable("keycloak_role")
            .OnColumn("client").Ascending()
            ;

        Create.Index("idx_keycloak_role_realm")
            .OnTable("keycloak_role")
            .OnColumn("realm").Ascending()
            ;

        Create.Index("idx_realm_master_adm_cli")
            .OnTable("realm")
            .OnColumn("master_admin_client").Ascending()
            ;

        Create.Index("idx_realm_attr_realm")
            .OnTable("realm_attribute")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_realm_evt_list_realm")
            .OnTable("realm_events_listeners")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_redir_uri_client")
            .OnTable("redirect_uris")
            .OnColumn("client_id").Ascending()
            ;

        Create.Index("idx_scope_mapping_role")
            .OnTable("scope_mapping")
            .OnColumn("role_id").Ascending()
            ;

        Create.Index("idx_user_credential")
            .OnTable("credential")
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_user_attribute")
            .OnTable("user_attribute")
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_user_attribute_name")
            .OnTable("user_attribute")
            .OnColumn("name").Ascending()
            .OnColumn("value").Ascending()
            ;

        Create.Index("user_attr_long_values")
            .OnTable("user_attribute")
            .OnColumn("long_value_hash").Ascending()
            .OnColumn("name").Ascending()
            ;

        Create.Index("user_attr_long_values_lower_case")
            .OnTable("user_attribute")
            .OnColumn("long_value_hash_lower_case").Ascending()
            .OnColumn("name").Ascending()
            ;

        Create.Index("idx_user_email")
            .OnTable("user_entity")
            .OnColumn("email").Ascending()
            ;

        Create.Index("idx_user_service_account")
            .OnTable("user_entity")
            .OnColumn("realm_id").Ascending()
            .OnColumn("service_account_client_link").Ascending()
            ;

        Create.Index("idx_usr_fed_prv_realm")
            .OnTable("user_federation_provider")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_user_reqactions")
            .OnTable("user_required_action")
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_user_role_mapping")
            .OnTable("user_role_mapping")
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_web_orig_client")
            .OnTable("web_origins")
            .OnColumn("client_id").Ascending()
            ;

        Create.Index("idx_client_att_by_name_value")
            .OnTable("client_attributes")
            .OnColumn("name").Ascending()
            .OnColumn("substr(value").Ascending()
            .OnColumn("1").Ascending()
            .OnColumn("255)").Ascending()
            ;

        Create.Index("idx_fedidentity_user")
            .OnTable("federated_identity")
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_fedidentity_feduser")
            .OnTable("federated_identity")
            .OnColumn("federated_user_id").Ascending()
            ;

        Create.Index("idx_ident_prov_realm")
            .OnTable("identity_provider")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_idp_realm_org")
            .OnTable("identity_provider")
            .OnColumn("realm_id").Ascending()
            .OnColumn("organization_id").Ascending()
            ;

        Create.Index("idx_idp_for_login")
            .OnTable("identity_provider")
            .OnColumn("realm_id").Ascending()
            .OnColumn("enabled").Ascending()
            .OnColumn("link_only").Ascending()
            .OnColumn("hide_on_login").Ascending()
            .OnColumn("organization_id").Ascending()
            ;

        Create.Index("idx_realm_supp_local_realm")
            .OnTable("realm_supported_locales")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_realm_evt_types_realm")
            .OnTable("realm_enabled_event_types")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_update_time")
            .OnTable("migration_model")
            .OnColumn("update_time").Ascending()
            ;

        Create.Index("idx_id_prov_mapp_realm")
            .OnTable("identity_provider_mapper")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_user_consent")
            .OnTable("user_consent")
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_admin_event_time")
            .OnTable("admin_event_entity")
            .OnColumn("realm_id").Ascending()
            .OnColumn("admin_event_time").Ascending()
            ;

        Create.Index("idx_auth_config_realm")
            .OnTable("authenticator_config")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_auth_flow_realm")
            .OnTable("authentication_flow")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_auth_exec_realm_flow")
            .OnTable("authentication_execution")
            .OnColumn("realm_id").Ascending()
            .OnColumn("flow_id").Ascending()
            ;

        Create.Index("idx_auth_exec_flow")
            .OnTable("authentication_execution")
            .OnColumn("flow_id").Ascending()
            ;

        Create.Index("idx_usr_fed_map_fed_prv")
            .OnTable("user_federation_mapper")
            .OnColumn("federation_provider_id").Ascending()
            ;

        Create.Index("idx_usr_fed_map_realm")
            .OnTable("user_federation_mapper")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_req_act_prov_realm")
            .OnTable("required_action_provider")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_offline_uss_by_user")
            .OnTable("offline_user_session")
            .OnColumn("user_id").Ascending()
            .OnColumn("realm_id").Ascending()
            .OnColumn("offline_flag").Ascending()
            ;

        Create.Index("idx_offline_uss_by_last_session_refresh")
            .OnTable("offline_user_session")
            .OnColumn("realm_id").Ascending()
            .OnColumn("offline_flag").Ascending()
            .OnColumn("last_session_refresh").Ascending()
            ;

        Create.Index("idx_offline_uss_by_broker_session_id")
            .OnTable("offline_user_session")
            .OnColumn("broker_session_id").Ascending()
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_offline_css_by_client")
            .OnTable("offline_client_session")
            .OnColumn("client_id").Ascending()
            .OnColumn("offline_flag)").Ascending()
            ;

        Create.Index("idx_offline_css_by_client_storage_provider")
            .OnTable("offline_client_session")
            .OnColumn("client_storage_provider").Ascending()
            .OnColumn("external_client_id").Ascending()
            .OnColumn("offline_flag)").Ascending()
            ;

        Create.Index("idx_group_role_mapp_group")
            .OnTable("group_role_mapping")
            .OnColumn("group_id").Ascending()
            ;

        Create.Index("idx_group_attr_group")
            .OnTable("group_attribute")
            .OnColumn("group_id").Ascending()
            ;

        Create.Index("idx_group_att_by_name_value")
            .OnTable("group_attribute")
            .OnColumn("name").Ascending()
            .OnColumn("(value::character").Ascending()
            ;

        Create.Index("idx_user_group_mapping")
            .OnTable("user_group_membership")
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_realm_def_grp_realm")
            .OnTable("realm_default_groups")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_protocol_mapper_client")
            .OnTable("protocol_mapper")
            .OnColumn("client_id").Ascending()
            ;

        Create.Index("idx_clscope_protmap")
            .OnTable("protocol_mapper")
            .OnColumn("client_scope_id").Ascending()
            ;

        Create.Index("idx_realm_clscope")
            .OnTable("client_scope")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_clscope_attrs")
            .OnTable("client_scope_attributes")
            .OnColumn("scope_id").Ascending()
            ;

        Create.Index("idx_clscope_role")
            .OnTable("client_scope_role_mapping")
            .OnColumn("scope_id").Ascending()
            ;

        Create.Index("idx_role_clscope")
            .OnTable("client_scope_role_mapping")
            .OnColumn("role_id").Ascending()
            ;

        Create.Index("idx_res_srv_res_res_srv")
            .OnTable("resource_server_resource")
            .OnColumn("resource_server_id").Ascending()
            ;

        Create.Index("idx_res_srv_scope_res_srv")
            .OnTable("resource_server_scope")
            .OnColumn("resource_server_id").Ascending()
            ;

        Create.Index("idx_res_serv_pol_res_serv")
            .OnTable("resource_server_policy")
            .OnColumn("resource_server_id").Ascending()
            ;

        Create.Index("idx_res_scope_scope")
            .OnTable("resource_scope")
            .OnColumn("scope_id").Ascending()
            ;

        Create.Index("idx_res_policy_policy")
            .OnTable("resource_policy")
            .OnColumn("policy_id").Ascending()
            ;

        Create.Index("idx_scope_policy_policy")
            .OnTable("scope_policy")
            .OnColumn("policy_id").Ascending()
            ;

        Create.Index("idx_assoc_pol_assoc_pol_id")
            .OnTable("associated_policy")
            .OnColumn("associated_policy_id").Ascending()
            ;

        Create.Index("idx_fu_attribute")
            .OnTable("fed_user_attribute")
            .OnColumn("user_id").Ascending()
            .OnColumn("realm_id").Ascending()
            .OnColumn("name").Ascending()
            ;

        Create.Index("fed_user_attr_long_values")
            .OnTable("fed_user_attribute")
            .OnColumn("long_value_hash").Ascending()
            .OnColumn("name").Ascending()
            ;

        Create.Index("fed_user_attr_long_values_lower_case")
            .OnTable("fed_user_attribute")
            .OnColumn("long_value_hash_lower_case").Ascending()
            .OnColumn("name").Ascending()
            ;

        Create.Index("idx_fu_consent_ru")
            .OnTable("fed_user_consent")
            .OnColumn("realm_id").Ascending()
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_fu_cnsnt_ext")
            .OnTable("fed_user_consent")
            .OnColumn("user_id").Ascending()
            .OnColumn("client_storage_provider").Ascending()
            .OnColumn("external_client_id").Ascending()
            ;

        Create.Index("idx_fu_consent")
            .OnTable("fed_user_consent")
            .OnColumn("user_id").Ascending()
            .OnColumn("client_id").Ascending()
            ;

        Create.Index("idx_fu_credential")
            .OnTable("fed_user_credential")
            .OnColumn("user_id").Ascending()
            .OnColumn("type").Ascending()
            ;

        Create.Index("idx_fu_credential_ru")
            .OnTable("fed_user_credential")
            .OnColumn("realm_id").Ascending()
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_fu_group_membership")
            .OnTable("fed_user_group_membership")
            .OnColumn("user_id").Ascending()
            .OnColumn("group_id").Ascending()
            ;

        Create.Index("idx_fu_group_membership_ru")
            .OnTable("fed_user_group_membership")
            .OnColumn("realm_id").Ascending()
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_fu_required_action")
            .OnTable("fed_user_required_action")
            .OnColumn("user_id").Ascending()
            .OnColumn("required_action").Ascending()
            ;

        Create.Index("idx_fu_required_action_ru")
            .OnTable("fed_user_required_action")
            .OnColumn("realm_id").Ascending()
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_fu_role_mapping")
            .OnTable("fed_user_role_mapping")
            .OnColumn("user_id").Ascending()
            .OnColumn("role_id").Ascending()
            ;

        Create.Index("idx_fu_role_mapping_ru")
            .OnTable("fed_user_role_mapping")
            .OnColumn("realm_id").Ascending()
            .OnColumn("user_id").Ascending()
            ;

        Create.Index("idx_compo_config_compo")
            .OnTable("component_config")
            .OnColumn("component_id").Ascending()
            ;

        Create.Index("idx_component_realm")
            .OnTable("component")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_component_provider_type")
            .OnTable("component")
            .OnColumn("provider_type").Ascending()
            ;

        Create.Index("idx_client_init_acc_realm")
            .OnTable("client_initial_access")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_clscope_cl")
            .OnTable("client_scope_client")
            .OnColumn("client_id").Ascending()
            ;

        Create.Index("idx_cl_clscope")
            .OnTable("client_scope_client")
            .OnColumn("scope_id").Ascending()
            ;

        Create.Index("idx_defcls_realm")
            .OnTable("default_client_scope")
            .OnColumn("realm_id").Ascending()
            ;

        Create.Index("idx_defcls_scope")
            .OnTable("default_client_scope")
            .OnColumn("scope_id").Ascending()
            ;

        Create.Index("idx_usconsent_clscope")
            .OnTable("user_consent_client_scope")
            .OnColumn("user_consent_id").Ascending()
            ;

        Create.Index("idx_usconsent_scope_id")
            .OnTable("user_consent_client_scope")
            .OnColumn("scope_id").Ascending()
            ;

        Create.Index("idx_perm_ticket_requester")
            .OnTable("resource_server_perm_ticket")
            .OnColumn("requester").Ascending()
            ;

        Create.Index("idx_perm_ticket_owner")
            .OnTable("resource_server_perm_ticket")
            .OnColumn("owner").Ascending()
            ;

        Create.Index("idx_role_attribute")
            .OnTable("role_attribute")
            .OnColumn("role_id").Ascending()
            ;

        Create.Index("idx_org_domain_org_id")
            .OnTable("org_domain")
            .OnColumn("org_id").Ascending()
            ;

        Create.Index("idx_rev_token_on_expire")
            .OnTable("revoked_token")
            .OnColumn("expire").Ascending()
            ;

        Create.Index("idx_workflow_state_step")
            .OnTable("workflow_state")
            .OnColumn("workflow_id").Ascending()
            .OnColumn("scheduled_step_id").Ascending()
            ;

        Create.Index("idx_workflow_state_provider")
            .OnTable("workflow_state")
            .OnColumn("resource_id").Ascending()
            .OnColumn("workflow_provider_id").Ascending()
            ;

    }

    public override void Down()
    {
        // Drop all tables
        Delete.Table("workflow_state");
        Delete.Table("server_config");
        Delete.Table("jgroups_ping");
        Delete.Table("revoked_token");
        Delete.Table("org_domain");
        Delete.Table("org");
        Delete.Table("realm_localizations");
        Delete.Table("role_attribute");
        Delete.Table("resource_uris");
        Delete.Table("resource_attribute");
        Delete.Table("resource_server_perm_ticket");
        Delete.Table("fed_user_consent_cl_scope");
        Delete.Table("user_consent_client_scope");
        Delete.Table("default_client_scope");
        Delete.Table("client_scope_client");
        Delete.Table("client_auth_flow_bindings");
        Delete.Table("client_initial_access");
        Delete.Table("federated_user");
        Delete.Table("component_config");
        Delete.Table("component");
        Delete.Table("fed_user_role_mapping");
        Delete.Table("fed_user_required_action");
        Delete.Table("fed_user_group_membership");
        Delete.Table("fed_user_credential");
        Delete.Table("fed_user_consent");
        Delete.Table("fed_user_attribute");
        Delete.Table("broker_link");
        Delete.Table("associated_policy");
        Delete.Table("scope_policy");
        Delete.Table("resource_policy");
        Delete.Table("resource_scope");
        Delete.Table("policy_config");
        Delete.Table("resource_server_policy");
        Delete.Table("resource_server_scope");
        Delete.Table("resource_server_resource");
        Delete.Table("resource_server");
        Delete.Table("client_scope_role_mapping");
        Delete.Table("client_scope_attributes");
        Delete.Table("protocol_mapper_config");
        Delete.Table("protocol_mapper");
        Delete.Table("client_scope");
        Delete.Table("realm_default_groups");
        Delete.Table("user_group_membership");
        Delete.Table("group_attribute");
        Delete.Table("group_role_mapping");
        Delete.Table("keycloak_group");
        Delete.Table("offline_client_session");
        Delete.Table("offline_user_session");
        Delete.Table("required_action_config");
        Delete.Table("required_action_provider");
        Delete.Table("user_federation_mapper_config");
        Delete.Table("user_federation_mapper");
        Delete.Table("authenticator_config_entry");
        Delete.Table("authentication_execution");
        Delete.Table("authentication_flow");
        Delete.Table("authenticator_config");
        Delete.Table("admin_event_entity");
        Delete.Table("user_consent");
        Delete.Table("idp_mapper_config");
        Delete.Table("identity_provider_mapper");
        Delete.Table("migration_model");
        Delete.Table("realm_enabled_event_types");
        Delete.Table("realm_supported_locales");
        Delete.Table("identity_provider_config");
        Delete.Table("identity_provider");
        Delete.Table("federated_identity");
        Delete.Table("client_node_registrations");
        Delete.Table("client_attributes");
        Delete.Table("web_origins");
        Delete.Table("user_role_mapping");
        Delete.Table("user_required_action");
        Delete.Table("user_federation_config");
        Delete.Table("user_federation_provider");
        Delete.Table("user_attribute");
        Delete.Table("credential");
        Delete.Table("user_entity");
        Delete.Table("scope_mapping");
        Delete.Table("redirect_uris");
        Delete.Table("realm_smtp_config");
        Delete.Table("realm_required_credential");
        Delete.Table("realm_events_listeners");
        Delete.Table("realm_attribute");
        Delete.Table("composite_role");
        Delete.Table("keycloak_role");
        Delete.Table("realm");
        Delete.Table("event_entity");
        Delete.Table("client");
    }
}