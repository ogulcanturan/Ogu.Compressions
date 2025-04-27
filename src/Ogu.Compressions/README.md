# Ogu.Compressions

This library provides concrete implementations of most popular compression algorithms using third-party libraries and also some utilities

## Usage

**Registering all compressions including ICompressionProvider ( Brotli, Deflate, Snappy, Zstd, Gzip, None ):**
```csharp
services.AddCompressions(opts =>
{
    opts.Level = CompressionLevel.Optimal;
});
```

**To resolve a specific compression:**
```csharp
private readonly IBrotliCompression _compression;

public BrotliController(IBrotliCompression compression)
{
     _compression = compression;
}
```

To resolve other compression types;
- **Deflate:** IDeflateCompression
- **Snappy:** ISnappyCompression
- **Zstd:** IZstdCompression
- **Gzip:** IGzipCompression
- **None:** INoneCompression

**To resolve compression using ICompressionProvider:**
```csharp
private readonly ICompressionProvider _compressionProvider;

public DecompressionHandler(ICompressionProvider compressionProvider)
{
    _compressionProvider = compressionProvider;
}
```

**brotli compression**
```csharp
var brotliCompression = _compressionFactory.Get(CompressionType.Brotli);
```

**Adding decompression handler to HttpClient**
```csharp
// registration
services.AddSingleton<DecompressionHandler>();
services.AddHttpClient("MySampleApiClient", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://...com")    

    // Request encoding (br, gzip) from server if possible
    CompressionType.Brotli.AddToRequestHeaders(httpClient.DefaultRequestHeaders);
    CompressionType.Gzip.AddToRequestHeaders(httpClient.DefaultRequestHeaders);
}).AddHttpMessageHandler<DecompressionHandler>();

// Register ICompressionProvider for DecompressionHandler to support all compressions
services.AddCompressions(opts =>
{
    opts.Level = CompressionLevel.Optimal;
});
```

To use

```csharp
public class MySampleApiClient : IMySampleApiClient
{
  private readonly IHttpClientFactory _httpClientFactory;

  public MySampleApiClient(IHttpClientFactory httpClientFactory)
  {
  }

  public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken)
  {
    var httpClient = _httpClientFactory.CreateClient("MySampleApiClient");
   
    return await httpClient.GetFromJsonAsync<Products[]>(cancellationToken: cancellationToken);
  }
}
```

When making requests through httpClient, the DecompressionHandler will decompress if the content is encoded.

**Links:**
- [GitHub](https://github.com/ogulcanturan/Ogu.Compressions)
- [Documentation](https://github.com/ogulcanturan/Ogu.Compressions#readme)
