using Threads.NET.Sdk.Posts.Commands.CreateCarouselContainer;

namespace Threads.NET.Sdk.Tests.Posts.Commands;

[TestFixture]
public class CreateCarouselContainerParameterBuilderTests
{
    [Test]
    public void AddMediaType_ShouldAddParameter()
    {
        // Arrange
        var builder = new CreateCarouselContainerParameterBuilder();

        // Act
        builder.AddMediaType();
        var result = builder.Build();

        // Assert
        result.ContainsKey("media_type").ShouldBeTrue();
        result["media_type"].ShouldBe("CAROUSEL");
    }

    [Test]
    public void AddChildren_ShouldAddParameter_WhenChildrenIsNotNullOrEmpty()
    {
        // Arrange
        var builder = new CreateCarouselContainerParameterBuilder();
        var children = new[] { "child1", "child2" };

        // Act
        builder.AddChildren(children);
        var result = builder.Build();

        // Assert
        result.ContainsKey("children").ShouldBeTrue();
        result["children"].ShouldBe("child1,child2");
    }

    [Test]
    public void AddChildren_ShouldNotAddParameter_WhenChildrenIsNull()
    {
        // Arrange
        var builder = new CreateCarouselContainerParameterBuilder();

        // Act
        builder.AddChildren(null);
        var result = builder.Build();

        // Assert
        result.ContainsKey("children").ShouldBeFalse();
    }

    [Test]
    public void AddChildren_ShouldNotAddParameter_WhenChildrenIsEmpty()
    {
        // Arrange
        var builder = new CreateCarouselContainerParameterBuilder();
        var children = Array.Empty<string>();

        // Act
        builder.AddChildren(children);
        var result = builder.Build();

        // Assert
        result.ContainsKey("children").ShouldBeFalse();
    }

    [Test]
    public void AddAccessToken_ShouldAddParameter_WhenAccessTokenIsNotNullOrWhiteSpace()
    {
        // Arrange
        var builder = new CreateCarouselContainerParameterBuilder();

        // Act
        builder.AddAccessToken("accessToken");
        var result = builder.Build();

        // Assert
        result.ContainsKey("access_token").ShouldBeTrue();
        result["access_token"].ShouldBe("accessToken");
    }

    [Test]
    public void AddAccessToken_ShouldNotAddParameter_WhenAccessTokenIsNullOrWhiteSpace()
    {
        // Arrange
        var builder = new CreateCarouselContainerParameterBuilder();

        // Act
        builder.AddAccessToken(null);
        var result = builder.Build();

        // Assert
        result.ContainsKey("access_token").ShouldBeFalse();
    }

    [Test]
    public void AddText_ShouldAddParameter_WhenTextIsNotNullOrWhiteSpace()
    {
        // Arrange
        var builder = new CreateCarouselContainerParameterBuilder();

        // Act
        builder.AddText("sample text");
        var result = builder.Build();

        // Assert
        result.ContainsKey("text").ShouldBeTrue();
        result["text"].ShouldBe("sample text");
    }

    [Test]
    public void AddText_ShouldNotAddParameter_WhenTextIsNullOrWhiteSpace()
    {
        // Arrange
        var builder = new CreateCarouselContainerParameterBuilder();

        // Act
        builder.AddText(null);
        var result = builder.Build();

        // Assert
        result.ContainsKey("text").ShouldBeFalse();
    }
}
