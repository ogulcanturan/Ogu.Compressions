# Ogu.AspNetCore.Compressions.Deflate

This library extends ASP.NET Core’s native compression capabilities with deflate compression provider which not included in the core framework

## Usage

**Changing provider options compression level:**
```csharp
services.Configure<DeflateCompressionOptions>(opts =>
{
    opts.Level = CompressionLevel.Fastest;
});
```

**Registering providers:**
```csharp
services.AddResponseCompression(opts =>
{
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
- [Documentation](https://github.com/ogulcanturan/Ogu.Compressions/tree/master/src/Ogu.AspNetCore.Compressions#readme)
