namespace Threads.NET.Sdk.Profiles.Queries.RetrieveUserProfile;

internal sealed class RetrieveUserProfileParameterBuilder
{
    private readonly Dictionary<string, string> _parameters = [];

    public RetrieveUserProfileParameterBuilder AddFields()
    {
        _parameters.Add("fields", "id,username,name,threads_profile_picture_url,threads_biography");
        return this;
    }

    public RetrieveUserProfileParameterBuilder AddAccessToken(string? accessToken)
    {
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            _parameters.Add("access_token", accessToken!);
        }

        return this;
    }

    public Dictionary<string, string> Build() => _parameters;
}