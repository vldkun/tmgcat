namespace tmgcat.Dal.Entities;

public class MovieListDataEntity
{
    public long Id { get; init; }
    public required long UserId { get; init; }
    public required long MovieId { get; init; }
    public string? PosterPath { get; init; }
    public required int Status { get; init; }
    public int? UserRating { get; init; }
    public DateTimeOffset? DeletedAt { get; init; }
}