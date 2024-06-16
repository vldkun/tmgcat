namespace tmgcat.Bll.Models.Users;

public class UserLogModel
{
    public required int TitleType { get; init; }
    public required long TitleId { get; init; }
    public required long UserId { get; init; }
    public required string Content { get; init; }
    public string? Title { get; set; }
    public string? PosterPath { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
}