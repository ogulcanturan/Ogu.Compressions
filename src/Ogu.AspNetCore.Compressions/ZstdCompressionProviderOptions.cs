using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.AspNetCore.Compressions
{
    /// <summary>
    /// Provides configuration options for the Zstandard (zstd) compression provider, 
    /// including the compression level and buffer size.
    /// </summary>
    public sealed class ZstdCompressionProviderOptions : IOptions<ZstdCompressionProviderOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZstdCompressionProviderOptions"/> class 
        /// with default values for compression level (<see cref="CompressionLevel.Fastest"/>) and buffer size (81920).
        /// </summary>
        public ZstdCompressionProviderOptions() : this(CompressionLevel.Fastest, CompressionDefaults.BufferSize) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZstdCompressionProviderOptions"/> class
        /// with the specified compression level and buffer size.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use.</param>
        public ZstdCompressionProviderOptions(CompressionLevel level, int bufferSize)
        {
            Level = level;
            BufferSize = bufferSize;
        }

        /// <summary>
        /// Gets the name of the encoding used for underlying compression.
        /// </summary>
        public string EncodingName => EncodingNames.Zstd;

        /// <summary>
        /// Gets the compression level to use for the underlying stream.
        /// The default is <see cref="CompressionLevel.Fastest" />.
        /// </summary>
        public CompressionLevel Level { get; }

        /// <summary>
        /// Gets the size, in bytes, of the buffer. The default size is 81920.
        /// </summary>
        /// <remarks>
        /// Value must be greater than zero.
        /// </remarks>
        public int BufferSize { get; }


        ZstdCompressionProviderOptions IOptions<ZstdCompressionProviderOptions>.Value => this;
    }
}