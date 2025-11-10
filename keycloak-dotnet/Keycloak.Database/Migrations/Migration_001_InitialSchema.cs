using FluentMigrator;

namespace Keycloak.Database.Migrations;

/// <summary>
/// Initial database schema for Keycloak.
/// Combines Liquibase migrations: jpa-changelog-1.0.0.Final.xml
/// </summary>
[Migration(1, "Initial Schema - v1.0.0.Final")]
public class Migration_001_InitialSchema : Migration
{
    public override void Up()
    {
        // APPLICATION_DEFAULT_ROLES
        Create.Table("APPLICATION_DEFAULT_ROLES")
            .WithColumn("APPLICATION_ID").AsString(36).NotNullable()
            .WithColumn("ROLE_ID").AsString(36).NotNullable();

        // CLIENT
        Create.Table("CLIENT")
            .WithColumn("DTYPE").AsString(31).NotNullable()
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_7")
            .WithColumn("ALLOWED_CLAIMS_MASK").AsInt64().Nullable()
            .WithColumn("ENABLED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("FULL_SCOPE_ALLOWED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("NAME").AsString(255).Nullable()
            .WithColumn("NOT_BEFORE").AsInt32().Nullable()
            .WithColumn("PUBLIC_CLIENT").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("SECRET").AsString(255).Nullable()
            .WithColumn("BASE_URL").AsString(255).Nullable()
            .WithColumn("BEARER_ONLY").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("MANAGEMENT_URL").AsString(255).Nullable()
            .WithColumn("SURROGATE_AUTH_REQUIRED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("DIRECT_GRANTS_ONLY").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("REALM_ID").AsString(36).Nullable();

        // CLIENT_SESSION
        Create.Table("CLIENT_SESSION")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_8")
            .WithColumn("ACTION").AsInt32().Nullable()
            .WithColumn("CLIENT_ID").AsString(36).Nullable()
            .WithColumn("REDIRECT_URI").AsString(255).Nullable()
            .WithColumn("STATE").AsString(255).Nullable()
            .WithColumn("TIMESTAMP").AsInt32().Nullable()
            .WithColumn("SESSION_ID").AsString(36).Nullable();

        // CLIENT_SESSION_ROLE
        Create.Table("CLIENT_SESSION_ROLE")
            .WithColumn("ROLE_ID").AsString(255).NotNullable()
            .WithColumn("CLIENT_SESSION").AsString(36).NotNullable();

        // COMPOSITE_ROLE
        Create.Table("COMPOSITE_ROLE")
            .WithColumn("COMPOSITE").AsString(36).NotNullable()
            .WithColumn("CHILD_ROLE").AsString(36).NotNullable();

        // CREDENTIAL
        Create.Table("CREDENTIAL")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_F")
            .WithColumn("DEVICE").AsString(255).Nullable()
            .WithColumn("HASH_ITERATIONS").AsInt32().Nullable()
            .WithColumn("SALT").AsBinary(16).Nullable()
            .WithColumn("TYPE").AsString(255).Nullable()
            .WithColumn("VALUE").AsString(255).Nullable()
            .WithColumn("USER_ID").AsString(36).Nullable();

        // EVENT_ENTITY
        Create.Table("EVENT_ENTITY")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_4")
            .WithColumn("CLIENT_ID").AsString(255).Nullable()
            .WithColumn("DETAILS_JSON").AsString(2550).Nullable()
            .WithColumn("ERROR").AsString(255).Nullable()
            .WithColumn("IP_ADDRESS").AsString(255).Nullable()
            .WithColumn("REALM_ID").AsString(255).Nullable()
            .WithColumn("SESSION_ID").AsString(255).Nullable()
            .WithColumn("TIME").AsInt64().Nullable()
            .WithColumn("TYPE").AsString(255).Nullable()
            .WithColumn("USER_ID").AsString(255).Nullable();

        // FED_PROVIDERS
        Create.Table("FED_PROVIDERS")
            .WithColumn("REALM_ID").AsString(36).NotNullable()
            .WithColumn("USERFEDERATIONPROVIDERS_ID").AsString(36).NotNullable();

        // KEYCLOAK_ROLE
        Create.Table("KEYCLOAK_ROLE")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_A")
            .WithColumn("APP_REALM_CONSTRAINT").AsString(36).Nullable()
            .WithColumn("APPLICATION_ROLE").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("DESCRIPTION").AsString(255).Nullable()
            .WithColumn("NAME").AsString(255).Nullable()
            .WithColumn("REALM_ID").AsString(255).Nullable()
            .WithColumn("APPLICATION").AsString(36).Nullable()
            .WithColumn("REALM").AsString(36).Nullable();

        // REALM
        Create.Table("REALM")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_4A")
            .WithColumn("ACCESS_CODE_LIFESPAN").AsInt32().Nullable()
            .WithColumn("USER_ACTION_LIFESPAN").AsInt32().Nullable()
            .WithColumn("ACCESS_TOKEN_LIFESPAN").AsInt32().Nullable()
            .WithColumn("ACCOUNT_THEME").AsString(255).Nullable()
            .WithColumn("ADMIN_THEME").AsString(255).Nullable()
            .WithColumn("EMAIL_THEME").AsString(255).Nullable()
            .WithColumn("ENABLED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("EVENTS_ENABLED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("EVENTS_EXPIRATION").AsInt64().Nullable()
            .WithColumn("LOGIN_THEME").AsString(255).Nullable()
            .WithColumn("NAME").AsString(255).Nullable()
            .WithColumn("NOT_BEFORE").AsInt32().Nullable()
            .WithColumn("PASSWORD_CRED_GRANT_ALLOWED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("PASSWORD_POLICY").AsString(255).Nullable()
            .WithColumn("PRIVATE_KEY").AsString(2048).Nullable()
            .WithColumn("PUBLIC_KEY").AsString(2048).Nullable()
            .WithColumn("REGISTRATION_ALLOWED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("REMEMBER_ME").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("RESET_PASSWORD_ALLOWED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("SOCIAL").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("SSL_REQUIRED").AsString(255).Nullable()
            .WithColumn("SSO_IDLE_TIMEOUT").AsInt32().Nullable()
            .WithColumn("SSO_MAX_LIFESPAN").AsInt32().Nullable()
            .WithColumn("UPDATE_PROFILE_ON_SOC_LOGIN").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("VERIFY_EMAIL").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("MASTER_ADMIN_APP").AsString(36).Nullable();

        // REALM_APPLICATION
        Create.Table("REALM_APPLICATION")
            .WithColumn("APPLICATION_ID").AsString(36).NotNullable()
            .WithColumn("REALM_ID").AsString(36).NotNullable();

        // REALM_ATTRIBUTE
        Create.Table("REALM_ATTRIBUTE")
            .WithColumn("NAME").AsString(255).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable()
            .WithColumn("REALM_ID").AsString(36).NotNullable();

        // REALM_DEFAULT_ROLES
        Create.Table("REALM_DEFAULT_ROLES")
            .WithColumn("REALM_ID").AsString(36).NotNullable()
            .WithColumn("ROLE_ID").AsString(36).NotNullable();

        // REALM_EVENTS_LISTENERS
        Create.Table("REALM_EVENTS_LISTENERS")
            .WithColumn("REALM_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable();

        // REALM_REQUIRED_CREDENTIAL
        Create.Table("REALM_REQUIRED_CREDENTIAL")
            .WithColumn("TYPE").AsString(255).NotNullable()
            .WithColumn("FORM_LABEL").AsString(255).Nullable()
            .WithColumn("INPUT").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("SECRET").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("REALM_ID").AsString(36).NotNullable();

        // REALM_SMTP_CONFIG
        Create.Table("REALM_SMTP_CONFIG")
            .WithColumn("REALM_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable()
            .WithColumn("NAME").AsString(255).NotNullable();

        // REALM_SOCIAL_CONFIG
        Create.Table("REALM_SOCIAL_CONFIG")
            .WithColumn("REALM_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable()
            .WithColumn("NAME").AsString(255).NotNullable();

        // REDIRECT_URIS
        Create.Table("REDIRECT_URIS")
            .WithColumn("CLIENT_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable();

        // SCOPE_MAPPING
        Create.Table("SCOPE_MAPPING")
            .WithColumn("CLIENT_ID").AsString(36).NotNullable()
            .WithColumn("ROLE_ID").AsString(36).NotNullable();

        // USERNAME_LOGIN_FAILURE
        Create.Table("USERNAME_LOGIN_FAILURE")
            .WithColumn("REALM_ID").AsString(36).NotNullable()
            .WithColumn("USERNAME").AsString(200).NotNullable()
            .WithColumn("FAILED_LOGIN_NOT_BEFORE").AsInt32().Nullable()
            .WithColumn("LAST_FAILURE").AsInt64().Nullable()
            .WithColumn("LAST_IP_FAILURE").AsString(255).Nullable()
            .WithColumn("NUM_FAILURES").AsInt32().Nullable();

        // USER_ATTRIBUTE
        Create.Table("USER_ATTRIBUTE")
            .WithColumn("NAME").AsString(255).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable()
            .WithColumn("USER_ID").AsString(36).NotNullable();

        // USER_ENTITY
        Create.Table("USER_ENTITY")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_FB")
            .WithColumn("EMAIL").AsString(255).Nullable()
            .WithColumn("EMAIL_CONSTRAINT").AsString(255).Nullable()
            .WithColumn("EMAIL_VERIFIED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("ENABLED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("FEDERATION_LINK").AsString(255).Nullable()
            .WithColumn("FIRST_NAME").AsString(255).Nullable()
            .WithColumn("LAST_NAME").AsString(255).Nullable()
            .WithColumn("REALM_ID").AsString(255).Nullable()
            .WithColumn("TOTP").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("USERNAME").AsString(255).Nullable();

        // USER_FEDERATION_CONFIG
        Create.Table("USER_FEDERATION_CONFIG")
            .WithColumn("USER_FEDERATION_PROVIDER_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable()
            .WithColumn("NAME").AsString(255).NotNullable();

        // USER_FEDERATION_PROVIDER
        Create.Table("USER_FEDERATION_PROVIDER")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_5C")
            .WithColumn("CHANGED_SYNC_PERIOD").AsInt32().Nullable()
            .WithColumn("DISPLAY_NAME").AsString(255).Nullable()
            .WithColumn("FULL_SYNC_PERIOD").AsInt32().Nullable()
            .WithColumn("LAST_SYNC").AsInt32().Nullable()
            .WithColumn("PRIORITY").AsInt32().Nullable()
            .WithColumn("PROVIDER_NAME").AsString(255).Nullable()
            .WithColumn("REALM_ID").AsString(36).Nullable();

        // USER_REQUIRED_ACTION
        Create.Table("USER_REQUIRED_ACTION")
            .WithColumn("ACTION").AsInt32().NotNullable()
            .WithColumn("USER_ID").AsString(36).NotNullable();

        // USER_ROLE_MAPPING
        Create.Table("USER_ROLE_MAPPING")
            .WithColumn("ROLE_ID").AsString(255).NotNullable()
            .WithColumn("USER_ID").AsString(36).NotNullable();

        // USER_SESSION
        Create.Table("USER_SESSION")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_57")
            .WithColumn("AUTH_METHOD").AsString(255).Nullable()
            .WithColumn("IP_ADDRESS").AsString(255).Nullable()
            .WithColumn("LAST_SESSION_REFRESH").AsInt32().Nullable()
            .WithColumn("LOGIN_USERNAME").AsString(255).Nullable()
            .WithColumn("REALM_ID").AsString(255).Nullable()
            .WithColumn("REMEMBER_ME").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("STARTED").AsInt32().Nullable()
            .WithColumn("USER_ID").AsString(255).Nullable();

        // USER_SOCIAL_LINK
        Create.Table("USER_SOCIAL_LINK")
            .WithColumn("SOCIAL_PROVIDER").AsString(255).NotNullable()
            .WithColumn("REALM_ID").AsString(255).Nullable()
            .WithColumn("SOCIAL_USER_ID").AsString(255).Nullable()
            .WithColumn("SOCIAL_USERNAME").AsString(255).Nullable()
            .WithColumn("USER_ID").AsString(36).NotNullable();

        // WEB_ORIGINS
        Create.Table("WEB_ORIGINS")
            .WithColumn("CLIENT_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable();

        // Add primary keys for tables that need composite keys
        Create.PrimaryKey("CONSTRAINT_1")
            .OnTable("REALM_SOCIAL_CONFIG")
            .Columns("REALM_ID", "NAME");

        Create.PrimaryKey("CONSTRAINT_17")
            .OnTable("USERNAME_LOGIN_FAILURE")
            .Columns("REALM_ID", "USERNAME");

        Create.PrimaryKey("CONSTRAINT_2")
            .OnTable("USER_REQUIRED_ACTION")
            .Columns("ACTION", "USER_ID");

        Create.PrimaryKey("CONSTRAINT_3")
            .OnTable("USER_SOCIAL_LINK")
            .Columns("SOCIAL_PROVIDER", "USER_ID");

        Create.PrimaryKey("CONSTRAINT_5")
            .OnTable("CLIENT_SESSION_ROLE")
            .Columns("CLIENT_SESSION", "ROLE_ID");

        Create.PrimaryKey("CONSTRAINT_6")
            .OnTable("USER_ATTRIBUTE")
            .Columns("NAME", "USER_ID");

        Create.PrimaryKey("CONSTRAINT_81")
            .OnTable("SCOPE_MAPPING")
            .Columns("CLIENT_ID", "ROLE_ID");

        Create.PrimaryKey("CONSTRAINT_9")
            .OnTable("REALM_ATTRIBUTE")
            .Columns("NAME", "REALM_ID");

        Create.PrimaryKey("CONSTRAINT_92")
            .OnTable("REALM_REQUIRED_CREDENTIAL")
            .Columns("REALM_ID", "TYPE");

        Create.PrimaryKey("CONSTRAINT_C")
            .OnTable("USER_ROLE_MAPPING")
            .Columns("ROLE_ID", "USER_ID");

        Create.PrimaryKey("CONSTRAINT_E")
            .OnTable("REALM_SMTP_CONFIG")
            .Columns("REALM_ID", "NAME");

        Create.PrimaryKey("CONSTRAINT_F9")
            .OnTable("USER_FEDERATION_CONFIG")
            .Columns("USER_FEDERATION_PROVIDER_ID", "NAME");

        // Add unique constraints
        Create.UniqueConstraint("UK_8AELWNIBJI49AVXSRTUF6XJOW")
            .OnTable("APPLICATION_DEFAULT_ROLES")
            .Columns("ROLE_ID");

        Create.UniqueConstraint("UK_B71CJLBENV945RB6GCON438AT")
            .OnTable("CLIENT")
            .Columns("REALM_ID", "NAME");

        Create.UniqueConstraint("UK_DCCIRJLIPU1478VQC89DID88C")
            .OnTable("FED_PROVIDERS")
            .Columns("USERFEDERATIONPROVIDERS_ID");

        Create.UniqueConstraint("UK_DYKN684SL8UP1CRFEI6ECKHD7")
            .OnTable("USER_ENTITY")
            .Columns("REALM_ID", "EMAIL_CONSTRAINT");

        Create.UniqueConstraint("UK_H4WPD7W4HSOOLNI3H0SW7BTJE")
            .OnTable("REALM_DEFAULT_ROLES")
            .Columns("ROLE_ID");

        Create.UniqueConstraint("UK_J3RWUVD56ONTGSUHOGM184WW2")
            .OnTable("KEYCLOAK_ROLE")
            .Columns("NAME", "APP_REALM_CONSTRAINT");

        Create.UniqueConstraint("UK_L5QGA3RFME47335JY8JXYXH3I")
            .OnTable("REALM_APPLICATION")
            .Columns("REALM_ID");

        Create.UniqueConstraint("UK_ORVSDMLA56612EAEFIQ6WL5OI")
            .OnTable("REALM")
            .Columns("NAME");

        Create.UniqueConstraint("UK_RU8TT6T700S9V50BU18WS5HA6")
            .OnTable("USER_ENTITY")
            .Columns("REALM_ID", "USERNAME");

        // Add foreign keys
        Create.ForeignKey("FK_11B7SGQW18I532811V7O2DV76")
            .FromTable("CLIENT_SESSION_ROLE").ForeignColumn("CLIENT_SESSION")
            .ToTable("CLIENT_SESSION").PrimaryColumn("ID");

        Create.ForeignKey("FK_1BURS8PB4OUJ97H5WUPPAHV9F")
            .FromTable("REDIRECT_URIS").ForeignColumn("CLIENT_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        Create.ForeignKey("FK_1FJ32F6PTOLW2QY60CD8N01E8")
            .FromTable("USER_FEDERATION_PROVIDER").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_213LYQ09FKXQ8K8NY8DY3737T")
            .FromTable("FED_PROVIDERS").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_5HG65LYBEVAVKQFKI3KPONH9V")
            .FromTable("REALM_REQUIRED_CREDENTIAL").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_5HRM2VLF9QL5FU043KQEPOVBR")
            .FromTable("USER_ATTRIBUTE").ForeignColumn("USER_ID")
            .ToTable("USER_ENTITY").PrimaryColumn("ID");

        Create.ForeignKey("FK_68CJYS5UWM55UY823Y75XG4OM")
            .FromTable("USER_SOCIAL_LINK").ForeignColumn("USER_ID")
            .ToTable("USER_ENTITY").PrimaryColumn("ID");

        Create.ForeignKey("FK_6QJ3W1JW9CVAFHE19BWSIUVMD")
            .FromTable("USER_REQUIRED_ACTION").ForeignColumn("USER_ID")
            .ToTable("USER_ENTITY").PrimaryColumn("ID");

        Create.ForeignKey("FK_6VYQFE4CN4WLQ8R6KT5VDSJ5C")
            .FromTable("KEYCLOAK_ROLE").ForeignColumn("REALM")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_70EJ8XDXGXD0B9HH6180IRR0O")
            .FromTable("REALM_SMTP_CONFIG").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_71S3P0DIUXAWWQQSA528UBY2Q")
            .FromTable("REALM_APPLICATION").ForeignColumn("APPLICATION_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_8AELWNIBJI49AVXSRTUF6XJOW")
            .FromTable("APPLICATION_DEFAULT_ROLES").ForeignColumn("ROLE_ID")
            .ToTable("KEYCLOAK_ROLE").PrimaryColumn("ID");

        Create.ForeignKey("FK_8SHXD6L3E9ATQUKACXGPFFPTW")
            .FromTable("REALM_ATTRIBUTE").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_A63WVEKFTU8JO1PNJ81E7MCE2")
            .FromTable("COMPOSITE_ROLE").ForeignColumn("COMPOSITE")
            .ToTable("KEYCLOAK_ROLE").PrimaryColumn("ID");

        Create.ForeignKey("FK_B4AO2VCVAT6UKAU74WBWTFQO1")
            .FromTable("CLIENT_SESSION").ForeignColumn("SESSION_ID")
            .ToTable("USER_SESSION").PrimaryColumn("ID");

        Create.ForeignKey("FK_C4FQV34P1MBYLLOXANG7B1Q3L")
            .FromTable("USER_ROLE_MAPPING").ForeignColumn("USER_ID")
            .ToTable("USER_ENTITY").PrimaryColumn("ID");

        Create.ForeignKey("FK_DCCIRJLIPU1478VQC89DID88C")
            .FromTable("FED_PROVIDERS").ForeignColumn("USERFEDERATIONPROVIDERS_ID")
            .ToTable("USER_FEDERATION_PROVIDER").PrimaryColumn("ID");

        Create.ForeignKey("FK_EVUDB1PPW84OXFAX2DRS03ICC")
            .FromTable("REALM_DEFAULT_ROLES").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_GR7THLLB9LU8Q4VQA4524JJY8")
            .FromTable("COMPOSITE_ROLE").ForeignColumn("CHILD_ROLE")
            .ToTable("KEYCLOAK_ROLE").PrimaryColumn("ID");

        Create.ForeignKey("FK_H4WPD7W4HSOOLNI3H0SW7BTJE")
            .FromTable("REALM_DEFAULT_ROLES").ForeignColumn("ROLE_ID")
            .ToTable("KEYCLOAK_ROLE").PrimaryColumn("ID");

        Create.ForeignKey("FK_H846O4H0W8EPX5NXEV9F5Y69J")
            .FromTable("REALM_EVENTS_LISTENERS").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_L5QGA3RFME47335JY8JXYXH3I")
            .FromTable("REALM_APPLICATION").ForeignColumn("REALM_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        Create.ForeignKey("FK_LOJPHO213XCX4WNKOG82SSRFY")
            .FromTable("WEB_ORIGINS").ForeignColumn("CLIENT_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        Create.ForeignKey("FK_MAYLTS7KLWQW2H8M2B5JOYTKY")
            .FromTable("APPLICATION_DEFAULT_ROLES").ForeignColumn("APPLICATION_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        Create.ForeignKey("FK_OUSE064PLMLR732LXJCN1Q5F1")
            .FromTable("SCOPE_MAPPING").ForeignColumn("CLIENT_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        Create.ForeignKey("FK_P3RH9GRKU11KQFRS4FLTT7RNQ")
            .FromTable("SCOPE_MAPPING").ForeignColumn("ROLE_ID")
            .ToTable("KEYCLOAK_ROLE").PrimaryColumn("ID");

        Create.ForeignKey("FK_P56CTINXXB9GSK57FO49F9TAC")
            .FromTable("CLIENT").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_PFYR0GLASQYL0DEI3KL69R6V0")
            .FromTable("CREDENTIAL").ForeignColumn("USER_ID")
            .ToTable("USER_ENTITY").PrimaryColumn("ID");

        Create.ForeignKey("FK_PIMO5LE2C0RAL09FL8CM9WFW9")
            .FromTable("KEYCLOAK_ROLE").ForeignColumn("APPLICATION")
            .ToTable("CLIENT").PrimaryColumn("ID");

        Create.ForeignKey("FK_RSAF444KK6QRKMS7N56AIWQ5Y")
            .FromTable("REALM").ForeignColumn("MASTER_ADMIN_APP")
            .ToTable("CLIENT").PrimaryColumn("ID");

        Create.ForeignKey("FK_SV5I3C2TI7G0G922FGE683SOV")
            .FromTable("REALM_SOCIAL_CONFIG").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.ForeignKey("FK_T13HPU1J94R2EBPEKR39X5EU5")
            .FromTable("USER_FEDERATION_CONFIG").ForeignColumn("USER_FEDERATION_PROVIDER_ID")
            .ToTable("USER_FEDERATION_PROVIDER").PrimaryColumn("ID");
    }

    public override void Down()
    {
        // Drop foreign keys first
        Delete.ForeignKey("FK_T13HPU1J94R2EBPEKR39X5EU5").OnTable("USER_FEDERATION_CONFIG");
        Delete.ForeignKey("FK_SV5I3C2TI7G0G922FGE683SOV").OnTable("REALM_SOCIAL_CONFIG");
        Delete.ForeignKey("FK_RSAF444KK6QRKMS7N56AIWQ5Y").OnTable("REALM");
        Delete.ForeignKey("FK_PIMO5LE2C0RAL09FL8CM9WFW9").OnTable("KEYCLOAK_ROLE");
        Delete.ForeignKey("FK_PFYR0GLASQYL0DEI3KL69R6V0").OnTable("CREDENTIAL");
        Delete.ForeignKey("FK_P56CTINXXB9GSK57FO49F9TAC").OnTable("CLIENT");
        Delete.ForeignKey("FK_P3RH9GRKU11KQFRS4FLTT7RNQ").OnTable("SCOPE_MAPPING");
        Delete.ForeignKey("FK_OUSE064PLMLR732LXJCN1Q5F1").OnTable("SCOPE_MAPPING");
        Delete.ForeignKey("FK_MAYLTS7KLWQW2H8M2B5JOYTKY").OnTable("APPLICATION_DEFAULT_ROLES");
        Delete.ForeignKey("FK_LOJPHO213XCX4WNKOG82SSRFY").OnTable("WEB_ORIGINS");
        Delete.ForeignKey("FK_L5QGA3RFME47335JY8JXYXH3I").OnTable("REALM_APPLICATION");
        Delete.ForeignKey("FK_H846O4H0W8EPX5NXEV9F5Y69J").OnTable("REALM_EVENTS_LISTENERS");
        Delete.ForeignKey("FK_H4WPD7W4HSOOLNI3H0SW7BTJE").OnTable("REALM_DEFAULT_ROLES");
        Delete.ForeignKey("FK_GR7THLLB9LU8Q4VQA4524JJY8").OnTable("COMPOSITE_ROLE");
        Delete.ForeignKey("FK_EVUDB1PPW84OXFAX2DRS03ICC").OnTable("REALM_DEFAULT_ROLES");
        Delete.ForeignKey("FK_DCCIRJLIPU1478VQC89DID88C").OnTable("FED_PROVIDERS");
        Delete.ForeignKey("FK_C4FQV34P1MBYLLOXANG7B1Q3L").OnTable("USER_ROLE_MAPPING");
        Delete.ForeignKey("FK_B4AO2VCVAT6UKAU74WBWTFQO1").OnTable("CLIENT_SESSION");
        Delete.ForeignKey("FK_A63WVEKFTU8JO1PNJ81E7MCE2").OnTable("COMPOSITE_ROLE");
        Delete.ForeignKey("FK_8SHXD6L3E9ATQUKACXGPFFPTW").OnTable("REALM_ATTRIBUTE");
        Delete.ForeignKey("FK_8AELWNIBJI49AVXSRTUF6XJOW").OnTable("APPLICATION_DEFAULT_ROLES");
        Delete.ForeignKey("FK_71S3P0DIUXAWWQQSA528UBY2Q").OnTable("REALM_APPLICATION");
        Delete.ForeignKey("FK_70EJ8XDXGXD0B9HH6180IRR0O").OnTable("REALM_SMTP_CONFIG");
        Delete.ForeignKey("FK_6VYQFE4CN4WLQ8R6KT5VDSJ5C").OnTable("KEYCLOAK_ROLE");
        Delete.ForeignKey("FK_6QJ3W1JW9CVAFHE19BWSIUVMD").OnTable("USER_REQUIRED_ACTION");
        Delete.ForeignKey("FK_68CJYS5UWM55UY823Y75XG4OM").OnTable("USER_SOCIAL_LINK");
        Delete.ForeignKey("FK_5HRM2VLF9QL5FU043KQEPOVBR").OnTable("USER_ATTRIBUTE");
        Delete.ForeignKey("FK_5HG65LYBEVAVKQFKI3KPONH9V").OnTable("REALM_REQUIRED_CREDENTIAL");
        Delete.ForeignKey("FK_213LYQ09FKXQ8K8NY8DY3737T").OnTable("FED_PROVIDERS");
        Delete.ForeignKey("FK_1FJ32F6PTOLW2QY60CD8N01E8").OnTable("USER_FEDERATION_PROVIDER");
        Delete.ForeignKey("FK_1BURS8PB4OUJ97H5WUPPAHV9F").OnTable("REDIRECT_URIS");
        Delete.ForeignKey("FK_11B7SGQW18I532811V7O2DV76").OnTable("CLIENT_SESSION_ROLE");

        // Drop tables
        Delete.Table("WEB_ORIGINS");
        Delete.Table("USER_SOCIAL_LINK");
        Delete.Table("USER_SESSION");
        Delete.Table("USER_ROLE_MAPPING");
        Delete.Table("USER_REQUIRED_ACTION");
        Delete.Table("USER_FEDERATION_PROVIDER");
        Delete.Table("USER_FEDERATION_CONFIG");
        Delete.Table("USER_ENTITY");
        Delete.Table("USER_ATTRIBUTE");
        Delete.Table("USERNAME_LOGIN_FAILURE");
        Delete.Table("SCOPE_MAPPING");
        Delete.Table("REDIRECT_URIS");
        Delete.Table("REALM_SOCIAL_CONFIG");
        Delete.Table("REALM_SMTP_CONFIG");
        Delete.Table("REALM_REQUIRED_CREDENTIAL");
        Delete.Table("REALM_EVENTS_LISTENERS");
        Delete.Table("REALM_DEFAULT_ROLES");
        Delete.Table("REALM_ATTRIBUTE");
        Delete.Table("REALM_APPLICATION");
        Delete.Table("REALM");
        Delete.Table("KEYCLOAK_ROLE");
        Delete.Table("FED_PROVIDERS");
        Delete.Table("EVENT_ENTITY");
        Delete.Table("CREDENTIAL");
        Delete.Table("COMPOSITE_ROLE");
        Delete.Table("CLIENT_SESSION_ROLE");
        Delete.Table("CLIENT_SESSION");
        Delete.Table("CLIENT");
        Delete.Table("APPLICATION_DEFAULT_ROLES");
    }
}
