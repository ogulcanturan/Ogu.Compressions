# Ogu.Compressions.Zstd

This library provides implementation for compressing and decompressing data using Zstd.

## Adding Package

```bash
dotnet add package Ogu.Compressions.Zstd
```

## Usage

**Registering provider:**

```csharp
services.AddZstdCompression();
```

You can customize configuration by passing action delegate:

```csharp
services.AddZstdCompression(options =>
{
    options.Level = CompressionLevel.Optimal;
    options.BufferSize = 4096;
});
```

Or configure via `IOptions<ZstdCompressionOptions>`:

```csharp
services.Configure<ZstdCompressionOptions>(options => { /* configure here */ });
```

| Configuration | Default Value |
|:--------------|:--------------|
| **Level** | `CompressionLevel.Fastest` |
| **BufferSize** | 81920 |

**Resolving service:**

you can inject the compression-specific interfaces directly:

```csharp
private readonly IZstdCompression _compression;

public ZstdController(IZstdCompression compression)
{
    _compression = compression;
}
```

**Links:**
- [GitHub](https://github.com/ogulcanturan/Ogu.Compressions)
- [Documentation](https://github.com/ogulcanturan/Ogu.Compressions#readme)
