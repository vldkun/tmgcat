﻿using System.Text.Json.Serialization;

namespace tmgcat.App.Contracts;

public class MovieTitleDto
{
    [JsonPropertyName("id")] public required long Id { get; set; }
    [JsonPropertyName("title_en")] public required string TitleEn { get; init; }
    [JsonPropertyName("title_ru")] public required string TitleRu { get; init; }
    [JsonPropertyName("released_at")] public DateTimeOffset? ReleasedAt { get; set; }
    [JsonPropertyName("poster_path")] public string? PosterPath { get; set; }
}