using FluentMigrator;

namespace tmgcat.Dal.Infrastructure;

[Migration(07, TransactionBehavior.None)]
public class SeedData : Migration
{
    public override void Up()
    {
        const string games = @"
        insert into games (title, description, igdb_id, released_at, platforms, genres, cover_path, status, category, involved_companies)
        values ('Fallout', '1997 role-playing video game developed and published by Interplay Productions.', '13', '1997-09-30', 'Steam', 'RPG', 'https://images.igdb.com/igdb/image/upload/t_cover_big/co1ybn.png', 0, 'Game', 'Interplay')
             , ('Fallout 2', '1998 role-playing video game developed by Black Isle Studios and published by Interplay Productions.', '14', '1998-10-29', 'Steam', 'RPG', 'https://images.igdb.com/igdb/image/upload/t_cover_big/co50kv.png', 0, 'Game', 'Interplay')
             , ('Fallout 3', 'The third game in the Fallout series, Fallout 3 is a singleplayer action role-playing game (RPG) set in a post-apocalyptic Washington DC.', '15', '2008-10-28', 'Steam', 'RPG', 'https://images.igdb.com/igdb/image/upload/t_cover_big/co1ycw.png', 0, 'Game', 'Bethesda')
             , ('Fallout: New Vegas', 'In this first-person Western RPG, the player takes on the role of Courier 6, barely surviving after being robbed of their cargo, shot and put into a shallow grave by a New Vegas mob boss.', '15', '2010-10-29', 'Steam', 'RPG', 'https://www.igdb.com/games/fallout-new-vegas', 0, 'Game', 'Bethesda')
             , ('Fallout 4', 'Bethesda Game Studios welcome you to the world of Fallout 4, their most ambitious game ever, and the next generation of open-world gaming.', '15', '2015-11-09', 'Steam', 'RPG', 'https://www.igdb.com/games/fallout-4', 0, 'Game', 'Bethesda');
        ";

        Execute.Sql(games);

        const string gameList = @"
        insert into game_list (user_id, game_id, status, minutes_played, user_rating)
        values (1, 2, 2, 34, 10)
             , (1, 3, 1, 345, 7)
             , (1, 4, 2, 246, 9)
             , (1, 5, 3, 352, 8);
        ";

        Execute.Sql(gameList);

        const string users = @"
        insert into users (name, email, password, info, profile_picture_path, created_at, blocked_at)
        values ('Vlad', 'vladkuzmin997@gmail.com', '123', 'The Creator', 'https://sun9-48.userapi.com/impg/TSvSYqrvAMQ1unoASYXuMcnFwUFiPZTh1aTRYw/VQDgRWolW34.jpg?size=467x467&quality=95&sign=3e8ab9686d8945c1e7bed31df5d174d0&type=album', '2024-01-01', null)
             , ('Evil Vlad', 'vladkuzmin9972@gmail.com', '123', 'The Devil', null, '2024-01-02', null);
        ";

        Execute.Sql(users);
    }

    public override void Down()
    {
        Delete.FromTable("games").AllRows();
        Delete.FromTable("users").AllRows();
        Delete.FromTable("game_list").AllRows();
    }
}