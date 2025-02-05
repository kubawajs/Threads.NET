using Threads.NET.Sdk.Authentication;
using Threads.NET.Sdk.Posts.Commands.CreateItemContainer;
using Threads.NET.Sdk.Posts.Commands.CreateSingleThreadPost;
using Threads.NET.Sdk.Posts.Commands.PublishMediaContainer;

namespace Threads.NET.Sdk.Client;

internal sealed class ThreadsClient(IMediator mediator, IThreadsAuthenticationClient authenticationClient)
    : IThreadsClient
{

    public async Task<AuthenticationResult> ExchangeCodeForTokenAsync(string code)
        => await authenticationClient.ExchangeCodeForTokenAsync(code);

    public async Task<LongLivedTokenResult> ExchangeForLongLivedTokenAsync(string accessToken)
        => await authenticationClient.ExchangeForLongLivedTokenAsync(accessToken);

    public string GetAuthorizationUrl(IEnumerable<string> scopes, string? state = null)
        => authenticationClient.GetAuthorizationUrl(scopes, state);

    public async Task<LongLivedTokenResult> RefreshLongLivedTokenAsync(string longLivedToken)
        => await authenticationClient.RefreshLongLivedTokenAsync(longLivedToken);

    public async Task<CreateSingleThreadPostResponse> CreateSingleThreadPostAsync(CreateSingleThreadPostRequest request)
        => await mediator.Send(request);

    public async Task<PublishMediaContainerResponse> PublishMediaContainerAsync(PublishMediaContainerRequest request)
        => await mediator.Send(request);
    public async Task<CreateItemContainerResponse> CreateItemContainerAsync(CreateItemContainerRequest request)
        => await mediator.Send(request);
}