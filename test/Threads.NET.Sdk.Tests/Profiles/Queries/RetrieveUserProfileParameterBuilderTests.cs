using Threads.NET.Sdk.Profiles.Queries.RetrieveUserProfile;

namespace Threads.NET.Sdk.Tests.Profiles.Queries;

[TestFixture]
public class RetrieveUserProfileParameterBuilderTests
{
    [Test]
    public void AddFields_ShouldAddParameter()
    {
        // Arrange
        var builder = new RetrieveUserProfileParameterBuilder();

        // Act
        builder.AddFields();
        var result = builder.Build();

        // Assert
        result.ContainsKey("fields").ShouldBeTrue();
        result["fields"].ShouldBe("id,username,name,threads_profile_picture_url,threads_biography");
    }

    [Test]
    public void AddAccessToken_ShouldAddParameter_WhenAccessTokenIsNotNullOrWhiteSpace()
    {
        // Arrange
        var builder = new RetrieveUserProfileParameterBuilder();

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
        var builder = new RetrieveUserProfileParameterBuilder();

        // Act
        builder.AddAccessToken(null);
        var result = builder.Build();

        // Assert
        result.ContainsKey("access_token").ShouldBeFalse();
    }
}

