namespace Threads.NET.Sdk.Authorization;

// TODO: to be moved in more generic place
internal interface IAuthorizedRequest<out TResponse> : IRequest<TResponse>
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