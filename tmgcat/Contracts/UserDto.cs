using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class UserDto
{
    [JsonPropertyName("user_id")] public required long UserId { get; init; }
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("info")] public string? Info { get; init; }
    [JsonPropertyName("profile_picture_path")] public string? ProfilePicturePath { get; init; }
    [JsonPropertyName("created_at")] public DateTimeOffset? CreatedAt { get; init; }
}