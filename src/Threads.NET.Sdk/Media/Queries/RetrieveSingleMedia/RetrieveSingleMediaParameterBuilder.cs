namespace Threads.NET.Sdk.Media.Queries.RetrieveSingleMedia;

internal sealed class RetrieveSingleMediaParameterBuilder
{
    private readonly Dictionary<string, string> _parameters = [];

    public RetrieveSingleMediaParameterBuilder AddFields()
    {
        _parameters.Add("fields", "id,media_product_type,media_type,media_url,permalink,owner,username,text,timestamp,shortcode,thumbnail_url,children,is_quote_post");
        return this;
    }

    public RetrieveSingleMediaParameterBuilder AddAccessToken(string? accessToken)
    {
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            _parameters.Add("access_token", accessToken!);
        }

        return this;
    }

    public Dictionary<string, string> Build() => _parameters;
}