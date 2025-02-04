using Threads.NET.Sdk.Exceptions;

namespace Threads.NET.Sdk.Posts.Commands.PublishMediaContainer;

internal sealed class PublishMediaContainerHandler(IHttpClientFactory httpClientFactory)
    : IRequestHandler<PublishMediaContainerRequest, PublishMediaContainerResponse>
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Threads");

    public async Task<PublishMediaContainerResponse> Handle(PublishMediaContainerRequest request, CancellationToken cancellationToken)
    {
        var path = $"{Constants.ApiVersion}/{request.UserId}/threads_publish";
        var content = new FormUrlEncodedContent(CreatePostParameters(request));

        var response = await _httpClient.PostAsync(path, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsInvalidResponseCodeException(response.StatusCode, responseContent);
        }

        var deserializedResponse = JsonSerializer.Deserialize<PublishMediaContainerResponse>(responseContent);
        return deserializedResponse is not null ? deserializedResponse : throw new ThreadsDeserializationException(typeof(PublishMediaContainerRequest), responseContent);
    }

    private static Dictionary<string, string> CreatePostParameters(PublishMediaContainerRequest request) =>
        new PublishMediaContainerParametersBuilder()
            .AddCreationId(request.CreationId)
            .AddAccessToken(request.AccessToken)
            .Build();
}
