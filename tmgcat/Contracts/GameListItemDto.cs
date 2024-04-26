using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class GameListItemDto
{
    [JsonPropertyName("game_id")] public required long GameId { get; set; }
    [JsonPropertyName("title")] public required string Title { get; set; }
    [JsonPropertyName("cover_path")] public string? CoverPath { get; set; }
    [JsonPropertyName("status")] public required string Status { get; set; }
    [JsonPropertyName("minutes_played")] public required int MinutesPlayed { get; set; }
    [JsonPropertyName("user_rating")] public int? UserRating { get; set; }
}