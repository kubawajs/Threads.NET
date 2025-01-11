
namespace Threads.NET.Sdk.Exceptions;

internal sealed class ThreadsDeserializationException(Type type, string responseContent)
    : ThreadsException($"Cannot deserialize response from API. Target type: {nameof(type)}.\nResponse: {responseContent}")
{
}