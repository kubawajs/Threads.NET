namespace Threads.NET.Sdk.Profiles.Queries.RetrieveUserProfile;

public sealed class RetrieveUserProfileResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("threads_profile_picture_url")]
    public string? ProfilePictureUrl { get; set; }

    [JsonPropertyName("threads_biography")]
    public string? Biography { get; set; }
}