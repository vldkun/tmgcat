namespace tmgcat.Bll.Models.Movies;

public class GetMovieTitleModel
{
    public required long Id { get; init; }
    public required string TitleEn { get; init; }
    public required string TitleRu { get; init; }
    public DateTimeOffset? ReleasedAt { get; init; }
    public string? PosterPath { get; init; }
}