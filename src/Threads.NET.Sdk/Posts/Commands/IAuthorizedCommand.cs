namespace Threads.NET.Sdk.Posts.Commands;

internal interface IAuthorizedCommand
{
    /// <summary>
    /// Access token used for validation.
    /// </summary>
    public string AccessToken { get; }

    /// <summary>
    /// Threads User ID.
    /// </summary>
    public string UserId { get; }
}