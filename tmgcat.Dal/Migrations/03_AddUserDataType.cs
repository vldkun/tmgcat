using FluentMigrator;

namespace tmgcat.Dal.Infrastructure;

[Migration(03, TransactionBehavior.None)]
public class AddUserDataType : Migration
{
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'user_data') THEN
            CREATE TYPE user_data as
            (
                  id                     bigint
                , name                   text
                , info                   text
                , profile_picture_path   text
                , created_at             timestamp with time zone
                , blocked_at             timestamp with time zone
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
        DROP TYPE IF EXISTS user_data;
    END
$$;";

        Execute.Sql(sql);
    }
}