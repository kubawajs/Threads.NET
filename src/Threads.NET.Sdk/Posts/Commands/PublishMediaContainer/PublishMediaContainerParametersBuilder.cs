namespace Threads.NET.Sdk.Posts.Commands.PublishMediaContainer;

internal sealed class PublishMediaContainerParametersBuilder
{
    private readonly Dictionary<string, string> _parameters = [];

    public PublishMediaContainerParametersBuilder AddCreationId(string creationId)
    {
        _parameters.Add("creation_id", creationId);
        return this;
    }

    public PublishMediaContainerParametersBuilder AddAccessToken(string accessToken)
    {
        _parameters.Add("access_token", accessToken);
        return this;
    }

    public Dictionary<string, string> Build() => _parameters;
}