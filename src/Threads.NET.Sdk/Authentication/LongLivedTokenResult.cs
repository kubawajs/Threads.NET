public record LongLivedTokenResult(string AccessToken, string TokenType, int ExpiresIn)
{
    /// <summary>
    /// Long-lived user access token.
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; } = AccessToken;

    /// <summary>
    /// Token type: bearer.
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; } = TokenType;

    /// <summary>
    /// Number of seconds until token expires.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; } = ExpiresIn;
}