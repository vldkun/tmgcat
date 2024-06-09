namespace tmgcat.Bll.Models.Comments;

public class GetCommentModel
{
    public required long Id { get; init; }
    public required int PageType { get; init; }
    public required long PageId { get; init; }
    public required string Content { get; init; }
    public long? ParentCommentId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public required long CreatedByUserId { get; init; }
    public required string UserName { get; init; }
    public string? UserProfilePicturePath { get; init; }
    public required int Level { get; init; }
    public required long RootCommentId { get; init; }
}