
using Threads.NET.Sdk.Exceptions;

namespace Threads.NET.Sdk.Posts.Commands.CreateCarouselContainer;

internal sealed class CreateCarouselContainerHandler(IHttpClientFactory httpClientFactory)
    : IRequestHandler<CreateCarouselContainerRequest, CreateCarouselContainerResponse>
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Threads");

    public async Task<CreateCarouselContainerResponse> Handle(CreateCarouselContainerRequest request, CancellationToken cancellationToken)
    {
        var path = $"{Constants.ApiVersion}/{request.UserId}/threads";
        var content = new FormUrlEncodedContent(CreatePostParameters(request));

        var response = await _httpClient.PostAsync(path, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsInvalidResponseCodeException(response.StatusCode, responseContent);
        }

        var deserializedResponse = JsonSerializer.Deserialize<CreateCarouselContainerResponse>(responseContent);
        return deserializedResponse is not null ? deserializedResponse : throw new ThreadsDeserializationException(typeof(CreateCarouselContainerResponse), responseContent);
    }

    private static Dictionary<string, string> CreatePostParameters(CreateCarouselContainerRequest request)
        => new CreateCarouselContainerParameterBuilder()
            .AddMediaType()
            .AddChildren(request.Children)
            .AddText(request.Text)
            .AddAccessToken(request.AccessToken)
            .Build();
}