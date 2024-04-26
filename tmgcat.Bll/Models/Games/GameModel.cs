namespace tmgcat.Bll.Models.Games;

public class GameModel
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required int IgdbId { get; init; }
    public DateTimeOffset? ReleasedAt { get; init; }
    public string? Platforms { get; init; }
    public string? CoverPath { get; init; }
    public required int Status { get; init; }
    public string? Genres { get; init; }
    public string? Category { get; init; }
    public string? InvolvedCompanies { get; init; }
}