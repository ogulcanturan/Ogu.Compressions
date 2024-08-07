# Ogu.AspNetCore.Compressions

This library extends ASP.NET Core’s native compression capabilities with additional options not included in the core framework

## Usage

**Changing provider options compression level:**
```csharp
services.Configure<BrotliCompressionProviderOptions>(opts =>
{
    opts.Level = CompressionLevel.Fastest;
});
```

**Registering providers:**
```csharp
services.AddResponseCompression(opts =>
{
    // Server decides which provider to use - (e.g. If client requests gzip, br - server will use available first encoding )
    opts.Providers.Add<BrotliCompressionProvider>();
    opts.Providers.Add<ZstdCompressionProvider>();
    opts.Providers.Add<GzipCompressionProvider>();
    opts.Providers.Add<SnappyCompressionProvider>();
    opts.Providers.Add<DeflateCompressionProvider>();

    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes;

    opts.EnableForHttps = true;
});
```

**Adding response compression [middleware](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0):**
```csharp
app.UseResponseCompression();
```


**Links:**
- [GitHub](https://github.com/ogulcanturan/Ogu.Compressions)
- [Documentation](https://github.com/ogulcanturan/Ogu.Compressions#readme)
