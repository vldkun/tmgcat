using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class GameDto
{
    [JsonPropertyName("game_id")] public required long GameId { get; set; }
    [JsonPropertyName("title")] public required string Title { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("user_score")] public string? UserScore { get; set; }
    [JsonPropertyName("igdb_id")] public required int IgdbId { get; set; }
    [JsonPropertyName("released_at")] public DateTimeOffset? ReleasedAt { get; set; }
    [JsonPropertyName("platforms")] public string? Platforms { get; set; }
    [JsonPropertyName("cover_path")] public string? CoverPath { get; set; }
    [JsonPropertyName("status")] public required string Status { get; set; }
    [JsonPropertyName("genres")] public string? Genres { get; set; }
    [JsonPropertyName("category")] public string? Category { get; set; }

    [JsonPropertyName("involved_companies")]
    public string? InvolvedCompanies { get; set; }
}