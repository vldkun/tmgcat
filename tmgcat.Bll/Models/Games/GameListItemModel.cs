namespace tmgcat.Bll.Models.Games;

public record GameListItemModel
{
    public required long GameId { get; init; }
    public required string Title { get; init; }
    public string? CoverPath { get; init; }
    public required int Status { get; init; }
    public required int MinutesPlayed { get; init; }
    public int? UserRating { get; init; }
    public required int GameStatus { get; init; }
}