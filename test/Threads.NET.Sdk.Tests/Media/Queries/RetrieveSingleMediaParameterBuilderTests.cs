using Threads.NET.Sdk.Media.Queries.RetrieveSingleMedia;

namespace Threads.NET.Sdk.Tests.Media.Queries;

[TestFixture]
public class RetrieveSingleMediaParameterBuilderTests
{
    [Test]
    public void AddFields_ShouldAddParameter()
    {
        // Arrange
        var builder = new RetrieveSingleMediaParameterBuilder();

        // Act
        builder.AddFields();
        var result = builder.Build();

        // Assert
        result.ContainsKey("fields").ShouldBeTrue();
        result["fields"].ShouldBe("id,media_product_type,media_type,media_url,permalink,owner,username,text,timestamp,shortcode,thumbnail_url,children,is_quote_post");
    }

    [Test]
    public void AddAccessToken_ShouldAddParameter_WhenAccessTokenIsNotNullOrWhiteSpace()
    {
        // Arrange
        var builder = new RetrieveSingleMediaParameterBuilder();

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
        var builder = new RetrieveSingleMediaParameterBuilder();

        // Act
        builder.AddAccessToken(null);
        var result = builder.Build();

        // Assert
        result.ContainsKey("access_token").ShouldBeFalse();
    }
}