namespace tmgcat.Bll.Models.Comments;

public class AddCommentModel
{
    public required long PageId { get; init; }
    public required string Content { get; init; }
    public long? ParentCommentId { get; init; }
    public required long CreatedByUserId { get; init; }
}