# <img src="logo/ogu-logo.png" alt="Header" width="24"/> Ogu.Compressions [![.NET Core Desktop](https://github.com/ogulcanturan/Ogu.Compressions/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/ogulcanturan/Ogu.Compressions/actions/workflows/dotnet.yml) [![NuGet](https://img.shields.io/nuget/v/Ogu.Compressions.svg?color=1ecf18)](https://nuget.org/packages/Ogu.Compressions) ![Nuget](https://img.shields.io/nuget/dt/Ogu.Compressions.svg?logo=nuget)

`Ogu.Compressions` provides a unified, extensible, and testable compression infrastructure for .NET applications. It abstracts the complexity of working with multiple compression algorithms and offers a provider-based model for resolving and applying compression strategies at runtime.

## Features

- **Unified Interface:** All compression algorithms implement a common `ICompression` interface, allowing consumers to interact with them in a consistent way.
- **Pluggable Architecture:** Each compression algorithm is packaged as its own library. This keeps the project lightweight and you choose only what you need.
- **Decompression Handler:** Includes a delegating handler for `HttpClient` that automatically decompresses responses if known encoding detected using the `ICompressionProvider` interface.

## Packages Overview

| Package | Description |
|:--------|:------------|
| **Ogu.Compressions.Abstractions** | Core interfaces like `ICompression`, `ICompressionProvider`, `IBrotliCompression`, and a default `CompressionProvider` implementation. |
| **[Ogu.Compressions.Brotli](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/src/Ogu.Compressions.Brotli)** | Brotli (Google) compression. Uses the built-in API if the target is newer than .NET Standard 2.0, otherwise it falls back to the [Brotli.NET](https://github.com/XieJJ99/brotli.net) third-party library. |
| **[Ogu.Compressions.Brotli.Native](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/src/Ogu.Compressions.Brotli.Native)** | Brotli (Google) compression. Uses the [Brotli.NET](https://github.com/XieJJ99/brotli.net) third-party library. |
| **[Ogu.Compressions.Snappy](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/src/Ogu.Compressions.Snappy)** | Snappy (Google) compression optimized for fast compression/decompression, mainly used in real-time messaging (e.g., RPC). Since .NET does not have native support for Snappy, this package uses the [Snappier](https://github.com/brantburnett/Snappier) library internally. |
| **[Ogu.Compressions.Zstd](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/src/Ogu.Compressions.Zstd)** | Zstandard (Facebook) compression. Since .NET does not have native support for Zstandard, this package uses the  [ZstdSharp.Port](https://github.com/oleg-st/ZstdSharp) library internally. |
| **[Ogu.Compressions.Gzip](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/src/Ogu.Compressions.Gzip)** | Gzip compression, using .NET’s built-in `System.IO.Compression` APIs. |
| **[Ogu.Compressions.Deflate](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/src/Ogu.Compressions.Deflate)** | Deflate compression, also using the built-in `System.IO.Compression` APIs. |
| **[Ogu.Compressions.None](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/src/Ogu.Compressions.None)** | A no-operation compression implementation that simply returns the input unmodified. |
| **[Ogu.Compressions](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/src/Ogu.Compressions)** | Aggregates all compression libraries and exposes the `AddCompressions` method to register everything at once. |

## Usage

If you need **all compression algorithms**, install the main package:

```bash
dotnet add package Ogu.Compressions
```

### Registration

To register the compressions with default configuration, you can call:

```csharp
services.AddCompressions();
```

You can also pass configuration options for the compression setup by providing a delegate:

```csharp
services.AddCompressions(opts =>
{
    opts.UseNativeBrotli = false;
    opts.CompressionOptions = compressionOpts =>
    {
        compressionOpts.Level = CompressionLevel.Fastest;
        compressionOpts.BufferSize = 81920;
    }
})
```

If you prefer to use the native brotli by setting `opts.UseNativeBrotli = true`, you may want to configure the window size. In that case, you will also need to add the following configuration:

```csharp
services.Configure<NativeBrotliCompressionOptions>(opts => 
{
    opts.WindowSize = 22;
});
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

Compress

```csharp
string data = "Hello, World!";
bytes[] compressedData = await _compression.CompressAsync(data);
```

Decompress

```csharp
bytes[] decompressedData = await _compression.DecompressAsync(compressedData);
string data = System.Text.Encoding.UTF8.GetString(decompressedData);
```

[Methods](https://github.com/ogulcanturan/Ogu.Compressions/blob/master/src/Ogu.Compressions.Abstractions/ICompression.cs):
- Compress(..)
- CompressAsync(..)
- Decompress(..)
- DecompressAsync(..)
- CompressToStream(..)
- CompressToStreamAsync(..)
- DecompressToStream(..)
- DecompressToStreamAsync(..)

Other compression types and their interfaces:
- **Deflate:** `IDeflateCompression`
- **Snappy:** `ISnappyCompression`
- **Zstd:** `IZstdCompression`
- **Gzip:** `IGzipCompression`
- **None:** `INoneCompression`

### Adding Decompression Handler to HttpClient

Register `DecompressionHandler`:

```csharp
services.AddSingleton<DecompressionHandler>();
services.AddHttpClient("MySampleApiClient", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://....com");

    // Inform the service that Brotli decompression is supported
    CompressionType.Brotli.AddToRequestHeaders(httpClient.DefaultRequestHeaders);
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

> [!NOTE]  
> When using multiple http delegating handlers, the order matters. To decompress the response body (including error responses), place the decompression handler before the resilience handler (e.g., StandardResilienceHandler).
If you have custom handlers (like logging or authentication), they should go before the decompression handler so they can run first.  

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

**To customize mappings:**

```csharp
services.AddSingleton<ICompressionTypeResolver>(sp =>
    new CompressionTypeResolver(new[] {
        new KeyValuePair<string, CompressionType>("custom-brotli", CompressionType.Brotli)
    }));
```

This lets you recognize specified aliases (like "custom-brotli" -> CompressionType.Brotli) returned by the server. When you register your custom implementation, default encoding mappings won't be recognized.

### Using Only Specific Algorithms

If you only need **Brotli**, install and register:

```bash
dotnet add package Ogu.Compressions.Brotli
```

```csharp
services.AddBrotliCompression();
services.AddCompressionProvider();
```

You can customize configuration by passing action delegate:

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
