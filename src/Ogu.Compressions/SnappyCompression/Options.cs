using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    /// <summary>
    /// Provides configuration options for the Snappy (snappy) compression, 
    /// including the compression level and buffer size.
    /// </summary>
    public sealed class SnappyCompressionOptions : CompressionOptions, IOptions<SnappyCompressionOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnappyCompressionOptions"/> class 
        /// with default values for compression level (<see cref="CompressionLevel.Fastest"/>) and buffer size (81920).
        /// </summary>
        public SnappyCompressionOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnappyCompressionOptions"/> class
        /// with the specified compression level and buffer size.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use.</param>
        public SnappyCompressionOptions(CompressionLevel level, int bufferSize) : base(level, bufferSize)
        {
        }

        public override string EncodingName => EncodingNames.Snappy;

        public override CompressionType Type => CompressionType.Snappy;

        SnappyCompressionOptions IOptions<SnappyCompressionOptions>.Value => this;
    }
}