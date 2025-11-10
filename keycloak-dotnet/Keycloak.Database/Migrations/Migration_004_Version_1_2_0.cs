using FluentMigrator;

namespace Keycloak.Database.Migrations;

/// <summary>
/// Updates for Keycloak version 1.2.0.
/// Combines Liquibase migrations: jpa-changelog-1.2.0.Beta1.xml, jpa-changelog-1.2.0.CR1.xml, jpa-changelog-1.2.0.Final.xml
/// </summary>
[Migration(4, "Version 1.2.0 - Identity providers and protocol mappers")]
public class Migration_004_Version_1_2_0 : Migration
{
    public override void Up()
    {
        // Clean up session tables (from Beta1)
        Delete.FromTable("CLIENT_SESSION_ROLE").AllRows();
        Delete.FromTable("CLIENT_SESSION_NOTE").AllRows();
        Delete.FromTable("CLIENT_SESSION").AllRows();
        Delete.FromTable("USER_SESSION").AllRows();

        // PROTOCOL_MAPPER
        Create.Table("PROTOCOL_MAPPER")
            .WithColumn("ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_PCM")
            .WithColumn("NAME").AsString(255).NotNullable()
            .WithColumn("PROTOCOL").AsString(255).NotNullable()
            .WithColumn("PROTOCOL_MAPPER_NAME").AsString(255).NotNullable()
            .WithColumn("CONSENT_REQUIRED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("CONSENT_TEXT").AsString(255).Nullable()
            .WithColumn("CLIENT_ID").AsString(36).NotNullable();

        Create.ForeignKey("FK_PCM_REALM")
            .FromTable("PROTOCOL_MAPPER").ForeignColumn("CLIENT_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        // PROTOCOL_MAPPER_CONFIG
        Create.Table("PROTOCOL_MAPPER_CONFIG")
            .WithColumn("PROTOCOL_MAPPER_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsCustom("TEXT").Nullable()
            .WithColumn("NAME").AsString(255).NotNullable();

        Create.PrimaryKey("CONSTRAINT_PMConfig")
            .OnTable("PROTOCOL_MAPPER_CONFIG")
            .Columns("PROTOCOL_MAPPER_ID", "NAME");

        Create.ForeignKey("FK_PMConfig")
            .FromTable("PROTOCOL_MAPPER_CONFIG").ForeignColumn("PROTOCOL_MAPPER_ID")
            .ToTable("PROTOCOL_MAPPER").PrimaryColumn("ID");

        // FEDERATED_IDENTITY
        Create.Table("FEDERATED_IDENTITY")
            .WithColumn("IDENTITY_PROVIDER").AsString(255).NotNullable()
            .WithColumn("REALM_ID").AsString(36).Nullable()
            .WithColumn("FEDERATED_USER_ID").AsString(255).Nullable()
            .WithColumn("FEDERATED_USERNAME").AsString(255).Nullable()
            .WithColumn("TOKEN").AsCustom("TEXT").Nullable()
            .WithColumn("USER_ID").AsString(36).NotNullable();

        Create.PrimaryKey("CONSTRAINT_40")
            .OnTable("FEDERATED_IDENTITY")
            .Columns("IDENTITY_PROVIDER", "USER_ID");

        Create.ForeignKey("FK404288B92EF007A6")
            .FromTable("FEDERATED_IDENTITY").ForeignColumn("USER_ID")
            .ToTable("USER_ENTITY").PrimaryColumn("ID");

        // IDENTITY_PROVIDER
        Create.Table("IDENTITY_PROVIDER")
            .WithColumn("INTERNAL_ID").AsString(36).NotNullable().PrimaryKey("CONSTRAINT_2B")
            .WithColumn("ENABLED").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("PROVIDER_ALIAS").AsString(255).Nullable()
            .WithColumn("PROVIDER_ID").AsString(255).Nullable()
            .WithColumn("UPDATE_PROFILE_FIRST_LOGIN").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("STORE_TOKEN").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("AUTHENTICATE_BY_DEFAULT").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("REALM_ID").AsString(36).Nullable();

        Create.UniqueConstraint("UK_2DAELWNIBJI49AVXSRTUF6XJ33")
            .OnTable("IDENTITY_PROVIDER")
            .Columns("PROVIDER_ALIAS", "REALM_ID");

        Create.ForeignKey("FK2B4EBC52AE5C3B34")
            .FromTable("IDENTITY_PROVIDER").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        // IDENTITY_PROVIDER_CONFIG
        Create.Table("IDENTITY_PROVIDER_CONFIG")
            .WithColumn("IDENTITY_PROVIDER_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsCustom("TEXT").Nullable()
            .WithColumn("NAME").AsString(255).NotNullable();

        Create.PrimaryKey("CONSTRAINT_D")
            .OnTable("IDENTITY_PROVIDER_CONFIG")
            .Columns("IDENTITY_PROVIDER_ID", "NAME");

        Create.ForeignKey("FKDC4897CF864C4E43")
            .FromTable("IDENTITY_PROVIDER_CONFIG").ForeignColumn("IDENTITY_PROVIDER_ID")
            .ToTable("IDENTITY_PROVIDER").PrimaryColumn("INTERNAL_ID");

        // CLIENT_IDENTITY_PROV_MAPPING
        Create.Table("CLIENT_IDENTITY_PROV_MAPPING")
            .WithColumn("CLIENT_ID").AsString(36).NotNullable()
            .WithColumn("IDENTITY_PROVIDER_ID").AsString(36).NotNullable()
            .WithColumn("RETRIEVE_TOKEN").AsBoolean().NotNullable().WithDefaultValue(false);

        Create.UniqueConstraint("UK_7CAELWNIBJI49AVXSRTUF6XJ12")
            .OnTable("CLIENT_IDENTITY_PROV_MAPPING")
            .Columns("IDENTITY_PROVIDER_ID", "CLIENT_ID");

        Create.ForeignKey("FK_7CELWNIBJI49AVXSRTUF6XJ12")
            .FromTable("CLIENT_IDENTITY_PROV_MAPPING").ForeignColumn("IDENTITY_PROVIDER_ID")
            .ToTable("IDENTITY_PROVIDER").PrimaryColumn("INTERNAL_ID");

        Create.ForeignKey("FK_56ELWNIBJI49AVXSRTUF6XJ23")
            .FromTable("CLIENT_IDENTITY_PROV_MAPPING").ForeignColumn("CLIENT_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        // REALM_SUPPORTED_LOCALES
        Create.Table("REALM_SUPPORTED_LOCALES")
            .WithColumn("REALM_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable();

        Create.ForeignKey("FK_SUPPORTED_LOCALES_REALM")
            .FromTable("REALM_SUPPORTED_LOCALES").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        // USER_SESSION_NOTE
        Create.Table("USER_SESSION_NOTE")
            .WithColumn("USER_SESSION").AsString(36).NotNullable()
            .WithColumn("NAME").AsString(255).NotNullable()
            .WithColumn("VALUE").AsString(2048).Nullable();

        Create.PrimaryKey("CONSTRAINT_USN_PK")
            .OnTable("USER_SESSION_NOTE")
            .Columns("USER_SESSION", "NAME");

        Create.ForeignKey("FK5EDFB00FF51D3472")
            .FromTable("USER_SESSION_NOTE").ForeignColumn("USER_SESSION")
            .ToTable("USER_SESSION").PrimaryColumn("ID");

        // Add columns to CLIENT
        Alter.Table("CLIENT")
            .AddColumn("FRONTCHANNEL_LOGOUT").AsBoolean().NotNullable().WithDefaultValue(false);

        // Add columns to USER_SESSION
        Alter.Table("USER_SESSION")
            .AddColumn("USER_SESSION_STATE").AsInt32().Nullable()
            .AddColumn("BROKER_SESSION_ID").AsString(255).Nullable()
            .AddColumn("BROKER_USER_ID").AsString(255).Nullable();

        // Add columns to REALM
        Alter.Table("REALM")
            .AddColumn("LOGIN_LIFESPAN").AsInt32().Nullable()
            .AddColumn("INTERNATIONALIZATION_ENABLED").AsBoolean().NotNullable().WithDefaultValue(false)
            .AddColumn("DEFAULT_LOCALE").AsString(255).Nullable()
            .AddColumn("REG_EMAIL_AS_USERNAME").AsBoolean().NotNullable().WithDefaultValue(false);

        // Fix REALM_APPLICATION table (KEYCLOAK-1106 - APPLICATION_ID and REALM_ID switched)
        Delete.ForeignKey("FK_71S3P0DIUXAWWQQSA528UBY2Q").OnTable("REALM_APPLICATION");
        Delete.ForeignKey("FK_L5QGA3RFME47335JY8JXYXH3I").OnTable("REALM_APPLICATION");
        Delete.UniqueConstraint("UK_L5QGA3RFME47335JY8JXYXH3I").FromTable("REALM_APPLICATION");

        Rename.Column("APPLICATION_ID").OnTable("REALM_APPLICATION").To("APPLICATION_ID_TMP");
        Rename.Column("REALM_ID").OnTable("REALM_APPLICATION").To("APPLICATION_ID");
        Rename.Column("APPLICATION_ID_TMP").OnTable("REALM_APPLICATION").To("REALM_ID");

        Create.UniqueConstraint("UK_M6QGA3RFME47335JY8JXYXH3I")
            .OnTable("REALM_APPLICATION")
            .Columns("APPLICATION_ID");

        Create.ForeignKey("FK_82S3P0DIUXAWWQQSA528UBY2Q")
            .FromTable("REALM_APPLICATION").ForeignColumn("APPLICATION_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        Create.ForeignKey("FK_M6QGA3RFME47335JY8JXYXH3I")
            .FromTable("REALM_APPLICATION").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        // Remove obsolete 'social' and 'claims' stuff
        Delete.ForeignKey("FK_68CJYS5UWM55UY823Y75XG4OM").OnTable("USER_SOCIAL_LINK");
        Delete.Table("USER_SOCIAL_LINK");

        Delete.ForeignKey("FK_SV5I3C2TI7G0G922FGE683SOV").OnTable("REALM_SOCIAL_CONFIG");
        Delete.Table("REALM_SOCIAL_CONFIG");

        Delete.Column("ALLOWED_CLAIMS_MASK").FromTable("CLIENT");

        // REALM_ENABLED_EVENT_TYPES
        Create.Table("REALM_ENABLED_EVENT_TYPES")
            .WithColumn("REALM_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable();

        Create.ForeignKey("FK_H846O4H0W8EPX5NWEDRF5Y69J")
            .FromTable("REALM_ENABLED_EVENT_TYPES").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        // From 1.2.0.Final - KEYCLOAK-1277 - Update NULL values
        Update.Table("CLIENT").Set(new { DIRECT_GRANTS_ONLY = false }).Where(new { DIRECT_GRANTS_ONLY = (bool?)null });
        Update.Table("CLIENT").Set(new { BEARER_ONLY = false }).Where(new { BEARER_ONLY = (bool?)null });
        Update.Table("CLIENT").Set(new { SURROGATE_AUTH_REQUIRED = false }).Where(new { SURROGATE_AUTH_REQUIRED = (bool?)null });
    }

    public override void Down()
    {
        // Remove tables in reverse order
        Delete.ForeignKey("FK_H846O4H0W8EPX5NWEDRF5Y69J").OnTable("REALM_ENABLED_EVENT_TYPES");
        Delete.Table("REALM_ENABLED_EVENT_TYPES");

        // Restore social config tables
        Create.Table("REALM_SOCIAL_CONFIG")
            .WithColumn("REALM_ID").AsString(36).NotNullable()
            .WithColumn("VALUE").AsString(255).Nullable()
            .WithColumn("NAME").AsString(255).NotNullable();

        Create.PrimaryKey("CONSTRAINT_1")
            .OnTable("REALM_SOCIAL_CONFIG")
            .Columns("REALM_ID", "NAME");

        Create.ForeignKey("FK_SV5I3C2TI7G0G922FGE683SOV")
            .FromTable("REALM_SOCIAL_CONFIG").ForeignColumn("REALM_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        Create.Table("USER_SOCIAL_LINK")
            .WithColumn("SOCIAL_PROVIDER").AsString(255).NotNullable()
            .WithColumn("REALM_ID").AsString(255).Nullable()
            .WithColumn("SOCIAL_USER_ID").AsString(255).Nullable()
            .WithColumn("SOCIAL_USERNAME").AsString(255).Nullable()
            .WithColumn("USER_ID").AsString(36).NotNullable();

        Create.PrimaryKey("CONSTRAINT_3")
            .OnTable("USER_SOCIAL_LINK")
            .Columns("SOCIAL_PROVIDER", "USER_ID");

        Create.ForeignKey("FK_68CJYS5UWM55UY823Y75XG4OM")
            .FromTable("USER_SOCIAL_LINK").ForeignColumn("USER_ID")
            .ToTable("USER_ENTITY").PrimaryColumn("ID");

        Alter.Table("CLIENT")
            .AddColumn("ALLOWED_CLAIMS_MASK").AsInt64().Nullable();

        // Revert REALM_APPLICATION changes
        Delete.ForeignKey("FK_M6QGA3RFME47335JY8JXYXH3I").OnTable("REALM_APPLICATION");
        Delete.ForeignKey("FK_82S3P0DIUXAWWQQSA528UBY2Q").OnTable("REALM_APPLICATION");
        Delete.UniqueConstraint("UK_M6QGA3RFME47335JY8JXYXH3I").FromTable("REALM_APPLICATION");

        Rename.Column("REALM_ID").OnTable("REALM_APPLICATION").To("APPLICATION_ID_TMP");
        Rename.Column("APPLICATION_ID").OnTable("REALM_APPLICATION").To("REALM_ID");
        Rename.Column("APPLICATION_ID_TMP").OnTable("REALM_APPLICATION").To("APPLICATION_ID");

        Create.UniqueConstraint("UK_L5QGA3RFME47335JY8JXYXH3I")
            .OnTable("REALM_APPLICATION")
            .Columns("REALM_ID");

        Create.ForeignKey("FK_L5QGA3RFME47335JY8JXYXH3I")
            .FromTable("REALM_APPLICATION").ForeignColumn("REALM_ID")
            .ToTable("CLIENT").PrimaryColumn("ID");

        Create.ForeignKey("FK_71S3P0DIUXAWWQQSA528UBY2Q")
            .FromTable("REALM_APPLICATION").ForeignColumn("APPLICATION_ID")
            .ToTable("REALM").PrimaryColumn("ID");

        // Remove columns from REALM
        Delete.Column("REG_EMAIL_AS_USERNAME").FromTable("REALM");
        Delete.Column("DEFAULT_LOCALE").FromTable("REALM");
        Delete.Column("INTERNATIONALIZATION_ENABLED").FromTable("REALM");
        Delete.Column("LOGIN_LIFESPAN").FromTable("REALM");

        // Remove columns from USER_SESSION
        Delete.Column("BROKER_USER_ID").FromTable("USER_SESSION");
        Delete.Column("BROKER_SESSION_ID").FromTable("USER_SESSION");
        Delete.Column("USER_SESSION_STATE").FromTable("USER_SESSION");

        // Remove columns from CLIENT
        Delete.Column("FRONTCHANNEL_LOGOUT").FromTable("CLIENT");

        // Remove tables
        Delete.ForeignKey("FK5EDFB00FF51D3472").OnTable("USER_SESSION_NOTE");
        Delete.Table("USER_SESSION_NOTE");

        Delete.ForeignKey("FK_SUPPORTED_LOCALES_REALM").OnTable("REALM_SUPPORTED_LOCALES");
        Delete.Table("REALM_SUPPORTED_LOCALES");

        Delete.ForeignKey("FK_56ELWNIBJI49AVXSRTUF6XJ23").OnTable("CLIENT_IDENTITY_PROV_MAPPING");
        Delete.ForeignKey("FK_7CELWNIBJI49AVXSRTUF6XJ12").OnTable("CLIENT_IDENTITY_PROV_MAPPING");
        Delete.Table("CLIENT_IDENTITY_PROV_MAPPING");

        Delete.ForeignKey("FKDC4897CF864C4E43").OnTable("IDENTITY_PROVIDER_CONFIG");
        Delete.Table("IDENTITY_PROVIDER_CONFIG");

        Delete.ForeignKey("FK2B4EBC52AE5C3B34").OnTable("IDENTITY_PROVIDER");
        Delete.Table("IDENTITY_PROVIDER");

        Delete.ForeignKey("FK404288B92EF007A6").OnTable("FEDERATED_IDENTITY");
        Delete.Table("FEDERATED_IDENTITY");

        Delete.ForeignKey("FK_PMConfig").OnTable("PROTOCOL_MAPPER_CONFIG");
        Delete.Table("PROTOCOL_MAPPER_CONFIG");

        Delete.ForeignKey("FK_PCM_REALM").OnTable("PROTOCOL_MAPPER");
        Delete.Table("PROTOCOL_MAPPER");
    }
}
