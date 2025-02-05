using Threads.NET.Sdk.Authorization;

namespace Threads.NET.Sdk.Profiles.Queries.RetrieveUserProfile;

public sealed class RetrieveUserProfileRequest(
    string AccessToken,
    string UserId)
    : IAuthorizedRequest<RetrieveUserProfileResponse>
{
    public string AccessToken { get; } = AccessToken;

    public string UserId { get; } = UserId;
}
