# Ogu.AspNetCore.Compressions

This library extends ASP.NET Core’s native compression capabilities with additional options not included in the core framework.

## Usage

**Changing provider options compression level:**
```csharp
services.Configure<BrotliCompressionProviderOptions>(opts =>
{
    opts.Level = CompressionLevel.Fastest;
});
```

| Encoding Name | CompressionProviderOptions |
|:--------------|:----------------|
| brotli | `BrotliCompressionProviderOptions` |
| snappy | `SnappyCompressionProviderOptions` |
| zstandard | `ZstdCompressionProviderOptions` |
| gzip | `GzipCompressionProviderOptions` |
| deflate | `DeflateCompressionProviderOptions` |

**Registering providers:**

The server decides which compression provider to use based on the client's `Accept-Encoding` header. For example, if a client sends `Accept-Encoding: gzip, br, zstd`, the server will select the first supported encoding in the list.

In the example below, the server will respond using Brotli (`br`) compression because the `BrotliCompressionProvider` registered first with `opts.Providers.Add<BrotliCompressionProvider>()`.

It is the caller’s responsibility to handle the response correctly.
The server includes the actual encoding used in the `Content-Encoding` header — in this case, it will be `br`.

```csharp
services.AddResponseCompression(opts =>
{
    opts.Providers.Add<BrotliCompressionProvider>();
    opts.Providers.Add<ZstdCompressionProvider>();
    opts.Providers.Add<GzipCompressionProvider>();
    opts.Providers.Add<SnappyCompressionProvider>();
    opts.Providers.Add<DeflateCompressionProvider>();

    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes;

    opts.EnableForHttps = true;
});
```

If the client requests encodings that the server does not support (e.g., `Accept-Encoding: special`), **compression will not occur**, and the response will be sent uncompressed.

> [!NOTE]  
> `opts.MimeTypes` defines which response `Content-Type`s are eligible for compression. In this example, it uses the defaults provided by `ResponseCompressionDefaults.MimeTypes` (e.g., `text/plain`, `application/json`, etc.).

**Adding the Middleware**

The last step is to add the response compression middleware to the pipeline, so it can automatically handle compression for outgoing responses.

```csharp
app.UseResponseCompression();
```

Once added, the server will compress eligible responses based on the client’s `Accept-Encoding` header and the registered compression providers.

> [!IMPORTANT]  
> Middleware must be registered in the correct order.  
> For more details, refer to the [official middleware documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-9.0). 

**Links:**
- [GitHub](https://github.com/ogulcanturan/Ogu.Compressions)
- [Documentation](https://github.com/ogulcanturan/Ogu.Compressions#readme)
