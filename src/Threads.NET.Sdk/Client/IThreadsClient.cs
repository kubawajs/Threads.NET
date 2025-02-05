using Threads.NET.Sdk.Authentication;
using Threads.NET.Sdk.Posts;
using Threads.NET.Sdk.Profiles;

namespace Threads.NET.Sdk.Client;

public interface IThreadsClient
    : IThreadsAuthenticationClient, IPostsClient, IProfilesClient
{
}