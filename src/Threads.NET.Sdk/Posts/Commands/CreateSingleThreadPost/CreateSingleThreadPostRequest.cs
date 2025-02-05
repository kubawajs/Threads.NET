using Threads.NET.Sdk.Authorization;

namespace Threads.NET.Sdk.Posts.Commands.CreateSingleThreadPost;

/// <summary>
/// Use the POST /{threads-user-id}/threads endpoint to create a Threads media container.
/// </summary>
public sealed record CreateSingleThreadPostRequest(
    bool IsCarouselItem,
    string ImageUrl,
    SingleThreadPostMediaType MediaType,
    string VideoUrl,
    string Text,
    string AccessToken,
    string UserId)
    : IAuthorizedRequest<CreateSingleThreadPostResponse>
{
    public string AccessToken { get; } = AccessToken;

    public string UserId { get; } = UserId;

    /// <summary>
    /// Default value is false for single thread posts. Indicates an image or video that will appear in a carousel.
    /// </summary>
    public bool IsCarouselItem { get; } = IsCarouselItem;

    /// <summary>
    /// (For images only.) The path to the image. We will cURL your image using the URL provided so it must be on a public server.
    /// </summary>
    public string ImageUrl { get; } = ImageUrl;

    /// <summary>
    /// Set to either TEXT, IMAGE, or VIDEO. Indicates the current media type. Note: Type CAROUSEL is not available for single thread posts.
    /// </summary>
    public SingleThreadPostMediaType MediaType { get; } = MediaType;

    /// <summary>
    /// (For videos only.) Path to the video. We will cURL your video using the URL provided so it must be on a public server.
    /// </summary>
    public string VideoUrl { get; } = VideoUrl;

    /// <summary>
    /// The text associated with the post. The first URL included in the text field will be used as the link preview for the post. For text-only posts, this parameter is required.
    /// </summary>
    public string Text { get; } = Text;
}
