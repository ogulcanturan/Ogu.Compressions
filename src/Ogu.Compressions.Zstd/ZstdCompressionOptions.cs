using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    /// <summary>
    /// Provides configuration options for the Zstandard (zstd) compression, 
    /// including the compression level and buffer size.
    /// </summary>
    public class ZstdCompressionOptions : CompressionOptions, IOptions<ZstdCompressionOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZstdCompressionOptions"/> class 
        /// with default values for compression level (<see cref="CompressionLevel.Fastest"/>) and buffer size (81920).
        /// </summary>
        public ZstdCompressionOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZstdCompressionOptions"/> class
        /// with the specified compression level and buffer size.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use. The default value is 81920 bytes and must be greater than zero.</param>
        public ZstdCompressionOptions(CompressionLevel level, int bufferSize) : base(level, bufferSize)
        {
        }

        public override string EncodingName => CompressionDefaults.EncodingNames.Zstd;

        public override CompressionType Type => CompressionType.Zstd;

        ZstdCompressionOptions IOptions<ZstdCompressionOptions>.Value => this;
    }
}