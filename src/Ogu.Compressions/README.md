# Ogu.Compressions

This library aggregates other Ogu.Compressions.* libraries and provides `AddCompressions()` extension method to register all compressions.

## Adding Package

```bash
dotnet add package Ogu.Compressions
```

## Usage

**Registering provider:**

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

**Links:**
- [GitHub](https://github.com/ogulcanturan/Ogu.Compressions)
- [Documentation](https://github.com/ogulcanturan/Ogu.Compressions#readme)
