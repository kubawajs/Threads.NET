using Threads.NET.Sdk.Media.Queries.RetrieveAllThreads;
using Threads.NET.Sdk.Media.Queries.RetrieveSingleMedia;

namespace Threads.NET.Sdk.Media;

public interface IMediaClient
{
    /// <summary>
    /// Use the GET /{threads-media-id} endpoint to return an individual Threads media object.
    /// </summary>
    Task<RetrieveSingleMediaResponse> RetrieveSingleMediaAsync(RetrieveSingleMediaRequest request);

    /// <summary>
    /// Use the GET /{threads-user-id}/threads endpoint to return a paginated list of all threads created by a user.
    /// </summary>
    Task<RetrieveAllThreadsResponse> RetrieveAllThreadsAsync(RetrieveAllThreadsRequest request);
}
