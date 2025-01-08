using System.Text.Json.Serialization;

namespace Threads.NET.Sdk.Authentication;

public record AuthenticationResult(string AccessToken, long UserId)
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; } = AccessToken;

    [JsonPropertyName("user_id")]
    public long UserId { get; } = UserId;
}
