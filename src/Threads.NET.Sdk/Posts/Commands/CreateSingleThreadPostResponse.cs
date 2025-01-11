namespace Threads.NET.Sdk.Posts.Commands;

public sealed record CreateSingleThreadPostResponse
{
    [JsonPropertyName("id")]
    public string? PostId { get; set; }
}
