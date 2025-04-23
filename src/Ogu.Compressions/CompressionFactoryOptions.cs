using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions
{
    public class CompressionFactoryOptions : IOptions<CompressionFactoryOptions>
    {
        /// <summary>
        /// The compression level to use for the stream.
        /// The default is <see cref="CompressionLevel.Fastest" />.
        /// </summary>
        public CompressionLevel Level { get; set; } = CompressionLevel.Fastest;

        /// <summary>
        /// The size, in bytes, of the buffer. The default size is 81920.
        /// </summary>
        /// <remarks>
        /// Value must be greater than zero.
        /// </remarks>
        public int BufferSize { get; set; } = CompressionDefaults.BufferSize;

        CompressionFactoryOptions IOptions<CompressionFactoryOptions>.Value => this;
    }
}