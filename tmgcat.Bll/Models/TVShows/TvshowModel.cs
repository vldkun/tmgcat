namespace tmgcat.Bll.Models.TVShows;

public class TvShowModel
{
    public required string TitleEn { get; init; }
    public required string TitleRu { get; init; }
    public string? Description { get; init; }
    public required int ImdbId { get; init; }
    public required int TmdbId { get; init; }
    public DateTimeOffset? FirstAirDate { get; init; }
    public DateTimeOffset? LastAirDate { get; init; }
    public string? PosterPath { get; init; }
    public required int AvgEpRuntime { get; init; }
    public required int EpisodesNumber { get; init; }
    public required int SeasonsNumber { get; init; }
    public required int Status { get; init; }
    public string? Genres { get; init; }
    public string? Creators { get; init; }
    public decimal ImdbRating { get; init; }
    public decimal KpRating { get; init; }
}