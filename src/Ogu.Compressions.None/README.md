# Ogu.Compressions.None

This library provides no-operation compression implementation that simply returns the input unmodified.

## Adding Package

```bash
dotnet add package Ogu.Compressions.None
```

## Usage

**Registering provider:**

```csharp
services.AddNoneCompression();
```

You can customize configuration by passing action delegate:

```csharp
services.AddNoneCompression(options =>
{
    options.Level = CompressionLevel.Optimal;
    options.BufferSize = 4096;
});
```

Or configure via `IOptions<NoneCompressionOptions>`:

```csharp
services.Configure<NoneCompressionOptions>(options => { /* configure here */ });
```

| Configuration | Default Value |
|:--------------|:--------------|
| **Level** | `CompressionLevel.Fastest` |
| **BufferSize** | 81920 |

**Resolving service:**

you can inject the compression-specific interfaces directly:

```csharp
private readonly INoneCompression _compression;

public NoneController(INoneCompression compression)
{
    _compression = compression;
}
```

**Links:**
- [GitHub](https://github.com/ogulcanturan/Ogu.Compressions)
- [Documentation](https://github.com/ogulcanturan/Ogu.Compressions#readme)
