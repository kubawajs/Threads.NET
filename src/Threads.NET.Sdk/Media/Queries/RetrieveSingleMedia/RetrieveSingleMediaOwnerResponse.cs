namespace Threads.NET.Sdk.Media.Queries.RetrieveSingleMedia;

public sealed record RetrieveSingleMediaOwnerResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set;}
}
