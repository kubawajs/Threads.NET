using Threads.NET.Sdk.Exceptions;

namespace Threads.NET.Sdk.Media.Queries.RetrieveAllThreads;

internal sealed class RetrieveAllThreadsThreadRequestHandler(IHttpClientFactory httpClientFactory)
    : IRequestHandler<RetrieveAllThreadsRequest, RetrieveAllThreadsResponse>
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Threads");

    public async Task<RetrieveAllThreadsResponse> Handle(RetrieveAllThreadsRequest request, CancellationToken cancellationToken)
    {
        var path = $"{Constants.ApiVersion}/{request.UserId}/threads?access_token={request.AccessToken}";

        var response = await _httpClient.GetAsync(path, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsInvalidResponseCodeException(response.StatusCode, responseContent);
        }

        var deserializedResponse = JsonSerializer.Deserialize<RetrieveAllThreadsResponse>(responseContent);
        return deserializedResponse is not null ? deserializedResponse : throw new ThreadsDeserializationException(typeof(RetrieveAllThreadsResponse), responseContent);
    }
}