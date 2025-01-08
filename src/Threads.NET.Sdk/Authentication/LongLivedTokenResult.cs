using System.Text.Json.Serialization;

public class LongLivedTokenResult(string AccessToken, string TokenType, int ExpiresIn)
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; } = AccessToken;

    [JsonPropertyName("token_type")]
    public string TokenType { get; } = TokenType;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; } = ExpiresIn;
}