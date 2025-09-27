using FluentMigrator;

namespace HighloadSocialNetwork.WebServer.DataAccess.Migrations;

[Migration(001)]
public class CreateUserTables : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("first_name").AsString(64).NotNullable()
            .WithColumn("second_name").AsString(64).NotNullable()
            .WithColumn("birthdate").AsDate().NotNullable()
            .WithColumn("biography").AsString(1024).Nullable()
            .WithColumn("city").AsString(64).NotNullable();

        Create.Table("users_logins")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("password").AsString().NotNullable();
        
        Create.ForeignKey("FK_users_logins_id")
            .FromTable("users_logins")
            .ForeignColumn("id")
            .ToTable("users")
            .PrimaryColumn("id");
    }
}