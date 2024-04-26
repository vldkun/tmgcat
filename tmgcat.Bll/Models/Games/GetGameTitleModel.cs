namespace tmgcat.Bll.Models.Games;

public class GetGameTitleModel
{
    public required long Id { get; init; }
    public required string Title { get; init; }
    public DateTimeOffset? ReleasedAt { get; init; }
    public string? CoverPath { get; init; }
}