using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Threads.NET.Sdk.Authentication;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddThreadsClient(
        this IServiceCollection services,
        Action<ThreadsClientOptions> configureOptions)
    {
        services.Configure(configureOptions);

        services.AddHttpClient<IAuthenticationClient, AuthenticationClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<ThreadsClientOptions>>().Value;

            client.BaseAddress = new Uri("https://graph.threads.net/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = options.HttpTimeout;
        });

        return services;
    }
}