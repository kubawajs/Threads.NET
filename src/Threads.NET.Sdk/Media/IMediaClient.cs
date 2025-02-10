using Threads.NET.Sdk.Media.Queries.RetrieveSingleMedia;

namespace Threads.NET.Sdk.Media;

public interface IMediaClient
{
    /// <summary>
    /// use the GET /{threads-media-id} endpoint to return an individual Threads media object.
    /// </summary>
    Task<RetrieveSingleMediaResponse> RetrieveSingleMediaAsync(RetrieveSingleMediaRequest request);
}
