using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Interfaces.TVShows;

public interface ITvshowListService
{
    Task<TvshowListItemModel[]> GetList(long userId, CancellationToken token);
    Task AddToList(AddTvshowListItemModel[] tvshows, CancellationToken token);
    Task ChangeUserRating(long userId, long tvshowId, int rating, CancellationToken token);
    Task ChangeEpisodesNumber(long userId, long tvshowId, int episodes, CancellationToken token);
    Task ChangeUserStatus(long userId, long tvshowId, int status, CancellationToken token);
}