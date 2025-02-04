using Threads.NET.Sdk.Exceptions;
using Threads.NET.Sdk.Posts.Commands;
using Threads.NET.Sdk.Posts.Commands.CreateSingleThreadPost;

namespace Threads.NET.Sdk.Tests.Posts.Commands;

[TestFixture]
public class CreateSingleThreadPostHandlerTests
{
    private Mock<IHttpClientFactory> _httpClientFactoryMock;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private CreateSingleThreadPostHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.threads.net")
        };

        _httpClientFactoryMock
            .Setup(factory => factory.CreateClient("Threads"))
            .Returns(httpClient);

        _handler = new CreateSingleThreadPostHandler(_httpClientFactoryMock.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnDeserializedResponse_WhenRequestIsSuccessful()
    {
        // Arrange
        var request = new CreateSingleThreadPostRequest(
            IsCarouselItem: false,
            ImageUrl: "https://example.com/image.jpg",
            MediaType: SingleThreadPostMediaType.IMAGE,
            VideoUrl: null,
            Text: "Test post",
            AccessToken: "token",
            UserId: "123"
        );

        var expectedResponse = new CreateSingleThreadPostResponse
        {
            PostId = "456"
        };

        var responseContent = JsonSerializer.Serialize(expectedResponse);

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseContent)
            });

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.PostId.ShouldBe(expectedResponse.PostId);
    }

    [Test]
    public void Handle_ShouldThrowThreadsInvalidResponseCodeException_WhenResponseIsNotSuccessful()
    {
        // Arrange
        var request = new CreateSingleThreadPostRequest(
            IsCarouselItem: false,
            ImageUrl: "https://example.com/image.jpg",
            MediaType: SingleThreadPostMediaType.IMAGE,
            VideoUrl: null,
            Text: "Test post",
            AccessToken: "token",
            UserId: "123"
        );

        var responseContent = "Error message";

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(responseContent)
            });

        // Act & Assert
        Should.Throw<ThreadsInvalidResponseCodeException>(async () => await _handler.Handle(request, CancellationToken.None)).Message.ShouldContain(responseContent);
    }

    [Test]
    public void Handle_ShouldThrowThreadsDeserializationException_WhenResponseCannotBeDeserialized()
    {
        // Arrange
        var request = new CreateSingleThreadPostRequest(
            IsCarouselItem: false,
            ImageUrl: "https://example.com/image.jpg",
            MediaType: SingleThreadPostMediaType.IMAGE,
            VideoUrl: null,
            Text: "Test post",
            AccessToken: "token",
            UserId: "123"
        );

        var responseContent = "Invalid JSON";

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseContent)
            });

        // Act & Assert
        Should.Throw<JsonException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Test]
    public async Task Handle_ShouldIncludeCorrectParametersInRequest_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateSingleThreadPostRequest(
            IsCarouselItem: true,
            ImageUrl: "https://example.com/image.jpg",
            MediaType: SingleThreadPostMediaType.IMAGE,
            VideoUrl: null,
            Text: "Test post",
            AccessToken: "token",
            UserId: "123"
        );

        var expectedResponse = new CreateSingleThreadPostResponse
        {
            PostId = "456",
        };

        var responseContent = JsonSerializer.Serialize(expectedResponse);

        HttpRequestMessage? capturedRequest = null;

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Callback<HttpRequestMessage, CancellationToken>((req, _) => capturedRequest = req)
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseContent)
            });

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        capturedRequest.ShouldNotBeNull();
        var requestContent = await capturedRequest.Content.ReadAsStringAsync();
        requestContent.ShouldContain("is_carousel_item=True");
        requestContent.ShouldContain($"image_url={HttpUtility.UrlEncode("https://example.com/image.jpg")}");
        requestContent.ShouldContain("media_type=IMAGE");
        requestContent.ShouldContain($"text={HttpUtility.UrlEncode("Test post")}");
        requestContent.ShouldContain("access_token=token");
    }
}