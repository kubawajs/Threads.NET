using Microsoft.AspNetCore.Mvc;
using Threads.Net.WebApp.Components;
using Threads.Net.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddThreadsClient(options =>
{
    options.ClientId = builder.Configuration["Threads:ClientId"] ?? "";
    options.ClientSecret = builder.Configuration["Threads:ClientSecret"] ?? "";
    options.HttpTimeout = TimeSpan.FromSeconds(int.Parse(builder.Configuration["Threads:HttpTimeout"] ?? ""));
    options.RedirectUri = builder.Configuration["Threads:RedirectUri"] ?? "";
});

builder.Services.AddSingleton<AuthService>(); // TODO: Temporary register as singleton to share between components.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapGet("auth/callback", ([FromQuery] string? code, AuthService authService) =>
{
    if (!string.IsNullOrEmpty(code))
    {
        authService.Code = code;
    }
    return Results.Redirect("/authentication");
});

app.Run();
