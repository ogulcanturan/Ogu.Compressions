namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Represents the supported compression algorithms that can be used for encoding and decoding data streams.
    /// </summary>
    public enum CompressionType
    {
        /// <summary>
        /// No compression. Used internally to indicate that no compression should be applied.
        /// </summary>
        None = 0,

        /// <summary>
        /// The compression type used for <c>snappy</c> encoding.
        /// </summary>
        Snappy = 1,

        /// <summary>
        /// The compression type used for <c>deflate</c> encoding.
        /// </summary>
        Deflate = 2,

        /// <summary>
        /// The compression type used for <c>gzip</c> encoding.
        /// </summary>
        Gzip = 3,

        /// <summary>
        /// The compression type used for <c>zstd</c> (Zstandard) encoding.
        /// </summary>
        Zstd = 4,

        /// <summary>
        /// The compression type used for <c>br</c> (Brotli) encoding.
        /// </summary>
        Brotli = 5
    }
}