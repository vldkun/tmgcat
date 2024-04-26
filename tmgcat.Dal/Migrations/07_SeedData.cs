using FluentMigrator;

namespace tmgcat.Dal.Infrastructure;

[Migration(07, TransactionBehavior.None)]
public class SeedData : Migration
{
    public override void Up()
    {
        const string games = @"
        insert into games (id, title, description, igdb_id, released_at, platforms, genres, cover_path, status, category, involved_companies)
        values (1, 'Fallout', '1997 role-playing video game developed and published by Interplay Productions.', '13', '1997-09-30', 'Steam', 'RPG', 'link', 0, 'Game', 'Interplay')
             , (2, 'Fallout 2', '1998 role-playing video game developed by Black Isle Studios and published by Interplay Productions.', '14', '1998-10-29', 'Steam', 'RPG', 'link', 0, 'Game', 'Interplay')
             , (3, 'Fallout 3', 'The third game in the Fallout series, Fallout 3 is a singleplayer action role-playing game (RPG) set in a post-apocalyptic Washington DC.', '15', '2008-10-28', 'Steam', 'RPG', 'link', 0, 'Game', 'Bethesda')
             , (4, 'Fallout: New Vegas', 'In this first-person Western RPG, the player takes on the role of Courier 6, barely surviving after being robbed of their cargo, shot and put into a shallow grave by a New Vegas mob boss.', '15', '2010-10-29', 'Steam', 'RPG', 'link', 0, 'Game', 'Bethesda')
             , (5, 'Fallout 4', 'Bethesda Game Studios welcome you to the world of Fallout 4, their most ambitious game ever, and the next generation of open-world gaming.', '15', '2015-11-09', 'Steam', 'RPG', 'link', 0, 'Game', 'Bethesda');
        ";

        //Execute.Sql(games);

        const string gameList = @"
        insert into game_list (id, user_id, game_id, status, minutes_played, user_rating)
        values (1, 1, 1, 0, 0, null)
             , (2, 1, 2, 2, 34, 10)
             , (3, 1, 3, 1, 345, 7)
             , (4, 1, 4, 2, 246, 9)
             , (5, 1, 5, 3, 352, 8);
        ";

        //Execute.Sql(gameList);

        const string users = @"
        insert into users (id,  name, email, password, info, profile_picture_path, created_at, blocked_at)
        values (1, 'User1', 'safwf@gmail.com', '123', 'asdf', 'saff', '2024-01-01', null)
             , (2, 'User2', 'safwf@gmail.com', '123', 'asfd', 'asdf', '2024-01-02', null)
             , (3, 'User3', 'safsf@gmail.com', '123', 'asdf', 'asdf', '2024-01-03', null);
        ";

        //Execute.Sql(users);
    }

    public override void Down()
    {
        Delete.FromTable("games").AllRows();
        Delete.FromTable("users").AllRows();
        Delete.FromTable("game_list").AllRows();
    }
}