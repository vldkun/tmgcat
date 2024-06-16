using FluentMigrator;

namespace tmgcat.Dal.Infrastructure;

[Migration(08, TransactionBehavior.None)]
public class NewTables : Migration
{
    public override void Up()
    {
        Create.Table("movies")
            .WithColumn("id").AsInt64().PrimaryKey("movies_pk").Identity()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("creators").AsString().Nullable()
            .WithColumn("title_ru").AsString().NotNullable()
            .WithColumn("title_en").AsString().NotNullable()
            .WithColumn("status").AsInt32().NotNullable()
            .WithColumn("poster_path").AsString().Nullable()
            .WithColumn("released_at").AsDateTimeOffset().Nullable()
            .WithColumn("imdb_id").AsInt32().NotNullable()
            .WithColumn("tmdb_id").AsInt32().NotNullable()
            .WithColumn("runtime_minutes").AsInt32().NotNullable()
            .WithColumn("genres").AsString().Nullable()
            .WithColumn("imdb_rating").AsDecimal().Nullable()
            .WithColumn("kp_rating").AsDecimal().Nullable();

        Create.Table("movie_list")
            .WithColumn("id").AsInt64().PrimaryKey("movie_list_pk").Identity()
            .WithColumn("user_id").AsInt64().NotNullable()
            .WithColumn("movie_id").AsInt64().NotNullable()
            .WithColumn("status").AsInt32().NotNullable()
            .WithColumn("user_rating").AsInt32().Nullable()
            .WithColumn("deleted_at").AsDateTimeOffset().Nullable();

        Create.Table("tvshows")
            .WithColumn("id").AsInt64().PrimaryKey("tvshows_pk").Identity()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("creators").AsString().Nullable()
            .WithColumn("title_ru").AsString().NotNullable()
            .WithColumn("title_en").AsString().NotNullable()
            .WithColumn("status").AsInt32().NotNullable()
            .WithColumn("poster_path").AsString().Nullable()
            .WithColumn("released_at").AsDateTimeOffset().Nullable()
            .WithColumn("first_air_date").AsDateTimeOffset().Nullable()
            .WithColumn("last_air_date").AsDateTimeOffset().Nullable()
            .WithColumn("imdb_id").AsInt32().NotNullable()
            .WithColumn("tmdb_id").AsInt32().NotNullable()
            .WithColumn("avg_ep_runtime").AsInt32().NotNullable()
            .WithColumn("episodes_number").AsInt32().NotNullable()
            .WithColumn("seasons_number").AsInt32().NotNullable()
            .WithColumn("genres").AsString().Nullable()
            .WithColumn("imdb_rating").AsDecimal().Nullable()
            .WithColumn("kp_rating").AsDecimal().Nullable();

        Create.Table("tvshow_list")
            .WithColumn("id").AsInt64().PrimaryKey("tvshow_pk").Identity()
            .WithColumn("user_id").AsInt64().NotNullable()
            .WithColumn("tvshow_id").AsInt64().NotNullable()
            .WithColumn("status").AsInt32().NotNullable()
            .WithColumn("episodes_watched").AsInt32().NotNullable()
            .WithColumn("user_rating").AsInt32().Nullable()
            .WithColumn("deleted_at").AsDateTimeOffset().Nullable();

        Create.Table("user_logs")
            .WithColumn("id").AsInt64().PrimaryKey("user_logs_pk").Identity()
            .WithColumn("title_type").AsInt32().NotNullable()
            .WithColumn("title_id").AsInt64().NotNullable()
            .WithColumn("content").AsString().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("user_id").AsInt64().NotNullable();

        Create.Table("friend_list")
            .WithColumn("id").AsInt64().PrimaryKey("friend_list_pk").Identity()
            .WithColumn("friend_id").AsInt64().NotNullable()
            .WithColumn("user_id").AsInt64().NotNullable();

        Create.Index()
            .OnTable("movie_list")
            .OnColumn("user_id").Ascending()
            .OnColumn("movie_id").Ascending();

        Create.Index()
            .OnTable("tvshow_list")
            .OnColumn("user_id").Ascending()
            .OnColumn("tvshow_id").Ascending();

        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'movie_data') THEN
            CREATE TYPE movie_data as
            (
                  id                  bigint
                , title_ru            text
                , title_en            text
                , description         text
                , imdb_id             integer
                , tmdb_id             integer
                , runtime_minutes     integer
                , released_at         timestamp with time zone
                , creators            text
                , genres              text
                , poster_path         text
                , status              integer
                , imdb_rating         decimal
                , kp_rating           decimal
            );
        END IF;
    END
$$;";

        Execute.Sql(sql);

        const string sql1 = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'tvshow_data') THEN
            CREATE TYPE tvshow_data as
            (
                  id                  bigint
                , title_ru            text
                , title_en            text
                , description         text
                , creators            text
                , imdb_id             integer
                , tmdb_id             integer
                , avg_ep_runtime      integer
                , episodes_number     integer
                , seasons_number      integer
                , released_at         timestamp with time zone
                , first_air_date      timestamp with time zone
                , last_air_date       timestamp with time zone
                , genres              text
                , poster_path         text
                , status              integer
                , imdb_rating         decimal
                , kp_rating           decimal
            );
        END IF;
    END
$$;";

        Execute.Sql(sql1);

        const string sql2 = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'movie_list_data') THEN
            CREATE TYPE movie_list_data as
            (
                  id                  bigint
                , user_id             bigint
                , movie_id            bigint
                , status              integer
                , user_rating         integer
                , deleted_at          timestamp with time zone
            );
        END IF;
    END
$$;";

        Execute.Sql(sql2);

        const string sql3 = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'tvshow_list_data') THEN
            CREATE TYPE tvshow_list_data as
            (
                  id                  bigint
                , user_id             bigint
                , tvshow_id           bigint
                , status              integer
                , episodes_watched    integer
                , user_rating         integer
                , deleted_at          timestamp with time zone
            );
        END IF;
    END
$$;";

        Execute.Sql(sql3);
    }

    public override void Down()
    {
        Delete.FromTable("movies").AllRows();
        Delete.FromTable("movie_list").AllRows();
        Delete.FromTable("tvshows").AllRows();
        Delete.FromTable("tvshow_list").AllRows();
        Delete.FromTable("friend_list").AllRows();
        Delete.FromTable("user_logs").AllRows();
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS movie_list_data;
    END
$$;";

        Execute.Sql(sql);
        const string sql1 = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS tvshow_list_data;
    END
$$;";

        Execute.Sql(sql1);
        const string sql2 = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS movie_data;
    END
$$;";

        Execute.Sql(sql2);
        const string sql3 = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS tvshow_data;
    END
$$;";

        Execute.Sql(sql3);
    }
}