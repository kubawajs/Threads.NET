namespace Threads.NET.Sdk.Exceptions;

internal sealed class ThreadsAuthenticationException(string responseContent)
    : ThreadsException($"Failed to exchange code for token: {responseContent}")
{
}
