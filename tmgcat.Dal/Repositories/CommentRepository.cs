using Dapper;
using Microsoft.Extensions.Options;
using tmgcat.Bll.Enums;
using tmgcat.Bll.Interfaces;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Models.Comments;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class CommentRepository : PgRepository, ICommentRepository
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public CommentRepository(
        IDateTimeProvider dateTimeProvider,
        IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<GetCommentModel[]> GetAsync(long pageId, PageType pageType, CancellationToken token)
    {
        var baseSql = @"
   with recursive comments_tree
       as (select c.id
                , c.parent_comment_id
                , 1 as level
                , c.created_at::text || '/' as order_sequence
             from comments c
            where c.parent_comment_id is null
            union all
           select c.id
                , c.parent_comment_id
                , ct.level + 1 as level
                , ct.order_sequence || c.created_at::text || '/'
             from comments c
             join comments_tree ct on ct.id = c.parent_comment_id)
   select c.id as id
        , c.page_type as page_type
        , c.page_id as page_id
        , c.content as content
        , c.parent_comment_id as parent_comment_id
        , c.created_at as created_at
        , c.created_by_user_id as created_by_user_id
        , u.name as user_name
        , u.profile_picture_path as user_profile_picture_path        
        , ct.level as level
        , (with recursive comments_tree2
             as (select c1.id
                      , c1.parent_comment_id
                      , 1 as level
                   from comments c1
                  where c1.id = c.id
                  union all
                 select c1.id
                      , c1.parent_comment_id
                      , ct2.level + 1 as level
                   from comments c1
                   join comments_tree2 ct2 on ct2.parent_comment_id = c1.id)
                 select ct2.id
                   from comments_tree2 ct2
                  order by ct2.level desc
                  limit 1) as root_comment_id
     from comments c
     join comments_tree as ct on ct.id = c.id
left join users u on u.id = c.created_by_user_id
    where c.page_id = @PageId
      and c.page_type = @PageType
    order by ct.order_sequence
";

        var cmd = new CommandDefinition(
            baseSql,
            new
            {
                PageId = pageId,
                PageType = (int)pageType,
            },
            commandTimeout: DefaultTimeoutInSeconds,
            cancellationToken: token);

        await using var connection = await GetConnection();
        return (await connection.QueryAsync<GetCommentModel>(cmd))
            .ToArray();
    }

    public async Task AddAsync(CommentModel comment, CancellationToken token)
    {
        const string sqlQuery = @"
   insert into comments (page_type, page_id, parent_comment_id, content, created_at, created_by_user_id) 
   values (@PageType, @PageId, @ParentCommentId, @Content, @CreatedAt, @CreatedByUserId)
returning id;
";

        await using var connection = await GetConnection();
        var id = await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Content = comment.Content,
                    ParentCommentId = comment.ParentCommentId,
                    PageId = comment.PageId,
                    PageType = comment.PageType,
                    CreatedByUserId = comment.CreatedByUserId,
                    CreatedAt = _dateTimeProvider.GetCurrentTime()
                },
                cancellationToken: token));

        var res = id.ToArray().First();
    }
}