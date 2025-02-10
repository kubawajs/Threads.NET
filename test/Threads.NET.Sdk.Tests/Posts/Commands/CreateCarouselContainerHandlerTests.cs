using Threads.NET.Sdk.Exceptions;
using Threads.NET.Sdk.Posts.Commands.CreateCarouselContainer;

namespace Threads.NET.Sdk.Tests.Posts.Commands;

[TestFixture]
public class CreateCarouselContainerHandlerTests
{
    private Mock<IHttpClientFactory> _httpClientFactoryMock;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private CreateCarouselContainerHandler _handler;

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

        _handler = new CreateCarouselContainerHandler(_httpClientFactoryMock.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnResponse_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateCarouselContainerRequest(
            ["child1", "child2"],
            "sample text",
            "accessToken",
            "userId"
        );

        var responseContent = "{\"id\":\"12345\"}";
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
        response.PostId.ShouldBe("12345");
    }

    [Test]
    public void Handle_ShouldThrowThreadsInvalidResponseCodeException_WhenResponseIsNotSuccessful()
    {
        // Arrange
        var request = new CreateCarouselContainerRequest(
            new[] { "child1", "child2" },
            "sample text",
            "accessToken",
            "userId"
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
        var request = new CreateCarouselContainerRequest(
            ["child1", "child2"],
            "sample text",
            "accessToken",
            "userId"
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
