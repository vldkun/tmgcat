namespace tmgcat.Bll.Models.Movies;

public class GetMovieModel
{
    public required long Id { get; init; }
    public required string TitleEn { get; init; }
    public required string TitleRu { get; init; }
    public string? Description { get; init; }
    public required int ImdbId { get; init; }
    public required int TmdbId { get; init; }
    public DateTimeOffset? ReleasedAt { get; init; }
    public string? PosterPath { get; init; }
    public required int RuntimeMinutes { get; init; }
    public required int Status { get; init; }
    public string? Genres { get; init; }
    public string? Creators { get; init; }
    public decimal? ImdbRating { get; init; }
    public decimal? KpRating { get; init; }
    public decimal? UserRating { get; init; }
}