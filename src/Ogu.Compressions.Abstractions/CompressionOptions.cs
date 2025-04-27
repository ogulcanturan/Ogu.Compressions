using System.IO.Compression;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Represents the base class for options related to compression, such as the compression level and buffer size.
    /// </summary>
    public abstract class CompressionOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionOptions"/> class with default values.
        /// The default compression level is <see cref="CompressionLevel.Fastest"/> and the default buffer size is 81920 bytes.
        /// </summary>
        protected CompressionOptions() : this(CompressionLevel.Fastest, CompressionDefaults.BufferSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionOptions"/> class with compression level and default buffer size (81920).
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        protected CompressionOptions(CompressionLevel level) : this(level, CompressionDefaults.BufferSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionOptions"/> class with specified values for compression level and buffer size.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use. The default value is 81920 bytes and must be greater than zero.</param>
        protected CompressionOptions(CompressionLevel level, int bufferSize)
        {
            Level = level;
            BufferSize = bufferSize;
        }

        /// <summary>
        /// The compression level to use for the stream.
        /// The default is <see cref="CompressionLevel.Fastest" />.
        /// </summary>
        public CompressionLevel Level { get; set; }

        /// <summary>
        /// The size, in bytes, of the buffer. The default size is 81920.
        /// </summary>
        /// <remarks>
        /// Value must be greater than zero.
        /// </remarks>
        public int BufferSize { get; set; }

        /// <summary>
        /// The encoding name used for the compression. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).
        /// </summary>
        public abstract string EncodingName { get; }

        /// <summary>
        /// The type of compression. This value identifies which compression algorithm is used.
        /// </summary>
        public abstract CompressionType Type { get; }
    }
}