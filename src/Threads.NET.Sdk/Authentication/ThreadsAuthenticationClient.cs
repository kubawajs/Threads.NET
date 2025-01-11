using Threads.NET.Sdk.Exceptions;

namespace Threads.NET.Sdk.Authentication;

internal sealed class ThreadsAuthenticationClient(
    IHttpClientFactory httpClientFactory,
    IOptions<ThreadsClientOptions> options) : IThreadsAuthenticationClient
{
    private readonly ThreadsClientOptions _options = options.Value;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Threads");

    public string GetAuthorizationUrl(IEnumerable<string> scopes, string? state = null)
    {
        var scopeString = string.Join(",", scopes);
        var queryParams = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["redirect_uri"] = _options.RedirectUri,
            ["scope"] = scopeString,
            ["response_type"] = "code"
        };

        if (!string.IsNullOrEmpty(state))
        {
            queryParams["state"] = state ?? "";
        }

        var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
        return $"https://threads.net/oauth/authorize?{queryString}"; // TODO: to constants
    }

    public async Task<AuthenticationResult> ExchangeCodeForTokenAsync(string code)
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["code"] = code,
            ["grant_type"] = "authorization_code",
            ["redirect_uri"] = _options.RedirectUri
        });

        var response = await _httpClient.PostAsync("oauth/access_token", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsAuthenticationException(responseContent);
        }

        var authenticationResult = JsonSerializer.Deserialize<AuthenticationResult>(responseContent);
        return authenticationResult is null
            ? throw new ThreadsDeserializationException(typeof(AuthenticationResult), responseContent)
            : authenticationResult;
    }

    public async Task<LongLivedTokenResult> ExchangeForLongLivedTokenAsync(string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["grant_type"] = "th_exchange_token",
            ["client_secret"] = _options.ClientSecret,
            ["access_token"] = accessToken
        };

        var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
        var response = await _httpClient.GetAsync($"access_token?{queryString}");
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsAuthenticationException(responseContent);
        }

        var tokenResult = JsonSerializer.Deserialize<LongLivedTokenResult>(responseContent);
        return tokenResult is null
            ? throw new ThreadsDeserializationException(typeof(LongLivedTokenResult), responseContent)
            : tokenResult;
    }

    public async Task<LongLivedTokenResult> RefreshLongLivedTokenAsync(string longLivedToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["grant_type"] = "th_refresh_token",
            ["access_token"] = longLivedToken
        };

        var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
        var response = await _httpClient.GetAsync($"refresh_access_token?{queryString}");
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsAuthenticationException(responseContent);
        }

        var tokenResult = JsonSerializer.Deserialize<LongLivedTokenResult>(responseContent);
        return tokenResult is null
            ? throw new ThreadsDeserializationException(typeof(LongLivedTokenResult), responseContent)
            : tokenResult;
    }
}
