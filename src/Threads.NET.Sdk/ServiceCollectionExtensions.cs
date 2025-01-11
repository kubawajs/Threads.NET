using Microsoft.Extensions.DependencyInjection;
using Threads.NET.Sdk;
using Threads.NET.Sdk.Authentication;
using Threads.NET.Sdk.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddThreadsClient(
        this IServiceCollection services,
        Action<ThreadsClientOptions> configureOptions)
    {
        services.Configure(configureOptions);

        services.AddHttpClient("Threads", (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<ThreadsClientOptions>>().Value;

            client.BaseAddress = new Uri(Constants.ApiUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = options.HttpTimeout;
        });

        services.AddScoped<IThreadsAuthenticationClient, ThreadsAuthenticationClient>();
        services.AddScoped<IThreadsClient, ThreadsClient>();

        return services;
    }
}