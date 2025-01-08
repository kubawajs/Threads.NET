namespace Threads.NET.Sdk.Authentication;

public interface IAuthenticationClient
{
    string GetAuthorizationUrl(IEnumerable<string> scopes, string? state = null);
    Task<AuthenticationResult> ExchangeCodeForTokenAsync(string code);
    Task<LongLivedTokenResult> ExchangeForLongLivedTokenAsync(string accessToken);
    Task<LongLivedTokenResult> RefreshLongLivedTokenAsync(string longLivedToken);
}
