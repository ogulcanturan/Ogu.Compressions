using Microsoft.Extensions.Options;
using System.IO.Compression;
using Ogu.Compressions.Abstractions;

namespace Ogu.AspNetCore.Compressions
{
    public sealed class ZstdCompressionProviderOptions : IOptions<ZstdCompressionProviderOptions>
    {
        public ZstdCompressionProviderOptions() { }

        public ZstdCompressionProviderOptions(CompressionLevel level, int bufferSize)
        {
            Level = level;
            BufferSize = bufferSize;
        }

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


        ZstdCompressionProviderOptions IOptions<ZstdCompressionProviderOptions>.Value => this;
    }
}