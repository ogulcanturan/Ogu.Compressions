# Ogu.AspNetCore.Compressions.Zstd

This library extends ASP.NET Core’s native compression capabilities with zstd compression provider which not included in the core framework

## Usage

**Changing provider options compression level:**
```csharp
services.Configure<ZstdCompressionProviderOptions>(opts =>
{
    opts.Level = CompressionLevel.Fastest;
    opts.BufferSize = 81920;
});
```

**Registering providers:**
```csharp
services.AddResponseCompression(opts =>
{
    // Server decides which provider to use - (e.g. If client requests gzip, br - server will use available first encoding in this case it would be zstd )
    opts.Providers.Add<ZstdCompressionProvider>();

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