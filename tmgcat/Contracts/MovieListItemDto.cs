using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class MovieListItemDto
{
    [JsonPropertyName("movie_id")] public required long MovieId { get; set; }
    [JsonPropertyName("title_en")] public required string TitleEn { get; set; }
    [JsonPropertyName("title_ru")] public required string TitleRu { get; set; }
    [JsonPropertyName("poster_path")] public string? PosterPath { get; set; }
    [JsonPropertyName("status")] public required string Status { get; set; }
    [JsonPropertyName("user_rating")] public int? UserRating { get; set; }
}