using Threads.NET.Sdk.Posts.Commands.PublishMediaContainer;

namespace Threads.NET.Sdk.Tests.Posts.Commands;

[TestFixture]
public class PublishMediaContainerParametersBuilderTests
{
    [Test]
    public void AddCreationId_ShouldAddCreationIdToParameters()
    {
        // Arrange
        var builder = new PublishMediaContainerParametersBuilder();
        var creationId = "test_creation_id";

        // Act
        builder.AddCreationId(creationId);
        var result = builder.Build();

        // Assert
        result.ShouldContainKeyAndValue("creation_id", creationId);
    }

    [Test]
    public void AddAccessToken_ShouldAddAccessTokenToParameters()
    {
        // Arrange
        var builder = new PublishMediaContainerParametersBuilder();
        var accessToken = "test_access_token";

        // Act
        builder.AddAccessToken(accessToken);
        var result = builder.Build();

        // Assert
        result.ShouldContainKeyAndValue("access_token", accessToken);
    }

    [Test]
    public void Build_ShouldReturnDictionaryWithAllParameters()
    {
        // Arrange
        var builder = new PublishMediaContainerParametersBuilder();
        var creationId = "test_creation_id";
        var accessToken = "test_access_token";

        // Act
        builder.AddCreationId(creationId);
        builder.AddAccessToken(accessToken);
        var result = builder.Build();

        // Assert
        result.ShouldContainKeyAndValue("creation_id", creationId);
        result.ShouldContainKeyAndValue("access_token", accessToken);
    }
}
