# Ogu.Compressions.Deflate

This library provides implementation for compressing and decompressing data using Deflate.

## Adding Package

```bash
dotnet add package Ogu.Compressions.Deflate
```

## Usage

**Registering provider:**

```csharp
services.AddDeflateCompression();
```

You can customize configuration by passing action delegate:

```csharp
services.AddDeflateCompression(options =>
{
    options.Level = CompressionLevel.Optimal;
    options.BufferSize = 4096;
});
```

Or configure via `IOptions<DeflateCompressionOptions>`:

```csharp
services.Configure<DeflateCompressionOptions>(options => { /* configure here */ });
```

| Configuration | Default Value |
|:--------------|:--------------|
| **Level** | `CompressionLevel.Fastest` |
| **BufferSize** | 81920 |

**Resolving service:**

you can inject the compression-specific interfaces directly:

```csharp
private readonly IDeflateCompression _compression;

public DeflateController(IDeflateCompression compression)
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
