using Threads.NET.Sdk.Profiles.Queries.RetrieveUserProfile;

namespace Threads.NET.Sdk.Profiles;

public interface IProfilesClient
{
    /// <summary>
    /// Use the GET /{threads-user-id}?fields=id,username,... endpoint to return profile information about a Threads user.
    /// </summary>
    Task<RetrieveUserProfileResponse> RetrieveUserProfileAsync(RetrieveUserProfileRequest request);
}
