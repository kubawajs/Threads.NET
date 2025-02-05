namespace Threads.NET.Sdk.Posts.Commands.CreateItemContainer;

public sealed record CreateItemContainerResponse
{
    [JsonPropertyName("id")]
    public string? PostId { get; set; }
}
