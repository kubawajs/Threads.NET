namespace Threads.NET.Sdk.Posts.Commands.CreateSingleThreadPost;

public sealed record CreateSingleThreadPostResponse
{
    [JsonPropertyName("id")]
    public string? PostId { get; set; }
}
