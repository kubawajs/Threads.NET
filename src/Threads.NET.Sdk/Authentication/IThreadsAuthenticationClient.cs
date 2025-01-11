namespace Threads.NET.Sdk.Authentication;

internal interface IThreadsAuthenticationClient
{
    /// <summary>
    /// Generates the authorization URL for the OAuth flow.
    /// </summary>
    /// <param name="scopes">Collection of permission scopes being requested.</param>
    /// <param name="state">Optional state parameter for security validation.</param>
    /// <returns>The complete authorization URL to redirect the user to.</returns>
    string GetAuthorizationUrl(IEnumerable<string> scopes, string? state = null);

    /// <summary>
    /// Exchanges the authorization code for an access token.
    /// </summary>
    /// <param name="code">The authorization code received from the OAuth redirect.</param>
    /// <returns>Authentication result containing the access token and related information.</returns>
    Task<AuthenticationResult> ExchangeCodeForTokenAsync(string code);

    /// <summary>
    /// Exchanges a short-lived access token for a long-lived access token.
    /// </summary>
    /// <param name="accessToken">The short-lived access token to exchange.</param>
    /// <returns>Result containing the long-lived token and its expiration details.</returns>
    Task<LongLivedTokenResult> ExchangeForLongLivedTokenAsync(string accessToken);

    /// <summary>
    /// Refreshes an expired long-lived access token.
    /// </summary>
    /// <param name="longLivedToken">The expired long-lived token to refresh.</param>
    /// <returns>Result containing the new long-lived token and its expiration details.</returns>
    Task<LongLivedTokenResult> RefreshLongLivedTokenAsync(string longLivedToken);
}
