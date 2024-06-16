namespace tmgcat.Bll.Models.TVShows;

public class TvShowListItemModel
{
    public required long TvShowId { get; init; }
    public required string TitleEn { get; init; }
    public required string TitleRu { get; init; }
    public string? PosterPath { get; init; }
    public required int Status { get; init; }
    public required int EpisodesWatched { get; init; }
    public int? UserRating { get; init; }
    public required int TvShowStatus { get; init; }
}