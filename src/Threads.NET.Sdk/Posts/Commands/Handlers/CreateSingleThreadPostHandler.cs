using Threads.NET.Sdk.Exceptions;

namespace Threads.NET.Sdk.Posts.Commands.Handlers;
internal sealed class CreateSingleThreadPostHandler(IHttpClientFactory httpClientFactory)
    : IRequestHandler<CreateSingleThreadPost, CreateSingleThreadPostResponse>
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Threads");

    public async Task<CreateSingleThreadPostResponse> Handle(CreateSingleThreadPost request, CancellationToken cancellationToken)
    {
        var path = $"{Constants.ApiVersion}/{request.UserId}/threads";
        var content = new FormUrlEncodedContent(CreatePostParameters(request));

        var response = await _httpClient.PostAsync(path, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsInvalidResponseCodeException(response.StatusCode, responseContent);
        }

        var deserializedResponse = JsonSerializer.Deserialize<CreateSingleThreadPostResponse>(responseContent);
        return deserializedResponse is not null ? deserializedResponse : throw new ThreadsDeserializationException(typeof(CreateSingleThreadPost), responseContent);
    }

    private static Dictionary<string, string> CreatePostParameters(CreateSingleThreadPost request)
        => new CreateSingleThreadPostParametersBuilder()
        .AddIsCarouselItem(request.IsCarouselItem)
        .AddImageUrl(request.ImageUrl)
        .AddMediaType(request.MediaType)
        .AddVideoUrl(request.VideoUrl)
        .AddText(request.Text)
        .AddAccessToken(request.AccessToken)
        .Build();
}
