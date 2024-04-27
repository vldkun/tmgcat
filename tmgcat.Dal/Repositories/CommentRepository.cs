using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Models.Comments;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class CommentRepository : PgRepository, ICommentRepository
{
    public CommentRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public Task<CommentModel[]> Get(string page, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task Add(CommentModel comment, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}