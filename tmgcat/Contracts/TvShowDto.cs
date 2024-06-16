using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class TvShowDto
{
    [JsonPropertyName("tvshow_id")] public required long TvShowId { get; init; }
    [JsonPropertyName("title_en")] public required string TitleEn { get; init; }
    [JsonPropertyName("title_ru")] public required string TitleRu { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("imdb_id")] public required int ImdbId { get; init; }
    [JsonPropertyName("tmdb_id")] public required int TmdbId { get; init; }
    [JsonPropertyName("first_air_date")] public DateTimeOffset? FirstAirDate { get; init; }
    [JsonPropertyName("last_air_date")] public DateTimeOffset? LastAirDate { get; init; }
    [JsonPropertyName("poster_path")] public string? PosterPath { get; init; }
    [JsonPropertyName("avg_ep_runtime")] public required int AvgEpRuntime { get; init; }
    [JsonPropertyName("episodes_number")] public required int EpisodesNumber { get; init; }
    [JsonPropertyName("seasons_number")] public required int SeasonsNumber { get; init; }
    [JsonPropertyName("status")] public required string Status { get; set; }
    [JsonPropertyName("genres")] public string? Genres { get; init; }
    [JsonPropertyName("creators")] public string? Creators { get; init; }
    [JsonPropertyName("imdb_rating")] public string? ImdbRating { get; init; }
    [JsonPropertyName("kp_rating")] public string? KpRating { get; init; }
    [JsonPropertyName("user_rating")] public string? UserRating { get; init; }
}