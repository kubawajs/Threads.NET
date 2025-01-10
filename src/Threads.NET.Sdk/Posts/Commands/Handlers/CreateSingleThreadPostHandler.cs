using Threads.NET.Sdk.Exceptions;
using Threads.NET.Sdk.Posts.Responses;

namespace Threads.NET.Sdk.Posts.Commands.Handlers;
internal sealed class CreateSingleThreadPostHandler(HttpClient httpClient)
    : IRequestHandler<CreateSingleThreadPost, CreateSingleThreadPostResponse>
{
    private static readonly string[] value = ["threads_basic", "threads_content_publish "]; // TODO: to constants
    private const string _apiVersion = "v1.0"; // TODO: to constants

    public async Task<CreateSingleThreadPostResponse> Handle(CreateSingleThreadPost request, CancellationToken cancellationToken)
    {
        var path = $"/{_apiVersion}/{request.UserId}/threads";
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["is_carousel_item "] = request.IsCarouselItem.ToString(),
            ["image_url"] = request.ImageUrl,
            ["media_type"] = request.MediaType.ToString(),
            ["video_url"] = request.VideoUrl,
            ["text"] = request.Text,
            ["access_token"] = request.AccessToken,
        });

        var response = await httpClient.PostAsync(path, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ThreadsInvalidResponseCodeException(response.StatusCode, responseContent);
        }

        return JsonSerializer.Deserialize<CreateSingleThreadPostResponse>(responseContent);
    }
}
