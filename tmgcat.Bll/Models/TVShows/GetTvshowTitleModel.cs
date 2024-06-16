namespace tmgcat.Bll.Models.TVShows;

public class GetTvShowTitleModel
{
    public required long Id { get; init; }
    public required string TitleEn { get; init; }
    public required string TitleRu { get; init; }
    public DateTimeOffset? FirstAirDate { get; init; }
    public DateTimeOffset? LastAirDate { get; init; }
    public string? PosterPath { get; init; }
}