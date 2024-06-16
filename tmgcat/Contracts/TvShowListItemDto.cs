using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class TvShowListItemDto
{
    [JsonPropertyName("tvshow_id")] public required long TvShowId { get; set; }
    [JsonPropertyName("title_en")] public required string TitleEn { get; set; }
    [JsonPropertyName("title_ru")] public required string TitleRu { get; set; }
    [JsonPropertyName("poster_path")] public string? PosterPath { get; set; }
    [JsonPropertyName("episodes_watched")] public required int EpisodesWatched { get; init; }
    [JsonPropertyName("status")] public required string Status { get; set; }
    [JsonPropertyName("user_rating")] public int? UserRating { get; set; }
}