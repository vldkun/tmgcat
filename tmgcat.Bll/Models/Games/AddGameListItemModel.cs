namespace tmgcat.Bll.Models.Games;

public class AddGameListItemModel
{
    public required long UserId { get; init; }
    public required long GameId { get; init; }
    public required int Status { get; init; }
    public required int MinutesPlayed { get; init; }
    public int? UserRating { get; init; }
    public DateTimeOffset? DeletedAt { get; init; }
}