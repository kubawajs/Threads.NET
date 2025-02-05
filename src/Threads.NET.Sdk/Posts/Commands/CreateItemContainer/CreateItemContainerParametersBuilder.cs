namespace Threads.NET.Sdk.Posts.Commands.CreateItemContainer;

internal sealed class CreateItemContainerParametersBuilder
{
    private readonly Dictionary<string, string> _parameters = [];

    public CreateItemContainerParametersBuilder AddIsCarouselItem(bool isCarouselItem)
    {
        if (isCarouselItem)
        {
            _parameters.Add("is_carousel_item", true.ToString());
        }

        return this;
    }

    public CreateItemContainerParametersBuilder AddImageUrl(string? imageUrl)
    {
        if (!string.IsNullOrWhiteSpace(imageUrl))
        {
            _parameters.Add("image_url", imageUrl!);
        }

        return this;
    }

    public CreateItemContainerParametersBuilder AddMediaType(Enum? mediaType)
    {
        if (mediaType != null)
        {
            _parameters.Add("media_type", mediaType.ToString());
        }

        return this;
    }

    public CreateItemContainerParametersBuilder AddVideoUrl(string? videoUrl)
    {
        if (!string.IsNullOrWhiteSpace(videoUrl))
        {
            _parameters.Add("video_url", videoUrl!);
        }

        return this;
    }

    public CreateItemContainerParametersBuilder AddAccessToken(string? accessToken)
    {
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            _parameters.Add("access_token", accessToken!);
        }

        return this;
    }

    public Dictionary<string, string> Build() => _parameters;
}