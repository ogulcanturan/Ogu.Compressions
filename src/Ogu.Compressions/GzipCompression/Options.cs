using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    /// <summary>
    /// Provides configuration options for the Gzip (gzip) compression, 
    /// including the compression level and buffer size.
    /// </summary>
    public sealed class GzipCompressionOptions : CompressionOptions, IOptions<GzipCompressionOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GzipCompressionOptions"/> class 
        /// with default values for compression level (<see cref="CompressionLevel.Fastest"/>) and buffer size (81920).
        /// </summary>
        public GzipCompressionOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GzipCompressionOptions"/> class
        /// with the specified compression level and buffer size.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use.</param>
        public GzipCompressionOptions(CompressionLevel level, int bufferSize) : base(level, bufferSize)
        {
        }

        public override string EncodingName => EncodingNames.Gzip;

        public override CompressionType Type => CompressionType.Gzip;

        GzipCompressionOptions IOptions<GzipCompressionOptions>.Value => this;
    }
}