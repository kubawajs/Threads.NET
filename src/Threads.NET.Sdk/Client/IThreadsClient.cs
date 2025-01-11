using Threads.NET.Sdk.Authentication;
using Threads.NET.Sdk.Posts;

namespace Threads.NET.Sdk.Client;

public interface IThreadsClient
    : IThreadsAuthenticationClient, IPostsClient
{
}