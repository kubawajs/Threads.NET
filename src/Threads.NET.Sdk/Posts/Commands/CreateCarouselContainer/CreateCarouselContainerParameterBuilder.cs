namespace Threads.NET.Sdk.Posts.Commands.CreateCarouselContainer;

internal sealed class CreateCarouselContainerParameterBuilder
{
    private readonly Dictionary<string, string> _parameters = [];

    public CreateCarouselContainerParameterBuilder AddMediaType()
    {
        _parameters.Add("media_type", "CAROUSEL");

        return this;
    }

    public CreateCarouselContainerParameterBuilder AddChildren(string[]? children)
    {
        if (children is not null && children.Any())
        {
            _parameters.Add("children", string.Join(",", children));
        }

        return this;
    }

    public CreateCarouselContainerParameterBuilder AddAccessToken(string? accessToken)
    {
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            _parameters.Add("access_token", accessToken!);
        }

        return this;
    }

    public CreateCarouselContainerParameterBuilder AddText(string? text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            _parameters.Add("text", text!);
        }

        return this;
    }

    public Dictionary<string, string> Build() => _parameters;
}