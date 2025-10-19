using FluentMigrator;

namespace HighloadSocialNetwork.WebServer.DataAccess.Migrations;

[Migration(002)]
public class AddIndexForSearch : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql("CREATE INDEX idx_users_first_name_second_name_id ON users(first_name text_pattern_ops, second_name text_pattern_ops, id);");
    }
}