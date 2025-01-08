using System.Text.Json;
using Threads.NET.Sdk.Exceptions;

namespace Threads.NET.Sdk.Authentication;

internal sealed class AuthenticationClient(
    IHttpClientFactory httpClientFactory,
    string clientId,
    string clientSecret,
    string redirectUri) : IAuthenticationClient
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    private readonly string _clientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
    private readonly string _clientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
    private readonly string _redirectUri = redirectUri ?? throw new ArgumentNullException(nameof(redirectUri));

    public string GetAuthorizationUrl(IEnumerable<string> scopes, string? state = null)
    {
        var scopeString = string.Join(",", scopes);
        var queryParams = new Dictionary<string, string>
        {
            ["client_id"] = _clientId,
            ["redirect_uri"] = _redirectUri,
            ["scope"] = scopeString,
            ["response_type"] = "code"
        };

        if (!string.IsNullOrEmpty(state))
        {
            queryParams["state"] = state ?? "";
        }

        var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
        return $"https://threads.net/oauth/authorize?{queryString}";
    }

    public async Task<AuthenticationResult> ExchangeCodeForTokenAsync(string code)
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["code"] = code,
            ["grant_type"] = "authorization_code",
            ["redirect_uri"] = _redirectUri
        });

        using var client = _httpClientFactory.CreateClient("ThreadsAuth");
        var response = await client.PostAsync("https://graph.threads.net/oauth/access_token", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsAuthenticationException(responseContent);
        }

        // Deserialize the response
        return JsonSerializer.Deserialize<AuthenticationResult>(responseContent);
    }
}
