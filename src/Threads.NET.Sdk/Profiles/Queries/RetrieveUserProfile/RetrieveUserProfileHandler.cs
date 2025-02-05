using Threads.NET.Sdk.Exceptions;

namespace Threads.NET.Sdk.Profiles.Queries.RetrieveUserProfile;

internal sealed class RetrieveUserProfileHandler(IHttpClientFactory httpClientFactory)
    : IRequestHandler<RetrieveUserProfileRequest, RetrieveUserProfileResponse>
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Threads");

    public async Task<RetrieveUserProfileResponse> Handle(RetrieveUserProfileRequest request, CancellationToken cancellationToken)
    {
        var path = $"{Constants.ApiVersion}/me";
        var queryParams = CreateGetParameters(request);
        var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        var fullUrl = $"{path}?{queryString}";

        var response = await _httpClient.GetAsync(fullUrl, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsInvalidResponseCodeException(response.StatusCode, responseContent);
        }

        var deserializedResponse = JsonSerializer.Deserialize<RetrieveUserProfileResponse>(responseContent);
        return deserializedResponse is not null ? deserializedResponse : throw new ThreadsDeserializationException(typeof(RetrieveUserProfileRequest), responseContent);
    }

    private static Dictionary<string, string> CreateGetParameters(RetrieveUserProfileRequest request)
        => new RetrieveUserProfileParameterBuilder()
            .AddFields()
            .AddAccessToken(request.AccessToken)
            .Build();
}