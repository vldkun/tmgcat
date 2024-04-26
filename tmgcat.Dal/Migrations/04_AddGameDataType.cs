using FluentMigrator;

namespace tmgcat.Dal.Infrastructure;

[Migration(04, TransactionBehavior.None)]
public class AddGameDataType : Migration
{
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'game_data') THEN
            CREATE TYPE game_data as
            (
                  id                  bigint
                , title               text
                , description         text
                , igdb_id             integer
                , released_at         timestamp with time zone
                , platforms           text
                , genres              text
                , cover_path          text
                , status              integer
                , category            text
                , involved_companies  text
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
        DROP TYPE IF EXISTS game_data;
    END
$$;";

        Execute.Sql(sql);
    }
}