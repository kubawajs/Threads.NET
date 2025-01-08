public class ThreadsClientOptions
{
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string RedirectUri { get; set; } = null!;

    public TimeSpan HttpTimeout { get; set; } = TimeSpan.FromSeconds(30);
}