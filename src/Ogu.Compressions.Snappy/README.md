# Ogu.Compressions.Snappy

This library provides implementation for compressing and decompressing data using Snappy.

## Adding Package

```bash
dotnet add package Ogu.Compressions.Snappy
```

## Usage

**Registering provider:**

```csharp
services.AddSnappyCompression();
```

You can customize configuration by passing action delegate:

```csharp
services.AddSnappyCompression(options =>
{
    options.Level = CompressionLevel.Optimal;
    options.BufferSize = 4096;
});
```

Or configure via `IOptions<SnappyCompressionOptions>`:

```csharp
services.Configure<SnappyCompressionOptions>(options => { /* configure here */ });
```

| Configuration | Default Value |
|:--------------|:--------------|
| **Level** | `CompressionLevel.Fastest` |
| **BufferSize** | 81920 |

**Resolving service:**

you can inject the compression-specific interfaces directly:

```csharp
private readonly ISnappyCompression _compression;

public SnappyController(ISnappyCompression compression)
{
    _compression = compression;
}
```

**Links:**
- [GitHub](https://github.com/ogulcanturan/Ogu.Compressions)
- [Documentation](https://github.com/ogulcanturan/Ogu.Compressions#readme)
