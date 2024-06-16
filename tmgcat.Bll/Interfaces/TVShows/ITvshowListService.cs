using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Interfaces.TVShows;

public interface ITvShowListService
{
    Task<TvShowListItemModel[]> GetList(long userId, CancellationToken token);
    Task AddToList(AddTvShowListItemModel[] tvShows, CancellationToken token);
    Task ChangeUserRating(long userId, long tvShowId, int rating, CancellationToken token);
    Task ChangeEpisodesNumber(long userId, long tvShowId, int episodes, CancellationToken token);
    Task ChangeUserStatus(long userId, long tvShowId, int status, CancellationToken token);
    Task<int> GetUserStatus(long userId, long tvShowId, CancellationToken token);
}