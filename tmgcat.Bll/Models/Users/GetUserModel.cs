namespace tmgcat.Bll.Models.Users;

public record GetUserModel
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public string? Info { get; init; }
    public string? ProfilePicturePath { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? BlockedAt { get; init; }
}