using Threads.NET.Sdk.Exceptions;
using Threads.NET.Sdk.Profiles.Queries.RetrieveUserProfile;

namespace Threads.NET.Sdk.Tests.Profiles.Queries;

[TestFixture]
public class RetrieveUserProfileHandlerTests
{
    private Mock<IHttpClientFactory> _httpClientFactoryMock;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private RetrieveUserProfileHandler _handler;

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

        _handler = new RetrieveUserProfileHandler(_httpClientFactoryMock.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnResponse_WhenRequestIsValid()
    {
        // Arrange
        var request = new RetrieveUserProfileRequest(
            "accessToken",
            "userId"
        );

        var responseContent = "{\"id\":\"12345\",\"username\":\"testuser\",\"name\":\"Test User\",\"threads_profile_picture_url\":\"https://example.com/profile.jpg\",\"threads_biography\":\"Test biography\"}";
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
        response.Username.ShouldBe("testuser");
        response.Name.ShouldBe("Test User");
        response.ProfilePictureUrl.ShouldBe("https://example.com/profile.jpg");
        response.Biography.ShouldBe("Test biography");
    }

    [Test]
    public void Handle_ShouldThrowThreadsInvalidResponseCodeException_WhenResponseIsNotSuccessful()
    {
        // Arrange
        var request = new RetrieveUserProfileRequest(
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
        var request = new RetrieveUserProfileRequest(
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

