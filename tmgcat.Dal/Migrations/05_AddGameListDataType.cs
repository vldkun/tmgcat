using FluentMigrator;

namespace tmgcat.Dal.Infrastructure;

[Migration(05, TransactionBehavior.None)]
public class AddGameListDataType : Migration
{
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'game_list_data') THEN
            CREATE TYPE game_list_data as
            (
                  id                  bigint
                , user_id             bigint
                , game_id             bigint
                , status              integer
                , minutes_played      integer
                , user_rating         integer
                , deleted_at          timestamp with time zone
            );
        END IF;
    END
$$;";
        
        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS game_list_data;
    END
$$;";

        Execute.Sql(sql);
    }
}