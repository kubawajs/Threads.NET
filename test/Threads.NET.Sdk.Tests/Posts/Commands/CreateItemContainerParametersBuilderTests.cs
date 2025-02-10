using Threads.NET.Sdk.Posts.Commands;
using Threads.NET.Sdk.Posts.Commands.CreateItemContainer;

namespace Threads.NET.Sdk.Tests.Posts.Commands;

[TestFixture]
public class CreateItemContainerParametersBuilderTests
{
    [Test]
    public void AddIsCarouselItem_ShouldAddParameter_WhenIsCarouselItemIsTrue()
    {
        // Arrange
        var builder = new CreateItemContainerParametersBuilder();

        // Act
        builder.AddIsCarouselItem(true);
        var result = builder.Build();

        // Assert
        result.ContainsKey("is_carousel_item").ShouldBeTrue();
        result["is_carousel_item"].ShouldBe("True");
    }

    [Test]
    public void AddIsCarouselItem_ShouldNotAddParameter_WhenIsCarouselItemIsFalse()
    {
        // Arrange
        var builder = new CreateItemContainerParametersBuilder();

        // Act
        builder.AddIsCarouselItem(false);
        var result = builder.Build();

        // Assert
        result.ContainsKey("is_carousel_item").ShouldBeFalse();
    }

    [Test]
    public void AddImageUrl_ShouldAddParameter_WhenImageUrlIsNotNullOrWhiteSpace()
    {
        // Arrange
        var builder = new CreateItemContainerParametersBuilder();

        // Act
        builder.AddImageUrl("https://example.com/image.jpg");
        var result = builder.Build();

        // Assert
        result.ContainsKey("image_url").ShouldBeTrue();
        result["image_url"].ShouldBe("https://example.com/image.jpg");
    }

    [Test]
    public void AddImageUrl_ShouldNotAddParameter_WhenImageUrlIsNullOrWhiteSpace()
    {
        // Arrange
        var builder = new CreateItemContainerParametersBuilder();

        // Act
        builder.AddImageUrl(null);
        var result = builder.Build();

        // Assert
        result.ContainsKey("image_url").ShouldBeFalse();
    }

    [Test]
    public void AddMediaType_ShouldAddParameter_WhenMediaTypeIsNotNull()
    {
        // Arrange
        var builder = new CreateItemContainerParametersBuilder();
        var mediaType = ItemContainerMediaType.IMAGE;

        // Act
        builder.AddMediaType(mediaType);
        var result = builder.Build();

        // Assert
        result.ContainsKey("media_type").ShouldBeTrue();
        result["media_type"].ShouldBe(mediaType.ToString());
    }

    [Test]
    public void AddMediaType_ShouldNotAddParameter_WhenMediaTypeIsNull()
    {
        // Arrange
        var builder = new CreateItemContainerParametersBuilder();

        // Act
        builder.AddMediaType(null);
        var result = builder.Build();

        // Assert
        result.ContainsKey("media_type").ShouldBeFalse();
    }

    [Test]
    public void AddVideoUrl_ShouldAddParameter_WhenVideoUrlIsNotNullOrWhiteSpace()
    {
        // Arrange
        var builder = new CreateItemContainerParametersBuilder();

        // Act
        builder.AddVideoUrl("https://example.com/video.mp4");
        var result = builder.Build();

        // Assert
        result.ContainsKey("video_url").ShouldBeTrue();
        result["video_url"].ShouldBe("https://example.com/video.mp4");
    }

    [Test]
    public void AddVideoUrl_ShouldNotAddParameter_WhenVideoUrlIsNullOrWhiteSpace()
    {
        // Arrange
        var builder = new CreateItemContainerParametersBuilder();

        // Act
        builder.AddVideoUrl(null);
        var result = builder.Build();

        // Assert
        result.ContainsKey("video_url").ShouldBeFalse();
    }

    [Test]
    public void AddAccessToken_ShouldAddParameter_WhenAccessTokenIsNotNullOrWhiteSpace()
    {
        // Arrange
        var builder = new CreateItemContainerParametersBuilder();

        // Act
        builder.AddAccessToken("token");
        var result = builder.Build();

        // Assert
        result.ContainsKey("access_token").ShouldBeTrue();
        result["access_token"].ShouldBe("token");
    }

    [Test]
    public void AddAccessToken_ShouldNotAddParameter_WhenAccessTokenIsNullOrWhiteSpace()
    {
        // Arrange
        var builder = new CreateItemContainerParametersBuilder();

        // Act
        builder.AddAccessToken(null);
        var result = builder.Build();

        // Assert
        result.ContainsKey("access_token").ShouldBeFalse();
    }
}
