namespace Threads.NET.Sdk.Media.Queries;

public sealed record OwnerResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
