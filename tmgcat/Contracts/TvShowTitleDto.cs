using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class TvShowTitleDto
{
    [JsonPropertyName("id")] public required long Id { get; set; }
    [JsonPropertyName("title_en")] public required string TitleEn { get; init; }
    [JsonPropertyName("title_ru")] public required string TitleRu { get; init; }
    [JsonPropertyName("first_air_date")] public DateTimeOffset? FirstAirDate { get; init; }
    [JsonPropertyName("last_air_date")] public DateTimeOffset? LastAirDate { get; init; }
    [JsonPropertyName("poster_path")] public string? PosterPath { get; set; }
}