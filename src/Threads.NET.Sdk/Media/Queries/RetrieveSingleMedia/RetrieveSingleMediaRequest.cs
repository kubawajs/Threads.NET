namespace Threads.NET.Sdk.Media.Queries.RetrieveSingleMedia;

public sealed class RetrieveSingleMediaRequest(
    string MediaId,
    string AccessToken)
    : IRequest<RetrieveSingleMediaResponse>
{
    public string AccessToken { get; } = AccessToken;

    /// <summary>
    /// The ID of the media to retrieve.
    /// </summary>
    public string MediaId { get; } = MediaId;
}
