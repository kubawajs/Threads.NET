namespace Threads.NET.Sdk.Posts.Commands.PublishMediaContainer;

public sealed record PublishMediaContainerResponse
{
    /// <summary>
    /// Threads Media ID
    /// </summary>
    [JsonPropertyName("id")]
    public string? MediaContainerId { get; set; }
}
