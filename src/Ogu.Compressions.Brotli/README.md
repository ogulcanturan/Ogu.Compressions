# Ogu.Compressions.Brotli

This library provides implementation for compressing and decompressing data using Brotli.

## Adding Package

```bash
dotnet add package Ogu.Compressions.Brotli
```

## Usage

**Registering provider:**

```csharp
services.AddBrotliCompression();
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

| Configuration | Default Value |
|:--------------|:--------------|
| **Level** | `CompressionLevel.Fastest` |
| **BufferSize** | 81920 |

**Resolving service:**

you can inject the compression-specific interfaces directly:

```csharp
private readonly IBrotliCompression _compression;

public BrotliController(IBrotliCompression compression)
{
    _compression = compression;
}
```

**Compress:**

```csharp
string data = "Hello, World!";
bytes[] compressedData = await _compression.CompressAsync(data);
```

**Decompress:**

```csharp
bytes[] decompressedData = await _compression.DecompressAsync(compressedData);
string data = System.Text.Encoding.UTF8.GetString(decompressedData);
```

**Links:**
- [GitHub](https://github.com/ogulcanturan/Ogu.Compressions)
- [Documentation](https://github.com/ogulcanturan/Ogu.Compressions#readme)
