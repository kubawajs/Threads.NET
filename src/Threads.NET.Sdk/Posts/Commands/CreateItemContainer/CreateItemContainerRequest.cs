namespace Threads.NET.Sdk.Posts.Commands.CreateItemContainer;

public sealed class CreateItemContainerRequest(
    string ImageUrl,
    ItemContainerMediaType MediaType,
    string VideoUrl,
    bool IsCarouselItem,
    string AccessToken,
    string UserId)
    : IAuthorizedRequest<CreateItemContainerResponse>
{
    public string AccessToken { get; } = AccessToken;

    public string UserId { get; } = UserId;

    /// <summary>
    /// Set to true. Indicates that the image or video will appear in a carousel.
    /// </summary>
    public bool IsCarouselItem { get; } = IsCarouselItem;

    /// <summary>
    /// (For images only.) The path to the image. We will cURL your image using the passed in URL so it must be on a public server.
    /// </summary>
    public string ImageUrl { get; } = ImageUrl;

    /// <summary>
    /// Set to IMAGE or VIDEO. Indicates that the media is an image or a video.
    /// </summary>
    public ItemContainerMediaType MediaType { get; } = MediaType;

    /// <summary>
    /// (For videos only.) Path to the video. We will cURL your video using the passed in URL so it must be on a public server.
    /// </summary>
    public string VideoUrl { get; } = VideoUrl;
}
