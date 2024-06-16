namespace tmgcat.Bll.Models.Movies;

public class AddMovieListItemModel
{
    public required long UserId { get; init; }
    public required long MovieId { get; init; }
    public required int Status { get; init; }
    public int? UserRating { get; init; }
    public DateTimeOffset? DeletedAt { get; init; }
}