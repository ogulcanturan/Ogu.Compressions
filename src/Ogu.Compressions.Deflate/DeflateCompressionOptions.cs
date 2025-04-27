using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    /// <summary>
    /// Provides configuration options for the Deflate (deflate) compression, 
    /// including the compression level and buffer size.
    /// </summary>
    public sealed class DeflateCompressionOptions : CompressionOptions, IOptions<DeflateCompressionOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeflateCompressionOptions"/> class 
        /// with default values for compression level (<see cref="CompressionLevel.Fastest"/>) and buffer size (81920).
        /// </summary>
        public DeflateCompressionOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeflateCompressionOptions"/> class 
        /// with compression level and default buffer size (81920).
        /// </summary>
        public DeflateCompressionOptions(CompressionLevel level) : base(level)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeflateCompressionOptions"/> class
        /// with the specified compression level and buffer size.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use. The default value is 81920 bytes and must be greater than zero.</param>
        public DeflateCompressionOptions(CompressionLevel level, int bufferSize) : base(level, bufferSize)
        {
        }

        public override string EncodingName => CompressionDefaults.EncodingNames.Deflate;

        public override CompressionType Type => CompressionType.Deflate;

        DeflateCompressionOptions IOptions<DeflateCompressionOptions>.Value => this;
    }
}