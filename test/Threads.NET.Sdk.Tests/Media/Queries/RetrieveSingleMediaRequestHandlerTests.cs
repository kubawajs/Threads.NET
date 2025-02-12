using Threads.NET.Sdk.Exceptions;
using Threads.NET.Sdk.Media.Queries.RetrieveSingleMedia;

namespace Threads.NET.Sdk.Tests.Media.Queries;

[TestFixture]
public class RetrieveSingleMediaRequestHandlerTests
{
    private Mock<IHttpClientFactory> _httpClientFactoryMock;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private RetrieveSingleMediaRequestHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.threads.net/")
        };

        _httpClientFactoryMock
            .Setup(factory => factory.CreateClient("Threads"))
            .Returns(httpClient);

        _handler = new RetrieveSingleMediaRequestHandler(_httpClientFactoryMock.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnResponse_WhenRequestIsValid()
    {
        // Arrange
        var request = new RetrieveSingleMediaRequest(
            "mediaId",
            "accessToken"
        );

        var responseContent = "{\"id\":\"12345\",\"media_product_type\":\"image\",\"media_type\":\"photo\",\"media_url\":\"https://example.com/media.jpg\",\"owner\":{\"id\":\"ownerId\"},\"comments\":\"testuser\",\"text\":\"Test text\",\"timestamp\":\"2023-10-01T00:00:00Z\",\"shortcode\":\"shortcode\",\"is_quote_post\":false}";
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent)
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage);

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.ShouldNotBeNull();
        response.Id.ShouldBe("12345");
        response.MediaProductType.ShouldBe("image");
        response.MediaType.ShouldBe("photo");
        response.Permalink.ShouldBe("https://example.com/media.jpg");
        response.Owner.ShouldNotBeNull();
        response.Owner.Id.ShouldBe("ownerId");
        response.Username.ShouldBe("testuser");
        response.Text.ShouldBe("Test text");
        response.Timestamp.ShouldBe(new DateTime(2023, 10, 1));
        response.Shortcode.ShouldBe("shortcode");
        response.IsQuotePost.ShouldBeFalse();
    }

    [Test]
    public void Handle_ShouldThrowThreadsInvalidResponseCodeException_WhenResponseIsNotSuccessful()
    {
        // Arrange
        var request = new RetrieveSingleMediaRequest(
            "mediaId",
            "accessToken"
        );

        var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("Bad Request")
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage);

        // Act & Assert
        Should.ThrowAsync<ThreadsInvalidResponseCodeException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Test]
    public void Handle_ShouldThrowThreadsDeserializationException_WhenResponseContentIsInvalid()
    {
        // Arrange
        var request = new RetrieveSingleMediaRequest(
            "mediaId",
            "accessToken"
        );

        var responseContent = "Invalid Content";
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent)
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage);

        // Act & Assert
        Should.ThrowAsync<ThreadsDeserializationException>(() => _handler.Handle(request, CancellationToken.None));
    }
}