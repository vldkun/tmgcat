namespace tmgcat.Bll.Models.Movies;

public class MovieListItemModel
{
    public required long MovieId { get; init; }
    public required string TitleEn { get; init; }
    public required string TitleRu { get; init; }
    public string? PosterPath { get; init; }
    public required int Status { get; init; }
    public int? UserRating { get; init; }
    public required int MovieStatus { get; init; }
}