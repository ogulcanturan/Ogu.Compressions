namespace Ogu.Compressions.Abstractions
{
    public enum CompressionType
    {
        None = 0,
        Snappy = 1,
        Deflate = 2,
        Gzip = 3,
        Zstd = 4,
        Brotli = 5
    }
}