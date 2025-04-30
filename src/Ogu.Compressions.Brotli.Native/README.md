# Ogu.Compressions.Brotli.Native

This library provides implementation for compressing and decompressing data using native brotli library (Brotli.NET).

## Adding Package

```bash
dotnet add package Ogu.Compressions.Brotli.Native
```

## Usage

**Registering provider:**

```csharp
services.AddNativeBrotliCompression();
```

You can customize configuration by passing action delegate:

```csharp
services.AddNativeBrotliCompression(options =>
{
    options.Level = CompressionLevel.Optimal;
    options.BufferSize = 4096;
    options.WindowSize = 22; // Valid range: 10 - 24
});
```

Or configure via `IOptions<NativeBrotliCompressionOptions>`:

```csharp
services.Configure<NativeBrotliCompressionOptions>(options => { /* configure here */ });
```

| Configuration  | Default Value              |
|:---------------|:---------------------------|
| **Level**      | `CompressionLevel.Fastest` |
| **BufferSize** | 81920                      |
| **WindowSize** | 22                         |

**Resolving service:**

you can inject the compression-specific interfaces directly:

```csharp
private readonly IBrotliCompression _compression;

public BrotliController(IBrotliCompression compression)
{
    _compression = compression;
}
```

**Links:**
- [GitHub](https://github.com/ogulcanturan/Ogu.Compressions)
- [Documentation](https://github.com/ogulcanturan/Ogu.Compressions#readme)
