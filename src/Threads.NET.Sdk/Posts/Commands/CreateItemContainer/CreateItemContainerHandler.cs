using Threads.NET.Sdk.Exceptions;

namespace Threads.NET.Sdk.Posts.Commands.CreateItemContainer;

internal sealed class CreateItemContainerHandler(IHttpClientFactory httpClientFactory)
    : IRequestHandler<CreateItemContainerRequest, CreateItemContainerResponse>
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Threads");

    public async Task<CreateItemContainerResponse> Handle(CreateItemContainerRequest request, CancellationToken cancellationToken)
    {
        var path = $"{Constants.ApiVersion}/{request.UserId}/threads";
        var content = new FormUrlEncodedContent(CreateItemContainerParameters(request));

        var response = await _httpClient.PostAsync(path, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsInvalidResponseCodeException(response.StatusCode, responseContent);
        }

        var deserializedResponse = JsonSerializer.Deserialize<CreateItemContainerResponse>(responseContent);
        return deserializedResponse is not null ? deserializedResponse : throw new ThreadsDeserializationException(typeof(CreateItemContainerRequest), responseContent);
    }

    private static Dictionary<string, string> CreateItemContainerParameters(CreateItemContainerRequest request)
        => new CreateItemContainerParametersBuilder()
            .AddIsCarouselItem(request.IsCarouselItem)
            .AddImageUrl(request.ImageUrl)
            .AddMediaType(request.MediaType)
            .AddVideoUrl(request.VideoUrl)
            .AddAccessToken(request.AccessToken)
            .Build();
}