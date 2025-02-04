namespace Threads.NET.Sdk.Posts.Commands.PublishMediaContainer;
public sealed class PublishMediaContainerRequest(string CreationId, string AccessToken, string UserId)
    : IAuthorizedRequest<PublishMediaContainerResponse>
{
    public string AccessToken { get; } = AccessToken;

    public string UserId { get; } = UserId;

    /// <summary>
    /// Identifier of the Threads media container created from the /threads endpoint.
    /// </summary>
    public string CreationId { get; } = CreationId;
}