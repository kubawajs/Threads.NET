namespace Threads.NET.Sdk.Authentication;

public record AuthenticationResult(string AccessToken, long UserId)
{
    /// <summary>
    /// The user’s short-lived Threads user access token, which your app can use to access Threads API endpoints.
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; } = AccessToken;

    /// <summary>
    /// User's ID.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; } = UserId;
}