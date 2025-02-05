using Threads.NET.Sdk.Posts.Commands.CreateItemContainer;
using Threads.NET.Sdk.Posts.Commands.CreateSingleThreadPost;
using Threads.NET.Sdk.Posts.Commands.PublishMediaContainer;

namespace Threads.NET.Sdk.Posts;

public interface IPostsClient
{
    /// <summary>
    /// Creates single thread post media container.
    /// </summary>
    public Task<CreateSingleThreadPostResponse> CreateSingleThreadPostAsync(CreateSingleThreadPostRequest request);

    /// <summary>
    /// Publishes a Threads Media Container.
    /// </summary>
    public Task<PublishMediaContainerResponse> PublishMediaContainerAsync(PublishMediaContainerRequest request);

    /// <summary>
    /// Create an item container for the image or video that will appear in a carousel.
    /// </summary>
    public Task<CreateItemContainerResponse> CreateItemContainerAsync(CreateItemContainerRequest request);
}
