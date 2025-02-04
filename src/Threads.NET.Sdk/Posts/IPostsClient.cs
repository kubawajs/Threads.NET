using Threads.NET.Sdk.Posts.Commands.CreateSingleThreadPost;
using Threads.NET.Sdk.Posts.Commands.PublishMediaContainer;

namespace Threads.NET.Sdk.Posts;
public interface IPostsClient
{
    /// <summary>
    /// Creates single thread post media container.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<CreateSingleThreadPostResponse> CreateSingleThreadPostAsync(CreateSingleThreadPostRequest request);

    /// <summary>
    /// Publishes a Threads Media Container.
    /// </summary>
    public Task<PublishMediaContainerResponse> PublishMediaContainerAsync(PublishMediaContainerRequest request);
}
