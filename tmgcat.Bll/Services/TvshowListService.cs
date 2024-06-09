using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Services;

public class TvShowListService : ITvShowListService
{
    public Task<TvShowListItemModel[]> GetList(long userId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task AddToList(AddTvShowListItemModel[] tvShows, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task ChangeUserRating(long userId, long tvShowId, int rating, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task ChangeEpisodesNumber(long userId, long tvShowId, int episodes, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task ChangeUserStatus(long userId, long tvShowId, int status, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}