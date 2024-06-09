using FluentMigrator;

namespace tmgcat.Dal.Infrastructure;

[Migration(01, TransactionBehavior.None)]
public class InitSchema : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsInt64().PrimaryKey("users_pk").Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("email").AsString().NotNullable()
            .WithColumn("password").AsString().NotNullable()
            .WithColumn("info").AsString().Nullable()
            .WithColumn("profile_picture_path").AsString().Nullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("blocked_at").AsDateTimeOffset().Nullable();
        
        Create.Table("games")
            .WithColumn("id").AsInt64().PrimaryKey("games_pk").Identity()
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("igdb_id").AsInt32().NotNullable()
            .WithColumn("released_at").AsDateTimeOffset().Nullable()
            .WithColumn("platforms").AsString().Nullable()
            .WithColumn("genres").AsString().Nullable()
            .WithColumn("cover_path").AsString().Nullable()
            .WithColumn("status").AsInt32().NotNullable()
            .WithColumn("category").AsString().Nullable()
            .WithColumn("involved_companies").AsString().Nullable();

        Create.Table("game_list")
            .WithColumn("id").AsInt64().PrimaryKey("game_list_pk").Identity()
            .WithColumn("user_id").AsInt64().NotNullable()
            .WithColumn("game_id").AsInt64().NotNullable()
            .WithColumn("status").AsInt32().NotNullable()
            .WithColumn("minutes_played").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("user_rating").AsInt32().Nullable()
            .WithColumn("deleted_at").AsDateTimeOffset().Nullable();

        Create.Table("comments")
            .WithColumn("id").AsInt64().PrimaryKey("comments_pk").Identity()
            .WithColumn("page_type").AsInt32().NotNullable()
            .WithColumn("page_id").AsInt64().NotNullable()
            .WithColumn("parent_comment_id").AsInt64().Nullable()
            .WithColumn("content").AsString().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("created_by_user_id").AsInt64().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("users");
        Delete.Table("games");
        Delete.Table("game_list");
        Delete.Table("comments");
    }
}