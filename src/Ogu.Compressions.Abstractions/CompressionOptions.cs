using System.IO.Compression;

namespace Ogu.Compressions.Abstractions
{
    public class CompressionOptions
    {
        /// <summary>
        /// What level of compression to use for the stream. The default is Fastest.
        /// </summary>
        public virtual CompressionLevel Level { get; set; } = CompressionLevel.Fastest;

        /// <summary>
        /// The size, in bytes, of the buffer. This value must be greater than zero. The default size is 81920.
        /// </summary>
        public virtual int BufferSize { get; set; } = 81920;

        /// <summary>
        /// The encoding name used for the compression. e.g. "br", "deflate", "snappy", "zstd", "gzip" or "" for NoneCompression.
        /// </summary>
        public virtual string EncodingName { get; }

        /// <summary>
        /// The type of compression. This value identifies which compression algorithm is used.
        /// </summary>
        public virtual CompressionType Type { get; }
    }
}