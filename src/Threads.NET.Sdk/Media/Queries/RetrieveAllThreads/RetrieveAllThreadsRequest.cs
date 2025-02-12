using Threads.NET.Sdk.Authorization;

namespace Threads.NET.Sdk.Media.Queries.RetrieveAllThreads;
public sealed record RetrieveAllThreadsRequest(
    string AccessToken,
    string UserId)
    : IAuthorizedRequest<RetrieveAllThreadsResponse>
{
    public string AccessToken { get; } = AccessToken;

    public string UserId { get; } = UserId;
}
