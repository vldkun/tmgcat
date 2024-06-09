using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class GameTitleDto
{
    [JsonPropertyName("id")] public required long Id { get; set; }
    [JsonPropertyName("title")] public required string Title { get; set; }
    [JsonPropertyName("released_at")] public DateTimeOffset? ReleasedAt { get; set; }
    [JsonPropertyName("poster_path")] public string? PosterPath { get; set; }
}