# Ogu.AspNetCore.Compressions.Snappy

This library extends ASP.NET Core’s native compression capabilities with snappy compression provider which not included in the core framework

## Usage

**Registering providers:**
```csharp
services.AddResponseCompression(opts =>
{
    opts.Providers.Add<SnappyCompressionProvider>();
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
