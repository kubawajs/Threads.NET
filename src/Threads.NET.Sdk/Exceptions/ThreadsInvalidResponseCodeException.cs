namespace Threads.NET.Sdk.Exceptions;

internal sealed class ThreadsInvalidResponseCodeException(HttpStatusCode code, string responseContent)
    : ThreadsException($"API returned error code: {code}.\nResponse: {responseContent}")
{
}