namespace Threads.NET.Sdk.Posts.Responses;

internal sealed class CreateSingleThreadPostResponse
{
    [JsonPropertyName("id")]
    public string? PostId { get; set; }
}
