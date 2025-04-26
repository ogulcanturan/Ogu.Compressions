# <img src="logo/ogu-logo.png" alt="Header" width="24"/> Ogu.Compressions [![.NET Core Desktop](https://github.com/ogulcanturan/Ogu.Compressions/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/ogulcanturan/Ogu.Compressions/actions/workflows/dotnet.yml) [![NuGet](https://img.shields.io/nuget/v/Ogu.Compressions.svg?color=1ecf18)](https://nuget.org/packages/Ogu.Compressions) ![Nuget](https://img.shields.io/nuget/dt/Ogu.Compressions.svg?logo=nuget)

`Ogu.Compressions` provides a unified, extensible, and testable compression infrastructure for .NET applications. It abstracts the complexity of working with multiple compression algorithms and offers a provider-based model for resolving and applying compression strategies at runtime.

## Features

- **Unified Interface:** All compression algorithms implement a common `ICompression` interface, allowing consumers to interact with them in a consistent way.
- **Pluggable Architecture:** Each compression algorithm is packaged as its own library. This keeps the project lightweight and you choose only what you need.
- **Decompression Handler:** Includes a delegating handler for `HttpClient` that automatically decompresses responses using the `ICompressionProvider` interface.

## Packages Overview

## Packages Overview

| Package | Description |
|:--------|:------------|
| **Ogu.Compressions.Abstractions** | Core interfaces like `ICompression`, `ICompressionProvider`, `IBrotliCompression`, and a default `CompressionProvider` implementation. |
| **Ogu.Compressions.Brotli** | Brotli (Google) compression. Uses the built-in API if the target is newer than .NET Standard 2.0, otherwise uses the Brotli.NET library. |
| **Ogu.Compressions.Snappy** | Snappy (Google) compression optimized for fast real-time compression. Powered by the Snappier library. |
| **Ogu.Compressions.Zstd** | Zstandard (Facebook) compression, implemented using the ZstdSharp.Port library. |
| **Ogu.Compressions.Gzip** | Gzip compression, using .NET’s built-in `System.IO.Compression` APIs. |
| **Ogu.Compressions.Deflate** | Deflate compression, also using the built-in `System.IO.Compression` APIs. |
| **Ogu.Compressions.None** | A no-operation compression implementation that simply returns the input unmodified. |
| **Ogu.Compressions** | Aggregates all compression libraries and exposes the `AddCompressions` method to register everything at once. |
| **Ogu.AspNetCore.Compressions** | Aggregates the ASP.NET Core-specific compression providers for Deflate, Snappy, and Zstd. |
| **Ogu.AspNetCore.Compressions.Deflate** | Provides a `DeflateCompressionProvider` for ASP.NET Core middleware with "deflate" encoding support. |
| **Ogu.AspNetCore.Compressions.Snappy** | Provides a `SnappyCompressionProvider` for ASP.NET Core middleware with "snappy" encoding support. |
| **Ogu.AspNetCore.Compressions.Zstd** | Provides a `ZstdCompressionProvider` for ASP.NET Core middleware with "zstd" encoding support. |
| **Ogu.AspNetCore.Compression.Abstractions** | Defines unified interfaces for the `Ogu.AspNetCore.Compressions.*` libraries. |

---

## Getting Started With Ogu.Compressions.*

If you need **all compression algorithms**, install the main package:

```bash
dotnet add package Ogu.Compressions
```

### Registration

```csharp
services.AddCompressions();
```

You can inject `ICompressionProvider` to resolve compressions based on `CompressionType` (enum) or encoding names like `br`, `gzip`, `deflate`, `snappy`, `zstd`, `none`.

Example:

```csharp
private readonly ICompressionProvider _compressionProvider;

public BrotliController(ICompressionProvider compressionProvider)
{
    _compressionProvider = compressionProvider;
}
```

To resolve Brotli:

```csharp
var brotliCompression = _compressionProvider.GetCompression(CompressionType.Brotli);
```

Alternatively, you can inject the compression-specific interfaces directly:

```csharp
private readonly IBrotliCompression _compression;

public BrotliController(IBrotliCompression compression)
{
    _compression = compression;
}
```

Other compression types and their interfaces:
- **Deflate:** `IDeflateCompression`
- **Snappy:** `ISnappyCompression`
- **Zstd:** `IZstdCompression`
- **Gzip:** `IGzipCompression`
- **None:** `INoneCompression`

---

### Adding Decompression Handler to HttpClient

Register `DecompressionHandler`:

```csharp
services.AddSingleton<DecompressionHandler>();
services.AddHttpClient("MySampleApiClient", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://....com");

    // Add request headers to ask for compressed responses
    CompressionType.Brotli.AddToRequestHeaders(httpClient.DefaultRequestHeaders);
    CompressionType.Gzip.AddToRequestHeaders(httpClient.DefaultRequestHeaders);
}).AddHttpMessageHandler<DecompressionHandler>();
```

Register `ICompressionProvider` for the handler:

```csharp
services.AddCompressions();
```


Example usage:

```csharp
public class MySampleApiClient : IMySampleApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MySampleApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient("MySampleApiClient");
        return await httpClient.GetFromJsonAsync<Product[]>(cancellationToken: cancellationToken);
    }
}
```

The `DecompressionHandler` will automatically decompress the response if it recognizes the content encoding (like `br`, `gzip`, etc.). Unknown encodings will be skipped safely and you need to handle it.

---

### Known Encoding Mappings

| Encoding Name | CompressionType |
|:--------------|:----------------|
| brotli | `CompressionType.Brotli` |
| br | `CompressionType.Brotli` |
| deflate | `CompressionType.Deflate` |
| snappy | `CompressionType.Snappy` |
| zstandard | `CompressionType.Zstd` |
| zstd | `CompressionType.Zstd` |
| gzip | `CompressionType.Gzip` |

To customize mappings:

```csharp
services.AddSingleton<ICompressionTypeResolver>(sp =>
    new CompressionTypeResolver(new[] {
        new KeyValuePair<string, CompressionType>("custom-brotli", CompressionType.Brotli)
    }));
```

This lets you recognize specified aliases (like "custom-brotli" -> CompressionType.Brotli) returned by the server. When you register your custom implementation, default encoding mappings won't be recognized.

---

## Using Only Specific Algorithms

If you only need **Brotli**, install and register:

```bash
dotnet add package Ogu.Compressions.Brotli
```

```csharp
services.AddBrotliCompression();
services.AddCompressionProvider();
```

You can customize settings:

```csharp
services.AddBrotliCompression(options =>
{
    options.Level = CompressionLevel.Optimal;
    options.BufferSize = 4096;
});
```

Or configure via `IOptions<BrotliCompressionOptions>`:

```csharp
services.Configure<BrotliCompressionOptions>(options => { /* configure here */ });
```

---

## Documentation and Comments

Almost every method and property is documented with XML comments. Make sure to check them out for further clarification!

---

# Ogu.AspNetCore.Compressions [![NuGet](https://img.shields.io/nuget/v/Ogu.AspNetCore.Compressions.svg?color=1ecf18)](https://nuget.org/packages/Ogu.AspNetCore.Compressions) ![Nuget](https://img.shields.io/nuget/dt/Ogu.AspNetCore.Compressions.svg?logo=nuget)

Ogu.AspNetCore.Compressions extends ASP.NET Core’s native compression capabilities with additional options not included in the core framework. [More info](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/src/Ogu.AspNetCore.Compressions#readme)

## Installation

You can install the library via NuGet Package Manager:

```bash
dotnet add package Ogu.AspNetCore.Compressions
```

## Sample Application
A sample application demonstrating the usage of Ogu.Compressions & Ogu.AspNetCore.Compressions can be found [here](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/samples/Sample.Api).
