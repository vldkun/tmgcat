using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Services;

public class TvshowListService : ITvshowListService
{
    public Task<TvshowListItemModel[]> GetList(long userId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task AddToList(AddTvshowListItemModel[] tvshows, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task ChangeUserRating(long userId, long tvshowId, int rating, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task ChangeEpisodesNumber(long userId, long tvshowId, int episodes, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task ChangeUserStatus(long userId, long tvshowId, int status, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}