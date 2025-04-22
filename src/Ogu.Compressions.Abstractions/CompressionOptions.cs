using System.IO.Compression;

namespace Ogu.Compressions.Abstractions
{
    public abstract class CompressionOptions
    {
        protected CompressionOptions() : this(CompressionLevel.Fastest, 81920)
        {
        }

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
        /// The encoding name used for the compression. e.g. "br", "deflate", "snappy", "zstd", "gzip" or "" for NoneCompression.
        /// </summary>
        public abstract string EncodingName { get; }

        /// <summary>
        /// The type of compression. This value identifies which compression algorithm is used.
        /// </summary>
        public abstract CompressionType Type { get; }
    }
}