using Threads.NET.Sdk.Posts.Commands;
using Threads.NET.Sdk.Posts.Commands.CreateSingleThreadPost;

namespace Threads.NET.Sdk.Tests.Posts.Commands;

[TestFixture]
public class CreateSingleThreadPostParametersBuilderTests
{
    [Test]
    public void AddIsCarouselItem_ShouldAddParameter_WhenTrue()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddIsCarouselItem(true);
        var result = builder.Build();

        result.ShouldContainKeyAndValue("is_carousel_item", true.ToString());
    }

    [Test]
    public void AddIsCarouselItem_ShouldNotAddParameter_WhenFalse()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddIsCarouselItem(false);
        var result = builder.Build();

        result.ShouldNotContainKey("is_carousel_item");
    }

    [Test]
    public void AddImageUrl_ShouldAddParameter_WhenNotNullOrWhiteSpace()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddImageUrl("http://example.com/image.jpg");
        var result = builder.Build();

        result.ShouldContainKeyAndValue("image_url", "http://example.com/image.jpg");
    }

    [Test]
    public void AddImageUrl_ShouldNotAddParameter_WhenNullOrWhiteSpace()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddImageUrl(null);
        var result = builder.Build();

        result.ShouldNotContainKey("image_url");
    }

    [Test]
    public void AddMediaType_ShouldAddParameter_WhenNotNull()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddMediaType(SingleThreadPostMediaType.IMAGE);
        var result = builder.Build();

        result.ShouldContainKeyAndValue("media_type", SingleThreadPostMediaType.IMAGE.ToString());
    }

    [Test]
    public void AddMediaType_ShouldNotAddParameter_WhenNull()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddMediaType(null);
        var result = builder.Build();

        result.ShouldNotContainKey("media_type");
    }

    [Test]
    public void AddVideoUrl_ShouldAddParameter_WhenNotNullOrWhiteSpace()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddVideoUrl("http://example.com/video.mp4");
        var result = builder.Build();

        result.ShouldContainKeyAndValue("video_url", "http://example.com/video.mp4");
    }

    [Test]
    public void AddVideoUrl_ShouldNotAddParameter_WhenNullOrWhiteSpace()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddVideoUrl(null);
        var result = builder.Build();

        result.ShouldNotContainKey("video_url");
    }

    [Test]
    public void AddText_ShouldAddParameter_WhenNotNullOrWhiteSpace()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddText("Sample text");
        var result = builder.Build();

        result.ShouldContainKeyAndValue("text", "Sample text");
    }

    [Test]
    public void AddText_ShouldNotAddParameter_WhenNullOrWhiteSpace()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddText(null);
        var result = builder.Build();

        result.ShouldNotContainKey("text");
    }

    [Test]
    public void AddAccessToken_ShouldAddParameter_WhenNotNullOrWhiteSpace()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddAccessToken("sample_access_token");
        var result = builder.Build();

        result.ShouldContainKeyAndValue("access_token", "sample_access_token");
    }

    [Test]
    public void AddAccessToken_ShouldNotAddParameter_WhenNullOrWhiteSpace()
    {
        var builder = new CreateSingleThreadPostParametersBuilder();
        builder.AddAccessToken(null);
        var result = builder.Build();

        result.ShouldNotContainKey("access_token");
    }
}