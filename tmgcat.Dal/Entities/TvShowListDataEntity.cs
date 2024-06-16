namespace tmgcat.Dal.Entities;

public class TvShowListDataEntity
{
    public long Id { get; init; }
    public required long UserId { get; init; }
    public required long TvShowId { get; init; }
    public string? PosterPath { get; init; }
    public required int Status { get; init; }
    public required int EpisodesWatched { get; init; }
    public int? UserRating { get; init; }
    public DateTimeOffset? DeletedAt { get; init; }
}