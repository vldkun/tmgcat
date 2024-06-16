using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class MovieDto
{
    [JsonPropertyName("movie_id")] public required long MovieId { get; init; }
    [JsonPropertyName("title_en")] public required string TitleEn { get; init; }
    [JsonPropertyName("title_ru")] public required string TitleRu { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("imdb_id")] public required int ImdbId { get; init; }
    [JsonPropertyName("tmdb_id")] public required int TmdbId { get; init; }
    [JsonPropertyName("released_at")] public DateTimeOffset? ReleasedAt { get; init; }
    [JsonPropertyName("poster_path")] public string? PosterPath { get; init; }
    [JsonPropertyName("runtime_minutes")] public required int RuntimeMinutes { get; init; }
    [JsonPropertyName("status")] public required string Status { get; set; }
    [JsonPropertyName("genres")] public string? Genres { get; init; }
    [JsonPropertyName("creators")] public string? Creators { get; init; }
    [JsonPropertyName("imdb_rating")] public string? ImdbRating { get; init; }
    [JsonPropertyName("kp_rating")] public string? KpRating { get; init; }
    [JsonPropertyName("user_rating")] public string? UserRating { get; init; }
}
