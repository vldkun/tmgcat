using FluentMigrator;

namespace tmgcat.Dal.Infrastructure;

[Migration(02, TransactionBehavior.None)]
public class AddIndexes : Migration
{
    public override void Up()
    {
        Create.Index()
            .OnTable("game_list")
            .OnColumn("user_id").Ascending()
            .OnColumn("game_id").Ascending();

        Create.Index()
            .OnTable("comments")
            .OnColumn("page_id").Ascending()
            .OnColumn("page_type").Ascending()
            .OnColumn("created_by_user_id").Ascending();
    }

    public override void Down()
    {
        Delete.Index().OnTable("game_list");
        Delete.Index().OnTable("comments");
    }
}