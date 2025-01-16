namespace Threads.NET.Sdk.Posts.Commands.Handlers;

public class CreateSingleThreadPostParametersBuilder
{
    private readonly Dictionary<string, string> _parameters = [];

    public CreateSingleThreadPostParametersBuilder AddIsCarouselItem(bool isCarouselItem)
    {
        if(isCarouselItem)
        {
            _parameters.Add("is_carousel_item", true.ToString());
        }

        return this;
    }

    public CreateSingleThreadPostParametersBuilder AddImageUrl(string? imageUrl)
    {
        if (!string.IsNullOrWhiteSpace(imageUrl))
        {
            _parameters.Add("image_url", imageUrl);
        }

        return this;
    }

    public CreateSingleThreadPostParametersBuilder AddMediaType(Enum? mediaType)
    {
        if (mediaType != null)
        {
            _parameters.Add("media_type", mediaType.ToString());
        }

        return this;
    }

    public CreateSingleThreadPostParametersBuilder AddVideoUrl(string? videoUrl)
    {
        if (!string.IsNullOrWhiteSpace(videoUrl))
        {
            _parameters.Add("video_url", videoUrl);
        }

        return this;
    }

    public CreateSingleThreadPostParametersBuilder AddText(string? text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            _parameters.Add("text", text);
        }

        return this;
    }

    public CreateSingleThreadPostParametersBuilder AddAccessToken(string? accessToken)
    {
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            _parameters.Add("access_token", accessToken);
        }

        return this;
    }

    public Dictionary<string, string> Build() => _parameters;
}
