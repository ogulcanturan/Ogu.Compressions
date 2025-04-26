using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    /// <summary>
    /// Provides configuration options for the Gzip (gzip) compression, 
    /// including the compression level and buffer size.
    /// </summary>
    public sealed class NoneCompressionOptions : CompressionOptions, IOptions<NoneCompressionOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoneCompressionOptions"/> class 
        /// with default values for compression level (<see cref="CompressionLevel.Fastest"/>) and buffer size (81920).
        /// </summary>
        public NoneCompressionOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoneCompressionOptions"/> class
        /// with the specified compression level and buffer size.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use.</param>
        public NoneCompressionOptions(CompressionLevel level, int bufferSize) : base(level, bufferSize)
        {
        }

        public override string EncodingName => CompressionDefaults.EncodingNames.None;

        public override CompressionType Type => CompressionType.None;

        NoneCompressionOptions IOptions<NoneCompressionOptions>.Value => this;
    }
}