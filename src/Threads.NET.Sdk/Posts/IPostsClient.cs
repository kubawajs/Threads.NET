using Threads.NET.Sdk.Posts.Commands.CreateSingleThreadPost;

namespace Threads.NET.Sdk.Posts;
public interface IPostsClient
{
    /// <summary>
    /// Creates single thread post media container.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<CreateSingleThreadPostResponse> CreateSingleThreadPostAsync(CreateSingleThreadPost request);
}
