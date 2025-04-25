namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Provides default values for compression.
    /// </summary>
    public static class CompressionDefaults
    {
        /// <summary>
        /// The default size, in bytes, of the buffer.
        /// </summary>
        public const int BufferSize = 81920;

        /// <summary>
        /// Contains constants for encoding names used in compression algorithms.
        /// </summary>
        public static class EncodingNames
        {
            /// <summary>
            /// The encoding name used for <see cref="CompressionType.Brotli" /> compression.
            /// </summary>
            public const string Brotli = "br";

            /// <summary>
            /// The encoding name used for <see cref="CompressionType.Deflate" /> compression.
            /// </summary>
            public const string Deflate = "deflate";

            /// <summary>
            /// The encoding name used for <see cref="CompressionType.Snappy" /> compression.
            /// </summary>
            public const string Snappy = "snappy";

            /// <summary>
            /// The encoding name used for <see cref="CompressionType.Zstd" /> compression.
            /// </summary>
            public const string Zstd = "zstd";

            /// <summary>
            /// The encoding name used for <see cref="CompressionType.Gzip" /> compression.
            /// </summary>
            public const string Gzip = "gzip";

            /// <summary>
            /// The encoding name used for <see cref="CompressionType.None" /> compression.
            /// This value is used internally to indicate no compression and does not correspond to a real encoding header.
            /// </summary>
            public const string None = "none";
        }
    }
}