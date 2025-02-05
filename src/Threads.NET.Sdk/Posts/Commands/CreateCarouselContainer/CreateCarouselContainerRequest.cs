using Threads.NET.Sdk.Authorization;

namespace Threads.NET.Sdk.Posts.Commands.CreateCarouselContainer;

/// <summary>
/// Use the POST /{threads-user-id}/threads endpoint to create a carousel container.
/// </summary>
public sealed class CreateCarouselContainerRequest(
    string[] Children,
    string Text,
    string AccessToken,
    string UserId)
    : IAuthorizedRequest<CreateCarouselContainerResponse>
{
    public string AccessToken { get; } = AccessToken;

    public string UserId { get; } = UserId;

    /// <summary>
    /// A comma-separated list of up to 20 container IDs of each image and/or video that should appear in the published carousel.
    /// Carousels can have at least 2 and up to 20 total images or videos or a mix of the two.
    /// </summary>
    public string[] Children { get; } = Children;

    /// <summary>
    /// (Optional.) The text associated with the post.
    /// </summary>
    public string Text { get; } = Text;
}
