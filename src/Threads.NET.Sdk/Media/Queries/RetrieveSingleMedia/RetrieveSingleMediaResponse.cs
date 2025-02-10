using Threads.NET.Sdk.Authorization;
using Threads.NET.Sdk.Profiles.Queries.RetrieveUserProfile;

namespace Threads.NET.Sdk.Media.Queries.RetrieveSingleMedia;

public sealed record RetrieveSingleMediaResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("media_product_type")]
    public string? MediaProductType { get; set; }

    [JsonPropertyName("media_type")]
    public string? MediaType { get; set; }

    [JsonPropertyName("media_url")]
    public string? Permalink { get; set; }

    [JsonPropertyName("owner")]
    public RetrieveSingleMediaOwnerResponse? Owner { get; set; }

    [JsonPropertyName("comments")]
    public string? Username { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("shortcode")]
    public string? Shortcode { get; set; }

    [JsonPropertyName("is_quote_post")]
    public bool IsQuotePost { get; set; }
}
