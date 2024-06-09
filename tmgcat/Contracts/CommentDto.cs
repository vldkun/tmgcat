using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class CommentDto
{
    [JsonPropertyName("id")] public required long Id { get; init; }
    [JsonPropertyName("page_type")] public required int PageType { get; init; }
    [JsonPropertyName("page_id")] public required long PageId { get; init; }
    [JsonPropertyName("content")] public required string Content { get; init; }

    [JsonPropertyName("parent_comment_id")]
    public long? ParentCommentId { get; init; }

    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("created_by_user_id")]
    public required long CreatedByUserId { get; init; }

    [JsonPropertyName("user_name")] public required string UserName { get; init; }

    [JsonPropertyName("user_profile_picture_path")]
    public string? UserProfilePicturePath { get; init; }

    [JsonPropertyName("level")] public required int Level { get; init; }
    [JsonPropertyName("root_comment_id")] public required long RootCommentId { get; init; }
}