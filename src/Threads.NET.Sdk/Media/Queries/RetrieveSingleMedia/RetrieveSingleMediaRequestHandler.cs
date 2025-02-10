using Threads.NET.Sdk.Exceptions;

namespace Threads.NET.Sdk.Media.Queries.RetrieveSingleMedia;

internal sealed class RetrieveSingleMediaRequestHandler(IHttpClientFactory httpClientFactory)
    : IRequestHandler<RetrieveSingleMediaRequest, RetrieveSingleMediaResponse>
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Threads");

    public async Task<RetrieveSingleMediaResponse> Handle(RetrieveSingleMediaRequest request, CancellationToken cancellationToken)
    {
        var path = $"{Constants.ApiVersion}/{request.MediaId}";
        var queryParams = CreateGetParameters(request);
        var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        var fullUrl = $"{path}?{queryString}";

        var response = await _httpClient.GetAsync(fullUrl, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsInvalidResponseCodeException(response.StatusCode, responseContent);
        }

        var deserializedResponse = JsonSerializer.Deserialize<RetrieveSingleMediaResponse>(responseContent);
        return deserializedResponse is not null ? deserializedResponse : throw new ThreadsDeserializationException(typeof(RetrieveSingleMediaRequest), responseContent);
    }

    private static Dictionary<string, string> CreateGetParameters(RetrieveSingleMediaRequest request)
        => new RetrieveSingleMediaParameterBuilder()
            .AddAccessToken(request.AccessToken)
            .Build();
}