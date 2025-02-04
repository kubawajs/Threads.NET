using Threads.NET.Sdk.Exceptions;
using Threads.NET.Sdk.Posts.Commands.PublishMediaContainer;

namespace Threads.NET.Sdk.Tests.Posts.Commands;

[TestFixture]
public class PublishMediaContainerHandlerTests
{
    private Mock<IHttpClientFactory> _httpClientFactoryMock;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private PublishMediaContainerHandler _handler;

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

        _handler = new PublishMediaContainerHandler(_httpClientFactoryMock.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnResponse_WhenRequestIsValid()
    {
        // Arrange
        var request = new PublishMediaContainerRequest("creationId", "accessToken", "userId");
        var responseContent = "{\"id\":\"mediaContainerId\"}";
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent)
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.ShouldNotBeNull();
        response.MediaContainerId.ShouldBe("mediaContainerId");
    }

    [Test]
    public void Handle_ShouldThrowThreadsInvalidResponseCodeException_WhenResponseIsNotSuccessful()
    {
        // Arrange
        var request = new PublishMediaContainerRequest("creationId", "accessToken", "userId");
        var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("Bad Request")
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act & Assert
        Should.Throw<ThreadsInvalidResponseCodeException>(async () =>
            await _handler.Handle(request, CancellationToken.None));
    }

    [Test]
    public void Handle_ShouldThrowThreadsDeserializationException_WhenResponseContentIsInvalid()
    {
        // Arrange
        var request = new PublishMediaContainerRequest("creationId", "accessToken", "userId");
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Invalid Content")
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act & Assert
        Should.Throw<JsonException>(async () =>
            await _handler.Handle(request, CancellationToken.None));
    }

    [Test]
    public async Task Handle_ShouldIncludeCorrectParametersInRequest_WhenRequestIsValid()
    {
        // Arrange
        var request = new PublishMediaContainerRequest("creationId", "accessToken", "userId");
        var expectedResponse = new PublishMediaContainerResponse
        {
            MediaContainerId = "mediaContainerId"
        };

        var responseContent = JsonSerializer.Serialize(expectedResponse);

        HttpRequestMessage? capturedRequest = null;

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
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
        capturedRequest.Method.ShouldBe(HttpMethod.Post);

        var requestContent = await capturedRequest.Content.ReadAsStringAsync();
        requestContent.ShouldContain("creation_id=creationId");
        requestContent.ShouldContain("access_token=accessToken");
    }
}
