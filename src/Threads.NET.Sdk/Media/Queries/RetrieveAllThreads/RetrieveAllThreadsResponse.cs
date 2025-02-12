namespace Threads.NET.Sdk.Media.Queries.RetrieveAllThreads;

public sealed record RetrieveAllThreadsResponse
{
    [JsonPropertyName("data")]
    public IEnumerable<RetrieveAllThreadsThreadResponse> Data { get; set; } = [];
}
