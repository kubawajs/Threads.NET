using Threads.NET.Sdk.Media.Queries.RetrieveSingleMedia;

namespace Threads.NET.Sdk.Media.Queries.RetrieveAllThreads;

public sealed record RetrieveAllThreadsThreadResponse
{
    /// <summary>
    /// The media's ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The media type for a Threads post will be one of these values: TEXT_POST, IMAGE, VIDEO, CAROUSEL_ALBUM, AUDIO, or REPOST_FACADE.
    /// </summary>
    [JsonPropertyName("media_type")]
    public RetrieveAllThreadsMediaType MediaType { get; set; }

    /// <summary>
    /// The post’s media URL.
    /// </summary>
    [JsonPropertyName("media_url")]
    public string? MediaUrl { get; set; }

    /// <summary>
    /// Permanent link to the post. Will be omitted if the media contains copyrighted material or has been flagged for a copyright violation.
    /// </summary>
    [JsonPropertyName("permalink")]
    public string? Permalink { get; set; }

    /// <summary>
    /// Threads user ID who created the post. Note: This is only available on top-level posts that you own.
    /// </summary>
    [JsonPropertyName("owner")]
    public OwnerResponse? Owner { get; set; }

    /// <summary>
    /// Threads username who created the post.
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Represents text for a Threads post.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Post time. The publish date in ISO 8601 format.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Shortcode of the media.
    /// </summary>
    [JsonPropertyName("shortcode")]
    public string? Shortcode { get; set; }

    /// <summary>
    /// URL of thumbnail. This only shows up for Threads media with video.
    /// </summary>
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }

    /// <summary>
    /// Indicates if the media is a quoted post made by another user.
    /// </summary>
    [JsonPropertyName("is_quote_post")]
    public bool IsQuotePost { get; set; }

    /// <summary>
    /// Media ID of the post that was quoted. Note: This only appears on quote posts.
    /// </summary>
    [JsonPropertyName("quoted_post")]
    public string? QuotedPost { get; set; }

    /// <summary>
    /// Media ID of the post that was reposted. Note: This only appears on reposts.
    /// </summary>
    [JsonPropertyName("reposted_post")]
    public string? RepostedPost { get; set; }

    /// <summary>
    /// The accessibility text label or description for an image or video in a Threads post.
    /// </summary>
    [JsonPropertyName("alt_text")]
    public string? AltText { get; set; }

    /// <summary>
    /// The URL attached to a Threads post.
    /// </summary>
    [JsonPropertyName("link_attachment_url")]
    public string? LinkAttachmentUrl { get; set; }
}
