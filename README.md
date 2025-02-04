# Threads.NET

[![Publish NuGet Package](https://github.com/kubawajs/Threads.NET/actions/workflows/publish-nuget.yml/badge.svg)](https://github.com/kubawajs/Threads.NET/actions/workflows/publish-nuget.yml)
![NuGet Version](https://img.shields.io/nuget/vpre/Threads.NET.Sdk?logo=nuget&labelColor=%23004880)

A .NET SDK that simplifies interaction with the Meta Threads API, enabling developers to integrate Threads features into their .NET applications.

## Features

- [x] Authorization
- [ ] Posts
    - [x] Create a single thread post
    - [ ] Create carousel post
    - [x] Publish media container

In progress...

---

## Installation

Install the SDK via NuGet:

```bash
dotnet add package Threads.NET.Sdk
```

---

## Getting Started

### Prerequisites

- .NET 8 or later
- A valid Meta Threads API key (obtainable from the [Meta Developer Portal](https://developers.meta.com)).

### Usage

TODO

## Configuration

Register the client to enable interaction with the Meta Threads API. This configuration sets up the necessary options for the client, including the client ID, client secret, HTTP timeout, and redirect URI.

```charp
builder.Services.AddThreadsClient(options =>
{
    options.ClientId = "";
    options.ClientSecret = "";
    options.HttpTimeout = TimeSpan.FromSeconds(60));
    options.RedirectUri = "";
});
```

## Contributing

Contributions are welcome! To contribute:

TODO

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## Support

If you encounter any issues, feel free to [open an issue](https://github.com/kubawajs/threads-net/issues).

---

## Resources

- [Meta Threads API Overview](https://developers.facebook.com/docs/threads/overview)
- NuGet Package - TODO
